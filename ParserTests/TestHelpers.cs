using JDunkerley.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JDunkerley.Parser.Components;

namespace ParserTests
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class TestHelpers
    {
        /// <summary>
        /// Helper Function To Check Evaluated To A Constant Number
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="expected"></param>
        /// <param name="tol"></param>
        /// <param name="ntxt"></param>
        public static void CheckNumberComp(this IComponent comp, double expected, double tol, string ntxt)
        {
            Assert.IsNotNull(comp, ntxt + ": NULL Component");
            Assert.IsTrue(comp.Type == ComponentType.Value, ntxt + ": Component should be a Value, was");
            Assert.IsTrue(comp.Constant == true, ntxt + ": Component should be constant");

            object eval = comp.Evaluate(null);
            Assert.IsTrue(eval is double, ntxt + ": Component should evaluate to a double");
            Assert.IsTrue(Math.Abs((double)eval - expected) < tol, ntxt + ": Component should evaluate to " + expected);
        }

        /// <summary>
        /// Compare A Component's Type and Text
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="type"></param>
        /// <param name="op"></param>
        /// <param name="txt"></param>
        public static void CheckTextComp(this IComponent comp, ComponentType type, string op, string txt)        
        {
            Assert.IsNotNull(comp, txt + ": NULL Component");
            Assert.IsTrue(comp.Type == type, txt + ": ComponentType MisMatch (Expected: " + type + ", Actual: " + comp.Type + ")");
            Assert.IsTrue(comp.Text == op, txt + ": ComponentText MisMatch (" + txt + ")");
        }

        /// <summary>
        /// Helper Function To Check Evaluated To (left, op, right) as and Expression
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="left"></param>
        /// <param name="opType"></param>
        /// <param name="op"></param>
        /// <param name="right"></param>
        /// <param name="txt"></param>
        public static void CheckBinaryExpresion(this IComponent comp, ComponentType exType, double left, ComponentType opType, string op, double right, string txt)
        {
            Assert.IsNotNull(comp, txt + ": NULL Component");
            Assert.IsTrue(comp.Type == exType, txt + ": ComponentType MisMatch (Expected: " + exType + ", Actual: " + comp.Type + ")");

            Assert.IsTrue(comp is IExpression, txt + ": Expected to get an IExpression");
            var expr = (IExpression)comp;
            Assert.IsTrue(expr.Params.Length == 3);

            expr.Params[0].CheckNumberComp(left, 1e-10, txt);
            expr.Params[1].CheckTextComp(opType, op, txt);
            expr.Params[2].CheckNumberComp(right, 1e-10, txt);
        }
    }
}
