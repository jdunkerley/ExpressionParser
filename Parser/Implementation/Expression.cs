using System;
using System.Linq;

namespace JDunkerley.Parser.Implementation
{
    /// <summary>
    /// Expression Block (...) or [...]
    /// Doesn't Support Evaluation
    /// </summary>
    internal class Expression : IExpression
    {
        private readonly ComponentType _componentType;
        private readonly string _functionName;
        private readonly IComponent[] _Params;
        
        /// <summary>
        /// Make A Bracket/Indexer/Function Expression Component
        /// </summary>
        public Expression(string FunctionName, IComponent[] Params)
        {
            this._functionName = FunctionName;
            this._componentType = (this.FunctionName == "(" ? ComponentType.Bracket : (this.FunctionName == "[" ? ComponentType.Indexer : ComponentType.Function));
            this._Params = Params;
        }

        /// <summary>
        /// Parameters
        /// </summary>
        public IComponent[] Params { get { return this._Params; } }

        /// <summary>
        /// Function Name
        /// </summary>
        public string FunctionName { get { return this._functionName; } }

        #region IComponent Members
        /// <summary>
        /// Type of Component
        /// </summary>        
        public ComponentType Type
        {
            get { return this._componentType; }
        }

        /// <summary>
        /// Text Value of Component
        /// </summary>        
        public string Text
        {
            get { return (this.Type == ComponentType.Function ? this.FunctionName : "") + (this.Type == ComponentType.Indexer ? "[" : "(") + string.Concat(this.Params.Select(prm => prm.Text)) + (this.Type == ComponentType.Indexer ? "]" : ")"); }
        }

        /// <summary>
        /// Flag If Component Is Constant
        /// </summary>
        public bool Constant
        {
            get { return false; }
        }

        /// <summary>
        /// Get Set Of Unique Value
        /// </summary>
        public string[] Variables
        {
            get { return new string[0]; }
        }

        /// <summary>
        /// Evaluate The Component
        /// </summary>
        /// <param name="VariableCallBack">Delegate To Get Variable Values</param>
        /// <returns>Evaluated Value</returns>
        public object Evaluate(Func<string, object> VariableCallBack)
        {
            return null;
        }

        /// <summary>
        /// Evaluate Constant Inputs
        /// </summary>
        public void EvaluateConstants()
        {
        }
        #endregion
    }
}