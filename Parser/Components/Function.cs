using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JDunkerley.Parser.Components
{
    /// <summary>
    /// Function Component
    /// </summary>
    public class Function : IComponent
    {
        /// <summary>
        /// Make A Function Component
        /// </summary>
        public Function(string FnName, MethodInfo method, params IComponent[] Params)
        {
            this.FnName = FnName;
            this.Method = method;
            this.Params = Params;
        }

        /// <summary>
        /// Function Name
        /// </summary>
        public string FnName { get; private set; }

        /// <summary>
        /// Method
        /// </summary>
        public MethodInfo Method { get; private set; }

        /// <summary>
        /// Parameters
        /// </summary>
        public IComponent[] Params { get; private set; }

        #region IComponent Members
        /// <summary>
        /// Type of Component
        /// </summary>        
        public ComponentType Type
        {
            get { return ComponentType.Function; }
        }

        /// <summary>
        /// Text Value of Component
        /// </summary>        
        public string Text
        {
            get { return (this.FnName == "(" ? "" : this.FnName) + "(" + string.Join(",", this.Params.Select(prm=>prm.Text).ToArray()) + ")"; }
        }

        /// <summary>
        /// Flag If Component Is Constant
        /// </summary>
        public bool Constant
        {
            get
            {
                foreach (var item in this.Params)
                    if (!item.Constant)
                        return false;
                return true;
            }
        }

        /// <summary>
        /// Get Set Of Unique Value
        /// </summary>
        public string[] Variables
        {
            get
            {
                List<string> output = new List<string>();
                foreach (var item in this.Params)
                    output.AddRange(item.Variables);
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
            try
            {   
                List<object> paramVals = new List<object>();
                foreach (var item in this.Params)
                    paramVals.Add(item.Evaluate(VariableCallBack));
                if (this.FnName == "(")
                    return paramVals[0];
                return this.Method.Invoke(null, paramVals.ToArray());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Evaluate Constant Inputs
        /// </summary>
        public void EvaluateConstants()
        {
            for (int i = 0; i < this.Params.Length; i++)
            {
                if (this.Params[i].Constant && !(this.Params[i] is EvaluatedBlock))
                    this.Params[i] = new EvaluatedBlock(this.Params[i].Evaluate(null));
                if (!this.Params[i].Constant)
                    this.Params[i].EvaluateConstants();
            }
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