using System;
using JDunkerley.Parser.Implementation;

namespace JDunkerley.Parser.Components
{
    /// <summary>
    /// Unary Operator Component
    /// </summary>
    public class OperatorUnary : IComponent
    {
        /// <summary>
        /// Make A Unary Component
        /// </summary>
        public OperatorUnary(string Op, IComponent Right)
        {
            this.Op = Op;
            this.Right = Right;
        }

        /// <summary>
        /// Make A Back Unary Component
        /// </summary>
        /// <param name="Left"></param>
        /// <param name="Op"></param>
        public OperatorUnary(IComponent Left, string Op)
        {
            this.Back = true;
            this.Op = Op;
            this.Right = Left;
        }

        /// <summary>
        /// Is A Back Operator
        /// </summary>
        public bool Back { get; private set; }

        /// <summary>
        /// Operator
        /// </summary>
        public string Op { get; private set; }

        /// <summary>
        /// Right Component
        /// </summary>
        public IComponent Right { get; private set; }

        #region IComponent Members
        /// <summary>
        /// Type of Component
        /// </summary>        
        public ComponentType Type
        {
            get { return this.Back ? ComponentType.BackUnary : ComponentType.Unary; }
        }

        /// <summary>
        /// Text Value of Component
        /// </summary>        
        public string Text
        {
            get { return this.Back ? Right.Text + this.Op : this.Op + Right.Text; }
        }

        /// <summary>
        /// Flag If Component Is Constant
        /// </summary>
        public bool Constant
        {
            get { return Right.Constant; }
        }

        /// <summary>
        /// Get Set Of Unique Value
        /// </summary>
        public string[] Variables
        {
            get { return Right.Variables;  }
        }

        /// <summary>
        /// Evaluate The Component
        /// </summary>
        /// <param name="VariableCallBack">Delegate To Get Variable Values</param>
        /// <returns>Evaluated Value</returns>
        public object Evaluate(Func<string, object> VariableCallBack)
        {
            object right = this.Right.Evaluate(VariableCallBack);

            // Boolean Operators
            if (this.Op == "!")
            {
                if (!(right is bool))
                    return null;
                return !(bool)right;
            }

            // Math Operators
            if (this.Op == "-")
            {
                double dblRight = right.ToDouble();
                if (double.IsNaN(dblRight)) 
                    return null;
                return -dblRight;
            }
            else if (this.Op == "+")
            {
                double dblRight = right.ToDouble();
                if (double.IsNaN(dblRight))
                    return null;
                return dblRight;
            }
            else if (this.Op == "%")
            {
                double dblRight = right.ToDouble();
                if (double.IsNaN(dblRight))
                    return null;
                return dblRight / 100;
            }
            else if (this.Op == "‰")
            {
                double dblRight = right.ToDouble();
                if (double.IsNaN(dblRight))
                    return null;
                return dblRight / 1000;
            }

            // Dont Know What To Do  :(
            return null;
        }

        /// <summary>
        /// Evaluate Constant Inputs
        /// </summary>
        public void EvaluateConstants()
        {
            if (this.Right.Constant && !(this.Right is EvaluatedBlock))
                this.Right = new EvaluatedBlock(this.Right.Evaluate(null));
            if (!this.Right.Constant)
                this.Right.EvaluateConstants();
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