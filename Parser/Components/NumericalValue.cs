using System;

namespace JDunkerley.Parser.Components
{
    /// <summary>
    /// Constant Value Component
    /// </summary>
    public class NumericalValue : IComponent
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Value"></param>
        public NumericalValue(double Value)
        {
            this.Value = Value;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value { get; private set; }

        #region IComponent Members
        /// <summary>
        /// Type of this Component
        /// </summary>
        public ComponentType Type
        {
            get { return ComponentType.Value; }
        }

        /// <summary>
        /// Text Value Of The Component
        /// </summary>
        public string Text
        {
            get { return Value.ToString(); }
        }

        /// <summary>
        /// Flag If Component Is Constant
        /// </summary>
        public bool Constant
        {
            get { return true; }
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
            return this.Value;
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

        /// <summary>
        /// Convert From A Double To A Component
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static implicit operator NumericalValue(double d)
        {
            return new NumericalValue(d);
        }

        /// <summary>
        /// Convert From A Component To A Double
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static implicit operator double(NumericalValue d)
        {
            return d.Value;
        }
    }
}
