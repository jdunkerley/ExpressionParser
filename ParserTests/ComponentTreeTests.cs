using System;
using System.Collections.Generic;
using JDunkerley.Parser;
using JDunkerley.Parser.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    /// <summary>
    /// Test The Component Tree Methods
    /// </summary>
    [TestClass]
    public class ComponentTreeTests : BaseTests
    {
        /// <summary>
        /// Check +1 Converts To A Constant Unary Evaluating To 1 
        /// (Not Really Needed As MergeOperators Should Have Eliminated Unary+)
        /// </summary>
        [TestMethod]
        public void TestUnaryP1()
        {
            var test = new IComponent[] { unP, Vl(1) };
            var output = test.MakeComponentTree();
            Assert.IsTrue(output.Type == ComponentType.Unary);
            Assert.IsTrue(output.Constant);
            Assert.IsTrue(output.Evaluate(null) is double);
            Assert.IsTrue(Math.Abs((double)output.Evaluate(null) - 1.0) < 1e-10);
        }

        /// <summary>
        /// Check -1 Converts To A Constant Unary Evaluating To -1
        /// </summary>
        [TestMethod]
        public void TestUnaryM1()
        {
            var test = new IComponent[] { unM, Vl(1) };
            var output = test.MakeComponentTree();
            Assert.IsTrue(output.Type == ComponentType.Unary);
            Assert.IsTrue(output.Constant);
            Assert.IsTrue(output.Evaluate(null) is double);
            Assert.IsTrue(Math.Abs((double)output.Evaluate(null) - -1.0) < 1e-10);
        }

        /// <summary>
        /// Check 1% Converts To A Constant BackUnary Evaluating To 0.01
        /// </summary>
        [TestMethod]
        public void TestBackUnary1p()
        {
            var test = new IComponent[] { Vl(1), Pct };
            var output = test.MakeComponentTree();
            Assert.IsTrue(output.Type == ComponentType.BackUnary);
            Assert.IsTrue(output.Constant);
            Assert.IsTrue(output.Evaluate(null) is double);
            Assert.IsTrue(Math.Abs((double)output.Evaluate(null) - 0.01) < 1e-10);
        }

        /// <summary>
        /// Check 3+1 Converts To A Constant Binary Evaluating To 4 
        /// </summary>
        [TestMethod]
        public void TestBinary3P1()
        {
            var test = new IComponent[] { Vl(3), opP, Vl(1) };
            var output = test.MakeComponentTree();
            Assert.IsTrue(output.Type == ComponentType.Binary);
            Assert.IsTrue(output.Constant);
            Assert.IsTrue(output.Evaluate(null) is double);
            Assert.IsTrue(Math.Abs((double)output.Evaluate(null) - 4.0) < 1e-10);
        }

        /// <summary>
        /// Check 3-1 Converts To A Constant Binary Evaluating To 2
        /// </summary>
        [TestMethod]
        public void TestBinary3M1()
        {
            var test = new IComponent[] { Vl(3), opM, Vl(1) };
            var output = test.MakeComponentTree();
            Assert.IsTrue(output.Type == ComponentType.Binary);
            Assert.IsTrue(output.Constant);
            Assert.IsTrue(output.Evaluate(null) is double);
            Assert.IsTrue(Math.Abs((double)output.Evaluate(null) - 2.0) < 1e-10);
        }

        /// <summary>
        /// Check 24%/0.01 Converts To A Constant Binary Evaluating To 24
        /// </summary>
        [TestMethod]
        public void TestBinary24pD1p()
        {
            var test = new IComponent[] { Vl(24), Pct, opD, Vl(0.01) };
            var output = test.MakeComponentTree();
            Assert.IsTrue(output.Type == ComponentType.Binary);
            Assert.IsTrue(output.Constant);
            Assert.IsTrue(output.Evaluate(null) is double);
            Assert.IsTrue(Math.Abs((double)output.Evaluate(null) - 24.0) < 1e-10);
        }

        /// <summary>
        /// Check 3*(4-5) Converts To A Constant Binary Evaluating To -3
        /// </summary>
        [TestMethod]
        public void TestBinary3Tb4M8b()
        {
            var test = new List<IComponent>() { Vl(3), opT, brO, Vl(4), opM, Vl(5), brC };
            test = test.MergeBrackets();
            var output = test.MakeComponentTree();
            Assert.IsTrue(output.Type == ComponentType.Binary);
            Assert.IsTrue(output.Constant);
            Assert.IsTrue(output.Evaluate(null) is double);
            Assert.IsTrue(Math.Abs((double)output.Evaluate(null) - -3) < 1e-10);
        }
    }
}
