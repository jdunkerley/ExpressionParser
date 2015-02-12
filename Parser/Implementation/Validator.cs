using System.Collections.Generic;

namespace JDunkerley.Parser.Implementation
{
    /// <summary>
    /// Set of extension method that converts a set of Raw Components into typed components
    /// </summary>
    public static class Validator
    {
        #region Check Operators
        /// <summary>
        /// Checks To See If Operators Are Valid
        /// </summary>
        /// <param name="Comps"></param>
        /// <returns>Index To First Invalid Operator Or -1</returns>
        public static bool CheckOperators(this List<IComponent> Comps)
        {
            for (int i = 0; i < Comps.Count; i++)
            {
                switch (Comps[i].Type)
                {
                    case ComponentType.Unary:
                        if (!CheckUnaryOperator(Comps, i))
                            return false;
                        break;
                    case ComponentType.BackUnary:
                        if (!CheckBackUnaryOperator(Comps, i))
                            return false;
                        break;
                    case ComponentType.Binary:
                        if (!CheckBinaryOperator(Comps, i))
                            return false;
                        break;
                }
            }

            return true;
        }

        /// <summary>
        /// Check To See That Binary Operators Have 2 Inputs
        /// </summary>
        /// <param name="Comps"></param>
        /// <param name="ii"></param>
        /// <returns></returns>
        public static bool CheckBinaryOperator(List<IComponent> Comps, int ii)
        {
            return CheckBackUnaryOperator(Comps, ii) && CheckUnaryOperator(Comps, ii);
        }

        /// <summary>
        /// Check To Make Sure Input To An Unary Operator Is Valid
        /// </summary>
        /// <param name="Comps"></param>
        /// <param name="ii"></param>
        /// <returns></returns>
        public static bool CheckUnaryOperator(List<IComponent> Comps, int ii)
        {
            if (ii + 1 >= Comps.Count) 
                return false;

            switch (Comps[ii + 1].Type)
            {
                case ComponentType.BackUnary: return false;
                case ComponentType.Binary: return false;
                case ComponentType.Bracket: return true;
                case ComponentType.BracketOpen: return true;
                case ComponentType.BracketClose: return false;
                case ComponentType.Comma: return false;
                case ComponentType.Function: return true;
                case ComponentType.Indexer: return false;
                case ComponentType.IndexerOpen: return false;
                case ComponentType.IndexerClose: return false;
                case ComponentType.Operator: return false;
                case ComponentType.Unary: return true;
                case ComponentType.Value: return true;
                case ComponentType.Variable: return true;
                default: return false;
            }
        }

        /// <summary>
        /// Check To Make Sure Input To A Back Unary Operator Is Valid
        /// </summary>
        /// <param name="Comps"></param>
        /// <param name="ii"></param>
        /// <returns></returns>
        public static bool CheckBackUnaryOperator(List<IComponent> Comps, int ii)
        {
            if (ii == 0) 
                return false;

            switch (Comps[ii - 1].Type)
            {
                case ComponentType.BackUnary: return true;
                case ComponentType.Binary: return false;
                case ComponentType.Bracket: return true;
                case ComponentType.BracketOpen: return false;
                case ComponentType.BracketClose: return true;
                case ComponentType.Comma: return false;
                case ComponentType.Function: return true;
                case ComponentType.Indexer: return true;
                case ComponentType.IndexerOpen: return false;
                case ComponentType.IndexerClose: return true;
                case ComponentType.Operator: return false;
                case ComponentType.Unary: return false;
                case ComponentType.Value: return true;
                case ComponentType.Variable: return true;
                default: return false;
            }
        }
        #endregion
    }
}
