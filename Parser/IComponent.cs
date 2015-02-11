using System;

namespace JDunkerley.Parser
{
    /// <summary>
    /// Raw Interface Of A Component
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Type of Component
        /// </summary>
        ComponentType Type { get; }
        /// <summary>
        /// Text Value of Component
        /// </summary>
        string Text { get; }
        /// <summary>
        /// Flag If Component Is Constant
        /// </summary>
        bool Constant { get; }
        /// <summary>
        /// List Of All Variables Used By Component
        /// </summary>
        string[] Variables { get; }
        /// <summary>
        /// Evaluation Component And Return Value
        /// </summary>
        /// <param name="VariableCallBack"></param>
        /// <returns></returns>
        object Evaluate(Func<string, object> VariableCallBack);
        /// <summary>
        /// Evaluates Constant Inputs
        /// </summary>
        void EvaluateConstants();
    }
}