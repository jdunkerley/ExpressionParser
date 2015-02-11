using System;
using System.Collections.Generic;
using System.Linq;
using JDunkerley.Parser.Implementation;

namespace JDunkerley.Parser
{
    // ToDo: Change fail reporting so no exceptions
    // ToDo: Support for odd variable names {}
    // ToDo: Operators to be overridable functions
    // ToDo: String concatenation
    // ToDo: Date constants

    /// <summary>
    /// Math Parser Class
    /// Public Static API
    /// </summary>
    public static partial class Parser
    {
        #region Evaluate
        /// <summary>
        /// Utility Function To Evaluate A String
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static object Evaluate(string Expression)
        {
            IComponent comp = GetComponentTree(Expression);
            if (comp == null) return null;
            if (!comp.Constant) throw new System.InvalidOperationException("Expression Contains Variables");
            return comp.Evaluate(null);
        }

        /// <summary>
        /// Utility Function To Evaluate A String
        /// </summary>
        /// <param name="Expression"></param>
        /// <param name="VariableCallback"></param>
        /// <returns></returns>
        public static object Evaluate(string Expression, Func<string, object> VariableCallback)
        {
            IComponent comp = GetComponentTree(Expression);
            if (comp == null) return null;
            return comp.Evaluate(VariableCallback);
        }
        #endregion        
        #region Components
        /// <summary>
        /// Try And Parse Text Into Components
        /// - Splits into Raw Components
        /// - Merge Constants
        /// - Merge Brackets and Functions
        /// </summary>
        /// <param name="Input">Raw Input Text</param>
        /// <param name="Parts">Return Set Of IComponets</param>
        /// <returns>True if parsed successfully, False if fails</returns>
        public static bool GetComponents(string Input, out IComponent[] Parts)
        {
            // Set To Null First Incase We Fail
            Parts = null;

            // Break Into Parts
            string errorCode;
            var cmps = RawParser.Parse(Input, out errorCode);
            if (cmps == null) return false;

            // Merge Constants
            cmps = cmps.MergeConstants();
            if (!cmps.CheckOperators()) return false;

            // Merge Brackets
            cmps = cmps.MergeBrackets();
            if (cmps == null) return false;
            if (cmps.Any(cmp => cmp.Type == ComponentType.BracketClose || cmp.Type == ComponentType.IndexerClose)) return false;

            // Merge Functions
            cmps = cmps.MergeFunctions();
            if (cmps.Any(cmp => cmp.Type == ComponentType.Indexer)) return false; // All Indexers Should Be Functions By Now
            if (!cmps.CheckOperators()) return false;
            
            Parts = cmps.ToArray();
            return true;
        }

        /// <summary>
        /// Get Raw Components
        /// Will All Be Component
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static List<IComponent> GetRawComponents(string Input)
        {
            string errorCode;
            return RawParser.Parse(Input, out errorCode);
        }

        /// <summary>
        /// Build Component Tree
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static IComponent GetComponentTree(string Expression)
        {
            IComponent[] comps;
            if (!GetComponents(Expression, out comps)) return null;
            return GetComponentTree(comps);
        }


        /// <summary>
        /// Build Component Tree
        /// </summary>
        /// <param name="components"></param>
        /// <returns></returns>
        public static IComponent GetComponentTree(IList<IComponent> components)
        {
            return GetComponentTree(components, true);
        }

        /// <summary>
        /// Build Component Tree
        /// </summary>
        /// <param name="components"></param>
        /// <param name="EvaluateConstants">If Constant Expression Pre-Evaluate</param>
        /// <returns></returns>
        public static IComponent GetComponentTree(IList<IComponent> components, bool EvaluateConstants)
        {
            IComponent cmp = components.MakeComponentTree();
            if (cmp == null) return null;

            if (EvaluateConstants)
            {
                if (cmp.Constant)
                    return new Components.EvaluatedBlock(cmp.Evaluate(null));
                cmp.EvaluateConstants();
            }

            return cmp;
        }
        #endregion
        #region Function Dictionary
        private static Dictionary<string, System.Reflection.MethodInfo> functionRegister = new Dictionary<string, System.Reflection.MethodInfo>();

        /// <summary>
        /// Register all the System.Math functions
        /// </summary>
        public static void RegisterMathFunctions()
        {
            Type mathType = typeof(System.Math);
            foreach (var method in mathType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
            {
                var paramSet = method.GetParameters();
                if (paramSet.Where(pt => pt.ParameterType != typeof(double)).Count() > 0) continue;
                SetFunction(method.Name, method);
            }

            SetFunction("Round", typeof(Parser).GetMethod("FuncRound", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public));
            SetFunction("If", typeof(Parser).GetMethod("FuncIf", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public));
        }

        /// <summary>
        /// Function To Do An If Statement
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static object FuncIf(bool cond, object a, object b)
        {
            return cond ? a : b;
        }

        /// <summary>
        /// Function To Expose Round(double, int) to Framework
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double FuncRound(double a, double b)
        {
            return Math.Round(a, (int)b);
        }

        /// <summary>
        /// Get Registered Function
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static System.Reflection.MethodInfo GetFunctions(string Name)
        {
            if (Name == null) return null;
            Name = Name.ToUpper().Trim();
            return (functionRegister.ContainsKey(Name) ? functionRegister[Name] : null);
        }

        /// <summary>
        /// Set Registered Function
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Function"></param>
        public static void SetFunction(string Name, System.Reflection.MethodInfo Function)
        {
            Name = Name.ToUpper().Trim();
            functionRegister[Name] = Function;
        }
        #endregion
    }
}