using System;
using System.Collections.Generic;
using System.Text;

namespace Autotask.Operations.Constants
{
    using System.Collections.Immutable;

    public class Operators
    {
        public const string And = "And";
        public const string Or = "Or";
        public const string Begin = "Begin";
        public const string End = "End";

        internal static ImmutableHashSet<string> Get => new List<string>
        {
            And,
            Or,
            Begin
        }.ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
    }
}
