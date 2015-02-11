using System;

namespace JDunkerley.Parser.Components
{
    /// <summary>
    /// Variable Component
    /// </summary>
    public class Variable : IComponent
    {
        private string varName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Text">Text Value</param>
        public Variable(string Text)
        {
            this.Text = Text;
            this.varName = this.Text.StartsWith("{") ? this.Text.Substring(1, this.Text.Length - 2) : this.Text;
        }

        #region IComponent Members
        /// <summary>
        /// Type of this Component
        /// </summary>
        public ComponentType Type
        {
            get { return ComponentType.Variable; }
        }

        /// <summary>
        /// Text Value Of The Component
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Flag If Component Is Constant
        /// </summary>
        public bool Constant
        {
            get { return false; }
        }

        /// <summary>
        /// List Of All Variables Used By Component
        /// </summary>
        public string[] Variables
        {
            get { return new string[] { this.varName }; }
        }

        /// <summary>
        /// Evaluation Component And Return Value
        /// </summary>
        /// <param name="VariableCallBack"></param>
        /// <returns></returns>
        public object Evaluate(Func<string, object> VariableCallBack)
        {
            return VariableCallBack(this.varName);
        }

        /// <summary>
        /// Evaluate Constant Inputs
        /// </summary>
        public void EvaluateConstants()
        {
        }
        #endregion

        /// <summary>
        /// Return Value as ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Text;
        }
    }
}
