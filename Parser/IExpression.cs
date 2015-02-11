using System;

namespace JDunkerley.Parser
{
    /// <summary>
    /// Expanded Interface For Expressions
    /// I.E. Components With Child Components
    /// </summary>
    public interface IExpression : IComponent
    {
        /// <summary>
        /// Function Name
        /// </summary>
        string FunctionName { get; }
        /// <summary>
        /// Set of Child Components
        /// </summary>
        IComponent[] Params { get; }
    }
}
