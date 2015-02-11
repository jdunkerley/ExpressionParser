using System;
using System.Collections.Generic;
using System.Linq;
using JDunkerley.Parser.Implementation;

namespace JDunkerley.Parser.Components
{
    /// <summary>
    /// Binary Operator Component
    /// </summary>
    public class OperatorBinary : IComponent
    {
        /// <summary>
        /// Make A Binary Component
        /// </summary>
        public OperatorBinary(IComponent Left, string Op, IComponent Right)
        {
            this.Left = Left;
            this.Op = Op;
            this.Right = Right;
        }

        /// <summary>
        /// Left Component
        /// </summary>
        public IComponent Left { get; private set; }

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
            get { return ComponentType.Binary; }
        }

        /// <summary>
        /// Text Value of Component
        /// </summary>        
        public string Text
        {
            get { return Left.Text + this.Op + Right.Text; }
        }

        /// <summary>
        /// Flag If Component Is Constant
        /// </summary>
        public bool Constant
        {
            get { return Left.Constant && Right.Constant; }
        }

        /// <summary>
        /// Get Set Of Unique Value
        /// </summary>
        public string[] Variables
        {
            get 
            {
                List<string> output = Left.Variables.ToList();
                output.AddRange(Right.Variables.ToList());
                return output.Distinct().ToArray();
            }
        }


        /// <summary>
        /// Evaluate The Component
        /// </summary>
        /// <param name="VariableCallBack">Delegate To Get Variable Values</param>
        /// <returns>Evaluated Value</returns>
        public object Evaluate(Func<string, object> VariableCallBack)
        {
            object left = this.Left.Evaluate(VariableCallBack);
            object right = this.Right.Evaluate(VariableCallBack);

            // Equal Operators
            if (this.Op == "==") return left == null ? right == null : left.Equals(right);
            else if (this.Op == "!=") return left == null ? right != null : !left.Equals(right);

            // Boolean Operators
            if (this.Op == "&&" || this.Op == "||" || this.Op == "|")
            {
                if (!(left is bool) || !(right is bool))
                    return null;
                bool blnLeft = (bool)left;
                bool blnRight = (bool)right;

                switch (this.Op)
                {
                    case "&&": return blnLeft && blnRight;
                    case "||": return blnLeft || blnRight;
                    case "|": return (blnLeft || blnRight) && (blnLeft != blnRight);
                }
            }

            // Comparison Operators
            if (this.Op == ">" || this.Op == ">=" || this.Op == "<" || this.Op == "<=")
            {
                if (left is IComparable)
                {
                    try
                    {
                        int comp = ((IComparable)left).CompareTo(right);
                        switch (this.Op)
                        {
                            case ">": return comp > 0;
                            case ">=": return comp >= 0;
                            case "<": return comp < 0;
                            case "<=": return comp <= 0;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            // Math Operators
            if (this.Op == "+" || this.Op == "-" || this.Op == "/" || this.Op == "*" || this.Op == "^" || this.Op == "%")
            {
                double dblLeft = left.ToDouble();
                if (double.IsNaN(dblLeft)) return null;
                double dblRight = right.ToDouble();
                if (double.IsNaN(dblRight)) return null;
                switch (this.Op)
                {
                    case "+": return dblLeft + dblRight;
                    case "-": return dblLeft - dblRight;
                    case "/": return dblLeft / dblRight;
                    case "*": return dblLeft * dblRight;
                    case "%": return dblLeft % dblRight;
                    case "^": return Math.Pow(dblLeft, dblRight);
                }
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
            if (this.Left.Constant && !(this.Left is EvaluatedBlock))
                this.Left = new EvaluatedBlock(this.Left.Evaluate(null));
            if (!this.Left.Constant)
                this.Left.EvaluateConstants();
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