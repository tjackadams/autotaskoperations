﻿namespace Autotask.Operations
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using Constants;
    using Extensions;

    /// <summary>
    ///     A collection of methods to generate a correctly formatted <see cref="XDocument" /> that can be used to query the
    ///     Autotask api.
    /// </summary>
    public class Query
    {
        /// <summary>
        ///     returns the query as an xml document ready to use with the autotask api
        /// </summary>
        /// <param name="queryString">The query as <see cref="string" /> </param>
        /// <param name="udf">A <see cref="bool" /> value to determine if the query includes a udf field or not</param>
        public static XDocument Generate(string queryString, bool udf = false)
        {
            // first convert the queryString into a string array
            string[] queryText = queryString.Split(RegexOptions.Multiline);

            // allowed operators
            var operators = Operators.Get;
            var conditions = Conditions.Get;
            var noValueNeededCondition = Conditions.GetNoValue;

            // create the xml document
            XDocument xml = new XDocument();

            // create base element and add a single Entity definition to it
            var rootNode = new XElement("queryxml");
            rootNode.SetAttributeValue("version", "1.0");

            var entityNode = new XElement("entity")
            {
                Value = queryText[0]
            };

            rootNode.Add(entityNode);

            // create an xml element for the query tag
            // this will contain all conditions
            var queryNode = new XElement("query");
            rootNode.Add(queryNode);

            // set generic pointer node to query tag
            var node = queryNode;

            // create an index pointer that starts on the second element
            // of the queryText array
            for (int i = 1; i < queryText.Length; i++)
            {
                var currentText = queryText[i];

                if (operators.Contains(currentText))
                {
                    // element is an operator. Add a condition tag with
                    // attribute 'operator' set to the value of element
                    var condition = new XElement("condition");
                    if (currentText.Equals(Operators.Begin, StringComparison.OrdinalIgnoreCase))
                    {
                        // add nested condition
                        node?.Add(condition);
                        node = condition;
                        condition = new XElement("condition");
                    }
                    if (currentText.Equals(Operators.Or,StringComparison.OrdinalIgnoreCase) || currentText.Equals(Operators.And, StringComparison.OrdinalIgnoreCase))
                    {
                        condition.SetAttributeValue("operator", currentText);
                    }

                    // add condition to current node
                    node?.Add(condition);

                    //set condition tage as current node. next field tag
                    // should be nested inside the condition tag.
                    node = condition;
                }
                else if (currentText.Equals(Operators.End, StringComparison.OrdinalIgnoreCase))
                {
                    node = node?.Parent;
                }
                else if (conditions.Contains(currentText))
                {
                    // element is a condition. Add an expression tag with
                    // attribute 'op' set to the value of the element
                    var expression = new XElement("expression");
                    expression.SetAttributeValue("op", currentText);

                    // append condition to current node
                    node?.Add(expression);

                    // not all conditions need a value
                    if (!noValueNeededCondition.Contains(currentText, StringComparer.OrdinalIgnoreCase))
                    {
                        // increase pointer and add next element as
                        // value to expression
                        i++;
                        expression.Value = queryText[i];
                    }

                    // an expression closes a field tag. The next
                    // element referes to the next level up
                    node = node?.Parent;

                    // if the parentnode is a conition tag we need
                    // to go up one more step
                    if (node?.Name == "condition")
                    {
                        node = node.Parent;
                    }
                }
                // everything that isn't an operator or condition is treated
                // as a field
                else
                {
                    // create a field tag, fill it with element
                    // and add it to the current node
                    var field = new XElement("field")
                    {
                        Value = queryText[i]
                    };
                    node?.Add(field);

                    // if UDf is set we must add an attribute to the field
                    // tag, but only once!
                    if (udf)
                    {
                        field.SetAttributeValue("udf", true);
                        // only the first field can be udf
                        udf = false;
                    }

                    // the field tag is now the current node
                    node = field;
                }
            }

            xml.Add(rootNode);
            return xml;
        }

        /// <summary>
        ///     The Autotask api when querying is limited to returning 500 entities
        ///     or less. This method takes the original <see cref="XDocument" /> query generated by the
        ///     <see cref="Generate(string, bool)" /> method
        ///     and adds the last imported id onto the query.
        /// </summary>
        /// <param name="xml">A previously generated query document</param>
        /// <param name="lastImportedId">
        ///     The id property of the last entity in the Entity Results array. This can be obtained by
        ///     inspected the result returned from the Autotask api Entity Results array.
        /// </param>
        /// <returns></returns>
        public static XDocument Generate(XDocument xml, long lastImportedId)
        {
            var queryNode = xml.Descendants().SingleOrDefault(n => n.Name == "query");

            if (queryNode != null)
            {
                var condition = new XElement("condition");
                condition.SetAttributeValue("operator", "and");

                var idField = new XElement("field")
                {
                    Value = "id"
                };

                var expression = new XElement("expression");
                expression.SetAttributeValue("op", "GreaterThan");
                expression.Value = lastImportedId.ToString();

                idField.Add(expression);
                condition.Add(idField);

                queryNode.Add(idField);
            }

            return xml;
        }
    }
}