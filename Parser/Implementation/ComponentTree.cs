using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JDunkerley.Parser.Implementation
{
    /// <summary>
    /// Methods Converting A List Of Components To A Single Component
    /// </summary>
    public static class ComponentTree
    {
        /// <summary>
        /// Converts a List of components into a single component
        /// </summary>
        /// <param name="components"></param>
        /// <returns></returns>
        public static IComponent MakeComponentTree(this IList<IComponent> components)
        {
            if (components == null) return null;
            return components.MakeComponentTree(0, components.Count);
        }

        /// <summary>
        /// Converts a List of components into a single component starting at specified index
        /// </summary>
        /// <param name="components"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IComponent MakeComponentTree(this IList<IComponent> components, int start, int count)
        {
            if (components == null || count == 0) return null;

            int idx = components.FindLowestPart(start, count);
            if (idx == -1) return null;

            var pivot = components[idx];
            switch (pivot.Type)
            {
                case ComponentType.BackUnary:
                    return components.MakeBackUnary(start, count);
                case ComponentType.Binary:
                    return components.MakeBinary(start, count, idx);
                case ComponentType.Bracket:
                case ComponentType.Function:
                    return components[idx].MakeFunction();
                case ComponentType.Unary:
                    return components.MakeUnary(0, count);
                case ComponentType.Value:
                    return pivot;
                case ComponentType.Variable:
                    return new Components.Variable(pivot.Text);
                case ComponentType.BracketOpen:
                case ComponentType.BracketClose:
                case ComponentType.Comma:
                case ComponentType.Indexer:
                case ComponentType.IndexerOpen:
                case ComponentType.IndexerClose:
                case ComponentType.Operator:
                    return null;
            }

            throw new ArgumentOutOfRangeException("Unsupported Type: Not Sure How This Is Possible!");
        }

        private static IComponent MakeBackUnary(this IList<IComponent> components, int start, int count)
        {
            var input = components.MakeComponentTree(start, count - 1);
            if (input == null) return null;
            return new Components.OperatorUnary(input, components[start + count - 1].Text);
        }

        private static IComponent MakeUnary(this IList<IComponent> components, int start, int count)
        {
            var input = components.MakeComponentTree(start + 1, count - 1);
            if (input == null) return null;
            return new Components.OperatorUnary(components[start].Text, input);
        }

        private static IComponent MakeBinary(this IList<IComponent> components, int start, int count, int idx)
        {
            var leftin = components.MakeComponentTree(start, idx - start);
            if (leftin == null) return null;

            var rightin = components.MakeComponentTree(idx + 1, count - (idx - start) - 1);
            if (rightin == null) return null;

            return new Components.OperatorBinary(leftin, components[idx].Text, rightin);
        }

        private static IComponent MakeFunction(this IComponent component)
        {
            var expr = component as IExpression;
            if (expr == null) return null;

            var comps = new List<IComponent>();
            int start = 0;
            for (int i = 0; i < expr.Params.Length; i++)
            {
                if (expr.Params[i].Type == ComponentType.Comma || i == expr.Params.Length - 1)
                {
                    var tmp = expr.Params.MakeComponentTree(start, i - start + 1);
                    if (tmp == null) return null;
                    comps.Add(tmp);
                    start = i + 1;
                }
            }

            if (component.Type == ComponentType.Bracket)
                return new Components.Function("(", null, comps.ToArray());
            else
                return new Components.Function(expr.FunctionName, Parser.GetFunctions(expr.FunctionName), comps.ToArray());
        }
    }
}
