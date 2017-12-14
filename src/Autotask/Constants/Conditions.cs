namespace Autotask.Operations.Constants
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;

    public class Conditions
    {
        public const string BeginsWith = "BeginsWith";
        public const string Contains = "Contains";
        public const string EndsWith = "EndsWith";
        public const string EqualTo = "Equals";
        public const string GreaterThan = "GreaterThan";
        public const string GreaterThanOrEquals = "GreaterThanOrEquals";
        public const string IsNotNull = "IsNotNull";
        public const string IsNull = "IsNull";
        public const string IsThisDay = "IsThisDay";
        public const string LessThan = "LessThan";
        public const string LessThanOrEquals = "LessThanOrEquals";
        public const string Like = "Like";
        public const string NotEqualTo = "NotEqual";
        public const string NotLike = "NotLike";
        public const string SoundsLike = "SoundsLike";

        /// <summary>
        /// Returns an Immutable Hash Set of all the available conditions.
        /// </summary>
        internal static ImmutableHashSet<string> Get => new List<string>
        {
            EqualTo,
            NotEqualTo,
            GreaterThan,
            LessThan,
            GreaterThanOrEquals,
            LessThanOrEquals,
            BeginsWith,
            EndsWith,
            Contains,
            IsNotNull,
            IsNull,
            IsThisDay,
            Like,
            NotLike,
            SoundsLike
        }.ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Returns an Immutable Hash Set of all the available conditions that do not require a value present in the query.
        /// </summary>
        internal static ImmutableHashSet<string> GetNoValue => new List<string>
        {
            IsNotNull,
            IsNull,
            IsThisDay
        }.ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
    }
}