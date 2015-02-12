using System.Collections.Generic;

namespace JDunkerley.Parser.Implementation
{
    /// <summary>
    /// Based on http://en.wikipedia.org/wiki/Order_of_operations
    /// </summary>
    public static class OrderOfOperation
    {
        /// <summary>
        /// Binary Operator Order
        /// In Increasing Priority Order
        /// </summary>
        public  static readonly string[] BinaryOrder = new string[] { "|", "||", "&&", "==", "!=", "<", "<=", ">", ">=", "+", "-", "*", "^", "/", "%" };

        /// <summary>
        /// Searches through components to find the component to be evaluated last, as this will be top of tree
        /// </summary>
        /// <param name="components"></param>
        /// <returns></returns>
        public static int FindLowestPart(this IList<IComponent> components)
        {
            return components.FindLowestPart(0, components.Count);
        }

        /// <summary>
        /// Searches through components to find the component to be evaluated last, as this will be top of tree
        /// Starts at a specific start and scans set
        /// </summary>
        /// <param name="components"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int FindLowestPart(this IList<IComponent> components, int start, int count)
        {
            int idx;

            // Try Binary
            if ((idx = _FindLowestBinary(components, start, count)) != -1)
                return idx;

            // First Is Unary
            if (components[start].Type == ComponentType.Unary)
                return start;

            // Last Is Back Unary
            if (components[start + count -1].Type == ComponentType.BackUnary)
                return start + count -1;

            // If Single Then Return 0
            if (count == 1)
                return start;

            // Fail :(
            return -1;
        }

        /// <summary>
        /// Searches through components to find the binary operator to be evaluated last, as this will be top of tree
        /// e.g. 1+3*2 would find index of + (1)
        /// e.g. 1+3+2 would find index of second + (3)
        /// </summary>
        /// <param name="components"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static int _FindLowestBinary(IList<IComponent> components, int start, int count)
        {
            foreach (string op in BinaryOrder)
            {
                int idx = components.LastIndex(ComponentType.Binary, op, start, count);
                if (idx != -1) return idx;
            }

            return -1;
        }

        /// <summary>
        /// Index To Last One Or -1 If Not Found
        /// </summary>
        /// <param name="components"></param>
        /// <param name="Type"></param>
        /// <param name="Text"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static int LastIndex(this IList<IComponent> components, ComponentType Type, string Text, int start, int count)
        {
            int i = (start + count - 1);
            while (i >= start)
            {
                var item = components[i];
                if (item.Type == Type && (Text == null || item.Text == Text))
                    return i;
                i--;
            }
            return -1;
        }
    }
}