using System;

namespace JDunkerley.Parser.Implementation
{
    /// <summary>
    /// Raw Component Component Of The Expression
    /// Used By GetComponents, Cannot Be Evaluated
    /// </summary>
    internal class Component : IComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Text"></param>
        public Component(ComponentType Type, string Text)
        {
            this.Type = Type;
            this.Text = Text;
        }

        /// <summary>
        /// Type of Component
        /// </summary>
        public ComponentType Type { get; private set; }

        /// <summary>
        /// Text Value of Component
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
            get { return new string[0]; }
        }

        /// <summary>
        /// Evaluation Component And Return Value
        /// </summary>
        /// <param name="VariableCallBack"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Override ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0}\t{1}", Type, Text);
        }
    }
}