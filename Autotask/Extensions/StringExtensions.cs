using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Autotask.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="string"/> class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Uses regex.Split method to split the given string by whitespaces.
        /// This method will not split on doubles quotes"", which allows you
        /// to pass in <see cref="DateTime"/> objects in double quotes "".
        /// </summary>
        public static string[] Split(this string str, RegexOptions options)
        {
            // split on ""
            string regex = @"[ ](?=(?:[^""]*""[^""]*"")*[^""]*$)";

            // define the regex
            Regex myRegex = new Regex(regex, options);


            var splitString = myRegex.Split(str);
            List<string> returnArray = new List<string>();

            // replace "" with empty string
            for (int i = 0; i < splitString.Count(); i++)
            {
                var s = splitString[i];

                s = s.Replace(@"""", string.Empty);

                returnArray.Add(s);
            }

            return returnArray.ToArray();
        }
    }
}
