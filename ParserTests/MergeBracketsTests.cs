using System.Collections.Generic;
using JDunkerley.Parser;
using JDunkerley.Parser.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class MergeBracketsTests : BaseTests
    {
        /// <summary>
        /// Want To Check That (1+3)
        /// Becomes Single Expression 1+3 as a Bracket
        /// </summary>
        [TestMethod]
        public void SimpleBracket()
        {
            var comps = new List<IComponent>() { brO, Vl(1), opP, Vl(3), brC };
            var results = comps.MergeBrackets();
            Assert.IsTrue(results.Count == 1);
            results[0].CheckBinaryExpresion(ComponentType.Bracket, 1, ComponentType.Binary, "+", 3, "(1+3)");
        }

        /// <summary>
        /// Want To Check That [1+3]
        /// Becomes Single Expression 1+3 as an Indexer
        /// </summary>
        [TestMethod]
        public void SimpleIndexer()
        {
            var comps = new List<IComponent>() { ixO, Vl(1), opP, Vl(3), ixC };
            var results = comps.MergeBrackets();
            Assert.IsTrue(results.Count == 1);
            results[0].CheckBinaryExpresion(ComponentType.Indexer, 1, ComponentType.Binary, "+", 3, "[1+3]");
        }

        /// <summary>
        /// Want To Check That 4*(1+3)
        /// Becomes 4,*,(1+3)
        /// </summary>
        [TestMethod]
        public void MidBracketExpression()
        {
            var comps = new List<IComponent>() { Vl(4), opT, brO, Vl(1), opP, Vl(3), brC };
            var results = comps.MergeBrackets();
            Assert.IsTrue(results.Count == 3);
            results[0].CheckNumberComp(4, 1e-10, "4*(1+3)");
            results[1].CheckTextComp(ComponentType.Binary, "*", "4*(1+3)");
            results[2].CheckBinaryExpresion(ComponentType.Bracket, 1, ComponentType.Binary, "+", 3, "4*(1+3)");
        }

        /// <summary>
        /// Want To Check That (4*(1+3))
        /// Becomes Single Expression 4*(1+3) as a Bracket
        /// </summary>
        [TestMethod]
        public void NestedBrackets()
        {
            var comps = new List<IComponent>() { brO, Vl(4), opT, brO, Vl(1), opP, Vl(3), brC, brC };

            var results = comps.MergeBrackets();
            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].Type == ComponentType.Bracket);
            Assert.IsTrue(results[0] is IExpression);
            var expr = (IExpression)results[0];
            Assert.IsTrue(expr.Params.Length == 3);
            
            expr.Params[0].CheckNumberComp(4, 1e-10, "(4*(1+3))");
            expr.Params[1].CheckTextComp(ComponentType.Binary, "*", "(4*(1+3))");
            expr.Params[2].CheckBinaryExpresion(ComponentType.Bracket, 1, ComponentType.Binary, "+", 3, "(4*(1+3))");
       }

        /// <summary>
        /// Want To Check That (1,3)
        /// Becomes Single Expression 1,3 as a Bracket
        /// </summary>
        [TestMethod()]
        public void SimpleBracketsWithCommas()
        {
            var comps = new List<IComponent>() { brO, Vl(1), cma, Vl(3), brC };
            var results = comps.MergeBrackets();
            Assert.IsTrue(results.Count == 1);
            results[0].CheckBinaryExpresion(ComponentType.Bracket, 1, ComponentType.Comma, ",", 3, "(1,3)");
        }

        /// <summary>
        /// Want To Check That (1*2,3-1)
        /// Becomes Single Expression (1*2),(3-1) as a Bracket
        /// </summary>
        [TestMethod()]
        public void ExpressionParamsBracketsWithCommas()
        {
            var comps = new List<IComponent>() { brO, Vl(1), opT, Vl(2), cma, Vl(3), opM, Vl(1), brC };
            var results = comps.MergeBrackets();
            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].Type == ComponentType.Bracket);
            Assert.IsTrue(results[0] is IExpression);
            var expr = (IExpression)results[0];
            Assert.IsTrue(expr.Params.Length == 3);

            expr.Params[0].CheckBinaryExpresion(ComponentType.Bracket, 1, ComponentType.Binary, "*", 2, "(1*2,3-1)");

            Assert.IsTrue(expr.Params[1].Type == ComponentType.Comma);
            Assert.IsTrue(expr.Params[1].Text == ",");

            expr.Params[2].CheckBinaryExpresion(ComponentType.Bracket, 3, ComponentType.Binary, "-", 1, "(1*2,3-1)");
        }
    }
}
