using System.Collections.Generic;

namespace JDunkerley.Parser.Implementation
{
    /// <summary>
    /// General Utility Functions
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// Given An Open Bracket, What Close Character
        /// </summary>
        /// <param name="closer"></param>
        /// <returns></returns>
        internal static char OpenBracket(char closer)
        {
            switch (closer)
            {
                case '{': return '}';
                case ')': return '(';
                case ']': return '[';
            }
            return '\0';
        }

        /// <summary>
        /// For A +/- Is A Unary Operator
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool isUnary(this ComponentType type)
        {
            switch (type)
            {
                case ComponentType.Comma:
                case ComponentType.Bracket:
                case ComponentType.BracketOpen:
                case ComponentType.Indexer:
                case ComponentType.IndexerOpen:
                case ComponentType.Unary:
                case ComponentType.BackUnary:
                case ComponentType.Operator:
                case ComponentType.Binary:
                    return true;
                default: 
                    return false;
            }
        }

        /// <summary>
        /// For A % Is A BackUnary Operator
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool isBackUnary(this ComponentType type)
        {
            switch (type)
            {
                case ComponentType.Comma:
                case ComponentType.Bracket:
                case ComponentType.BracketClose:
                case ComponentType.Indexer:
                case ComponentType.IndexerClose:
                case ComponentType.Unary:
                case ComponentType.BackUnary:
                case ComponentType.Operator:
                case ComponentType.Binary:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Index To First One Or -1 If Not Found
        /// </summary>
        /// <param name="components"></param>
        /// <param name="Type"></param>
        /// <param name="Text"></param>
        /// <returns></returns>
        internal static int FirstIndex(this IEnumerable<IComponent> components, ComponentType Type, string Text)
        {
            int i = 0;
            foreach (var item in components)
            {
                if (item.Type == Type && (Text == null || item.Text == Text))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Parse An Object Into A Double
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        internal static double ToDouble(this object src)
        {
            if (src is double) return (double)src;
            if (src is short) return (double)((short)src);
            if (src is ushort) return (double)((ushort)src);
            if (src is int) return (double)((int)src);
            if (src is uint) return (double)((uint)src);
            if (src is long) return (double)((long)src);
            if (src is ulong) return (double)((ulong)src);
            if (src is float) return (double)((float)src);
            if (src is decimal) return (double)((decimal)src);

            double strOutput = double.NaN;
            if (src is string && double.TryParse((string)src, out strOutput))
                return strOutput;

            return double.NaN;
        }
    }
}
