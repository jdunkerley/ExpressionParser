using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JDunkerley.Parser.Implementation
{
    /// <summary>
    /// Set of Merge Functions Which Can Be Applied To Set Of Raw Components
    /// - Unary
    /// - BackUnary
    /// </summary>
    public static class MergeConstantsWithOperators
    {
        private static void _AddCur(IComponent Cur, ref int ii, List<IComponent> output)
        {
            output.Add(Cur);
            ii++;
        }

        private static bool _UnaryOpeartorSkipDupe(IComponent Cur, IComponent Next, ref int ii)
        {
            if (Next.Type != ComponentType.Unary || Next.Text != Cur.Text)
                return false;

            ii += 2;
            return true;
        }

        private static void _UnaryOperatorMinus(IComponent Cur, List<IComponent> Comps, ref int ii, List<IComponent> output)
        {
            var Next = (ii + 1) < Comps.Count ? Comps[ii + 1] : null;
            int jj = 1;
            while (Next.Type == ComponentType.Unary && Next.Text == "+")
            {
                jj++;
                Next = (ii + jj) < Comps.Count ? Comps[ii + jj] : null;
            }

            if (Next is Components.NumericalValue)
            {
                // Create A New Negative Value And Then Skip (jj + 1)
                output.Add(new Components.NumericalValue(-((Components.NumericalValue)Next).Value));
                ii += jj + 1;
                return;
            }

            // Cant Handle This So Back To Default
            _AddCur(Cur, ref ii, output);
        }

        private static bool _BackUnaryOpeartorPct(IComponent Cur, IComponent Prev, ref int ii, List<IComponent> output)
        {
            if (!(Prev is Components.NumericalValue))
                return false;

            double val = ((Components.NumericalValue)Prev).Value;
            if (Cur.Text == "%") val = val / 100.0;
            else if (Cur.Text == "‰") val = val / 1000.0;

            output[output.Count - 1] = new Components.NumericalValue(val);
            ii++;
            return true;
        }

        /// <summary>
        /// Unary Operators: +, -, !, %, ‰
        /// Removes + Unary
        /// Removes -- 
        /// Removes !!
        /// Merges -, %, ‰ Into Constants
        /// </summary>
        /// <param name="Comps"></param>
        /// <param name="HandleUnary">Do Unary Operators</param>
        /// <param name="HandleBack">Do Back Unary Operators</param>
        /// <returns></returns>
        public static List<IComponent> MergeConstants(this List<IComponent> Comps, bool HandleUnary = true, bool HandleBack = true)
        {
            var output = new List<IComponent>();
            int ii = 0;
            while (ii < Comps.Count)
            {
                var cur = Comps[ii];

                switch (cur.Type)
                {
                    case ComponentType.Unary:
                        _MergeUnaryOperator(Comps, cur, ref ii, output);
                        continue;
                    case ComponentType.BackUnary:
                        _MergeBackUnaryOperator(cur, ref ii, output);
                        continue;
                }

                _AddCur(cur, ref ii, output);
            }
            return output;
        }

        private static void _MergeUnaryOperator(List<IComponent> Comps, IComponent cur, ref int ii, List<IComponent> output)
        {
            // Unary Operator So Try And Handle
            var next = (ii + 1) < Comps.Count ? Comps[ii + 1] : null;
            switch (cur.Text)
            {
                case "+": // Remove + Unary Operator
                    ii++;
                    break;
                case "-":
                    if (!_UnaryOpeartorSkipDupe(cur, next, ref ii))
                        _UnaryOperatorMinus(cur, Comps, ref ii, output);
                    break;
                case "!":
                    if (!_UnaryOpeartorSkipDupe(cur, next, ref ii))
                        _AddCur(cur, ref ii, output);
                    break;
                default: // Default Handler
                    _AddCur(cur, ref ii, output);
                    break;
            }
        }

        private static void _MergeBackUnaryOperator(IComponent cur, ref int ii, List<IComponent> output)
        {
            var prev = output.Count > 0 ? output[output.Count - 1] : null;
            switch (cur.Text)
            {
                case "%":
                case "‰":
                    if (!_BackUnaryOpeartorPct(cur, prev, ref ii, output))
                        _AddCur(cur, ref ii, output);
                    break;
                default:
                    _AddCur(cur, ref ii, output);
                    break;
            }
        }

        /// <summary>
        /// Unary Operators: +, -, !
        /// Removes + Unary
        /// Removes -- 
        /// Removes !!
        /// Merges - Into Constants
        /// </summary>
        /// <param name="Comps"></param>
        /// <returns></returns>
        public static List<IComponent> MergeConstantsUnary(this List<IComponent> Comps)
        {
            return Comps.MergeConstants(true, false);
        }

        /// <summary>
        /// Merges %/‰ Back Into Constant
        /// </summary>
        /// <param name="Comps"></param>
        /// <returns></returns>
        public static List<IComponent> MergeConstantsBackUnary(this List<IComponent> Comps)
        {
            return Comps.MergeConstants(false, true);
        }
    }
}
