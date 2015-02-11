using System;
using JDunkerley.Parser;
using JDunkerley.Parser.Components;

namespace ParserTests
{
    /// <summary>
    /// Set of static method to help construct test components
    /// </summary>
    public class BaseTests
    {
        /// <summary>
        /// - : Unary Minus
        /// </summary>
        protected static readonly IComponent unM = new Component(ComponentType.Unary, "-");

        /// <summary>
        /// + : Unary Plus
        /// </summary>
        protected static readonly IComponent unP = new Component(ComponentType.Unary, "+");

        /// <summary>
        /// % : BackUnary Percent
        /// </summary>
        protected static readonly IComponent Pct = new Component(ComponentType.BackUnary, "%");

        /// <summary>
        /// ‰ : BackUnary Percent
        /// </summary>
        protected static readonly IComponent Mle = new Component(ComponentType.BackUnary, "‰");

        /// <summary>
        /// + : Binary Plus
        /// </summary>
        protected static readonly IComponent opP = new Component(ComponentType.Binary, "+");

        /// <summary>
        /// - : Binary Minus
        /// </summary>
        protected static readonly IComponent opM = new Component(ComponentType.Binary, "-");

        /// <summary>
        /// * : Binary Multiply
        /// </summary>
        protected static readonly IComponent opT = new Component(ComponentType.Binary, "*");

        /// <summary>
        /// * : Binary Divide
        /// </summary>
        protected static readonly IComponent opD = new Component(ComponentType.Binary, "/");

        /// <summary>
        /// ( : Bracket Open
        /// </summary>
        protected static readonly IComponent brO = new Component(ComponentType.BracketOpen, "(");

        /// <summary>
        /// ) : Bracket Close
        /// </summary>
        protected static readonly IComponent brC = new Component(ComponentType.BracketClose, ")");

        /// <summary>
        /// [ : Indexer Open
        /// </summary>
        protected static readonly IComponent ixO = new Component(ComponentType.IndexerOpen, "[");

        /// <summary>
        /// ] : Indexer Close
        /// </summary>
        protected static readonly IComponent ixC = new Component(ComponentType.IndexerClose, "]");

        /// <summary>
        /// , : Comma
        /// </summary>
        protected static readonly IComponent cma = new Component(ComponentType.Comma, ",");

        /// <summary>
        /// Numerical Value
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        protected static IComponent Vl(double val) { return new NumericalValue(val); }
    }
}
