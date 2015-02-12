using System.Collections.Generic;
using JDunkerley.Parser;
using JDunkerley.Parser.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class MergeConstantsTests : BaseTests
    {
        /// <summary>
        /// Confirm That {-, 1} Evaluates To -1
        /// </summary>
        [TestMethod]
        public void BasicNegativeSign()
        {
            var comps = new List<IComponent>() { unM, Vl(1) };
            var results = comps.MergeConstants();
            Assert.IsTrue(results.Count == 1);
            results[0].CheckNumberComp(-1, 1e-10, "-1");
        }

        /// <summary>
        /// Confirm That {-, -, 1} Evaluates To -1
        /// </summary>
        [TestMethod]
        public void DuplicateNegativeSign()
        {
            var comps = new List<IComponent>() { unM, unM, Vl(1) };
            var results = comps.MergeConstants();
            Assert.IsTrue(results.Count == 1);
            results[0].CheckNumberComp(1, 1e-10, "--1");
        }

        /// <summary>
        /// Confirm That {+, 1} Evaluates To 1
        /// </summary>
        [TestMethod]
        public void BasicPositiveSign()
        {
            var comps = new List<IComponent>() { unP, Vl(1) };
            var results = comps.MergeConstants();
            Assert.IsTrue(results.Count == 1);
            results[0].CheckNumberComp(1, 1e-10, "+1");
        }

        /// <summary>
        /// Confirm That {+, +, 1} Evaluates To 1
        /// </summary>
        [TestMethod]
        public void DuplicatePositiveSign()
        {
            var comps = new List<IComponent>() { unP, unP, Vl(1) };
            var results = comps.MergeConstants();
            Assert.IsTrue(results.Count == 1);
            results[0].CheckNumberComp(1, 1e-10, "++1");
        }

        /// <summary>
        /// Confirm That {-, +, 1} Evaluates To -1
        /// </summary>
        [TestMethod]
        public void MixedSignPN()
        {
            var comps = new List<IComponent>() { unP, unM, Vl(1) };
            var results = comps.MergeConstants();
            Assert.IsTrue(results.Count == 1);
            results[0].CheckNumberComp(-1, 1e-10, "+-1");
        }

        /// <summary>
        /// Confirm That {-, +, 1} Evaluates To -1
        /// </summary>
        [TestMethod]
        public void MixedSignNP()
        {
            var comps = new List<IComponent>() { unM, unP, Vl(1) };
            var results = comps.MergeConstants();
            Assert.IsTrue(results.Count == 1);
            results[0].CheckNumberComp(-1, 1e-10, "-+1");
        }

        /// <summary>
        /// Confirm That {-, -, +, 1} Evaluates To -1
        /// </summary>
        [TestMethod]
        public void MixedSignNNP()
        {
            var comps = new List<IComponent>() { unM, unM, unP, Vl(1) };
            var results = comps.MergeConstants();
            Assert.IsTrue(results.Count == 1);
            results[0].CheckNumberComp(1, 1e-10, "--+1");
        }

        /// <summary>
        /// Confirm That {1, %} Evaluates To 1%
        /// </summary>
        [TestMethod]
        public void BasicPercent()
        {
            var comps = new List<IComponent>() { Vl(1), Pct };

            var results = comps.MergeConstants();
            Assert.IsTrue(results.Count == 1);
            results[0].CheckNumberComp(0.01, 1e-10, "1%");
        }

        /// <summary>
        /// Confirm That {1, %} Evaluates To 1%
        /// </summary>
        [TestMethod]
        public void BasicMileOperator()
        {
            var comps = new List<IComponent>() { Vl(1), Mle };

            var results = comps.MergeConstants();
            Assert.IsTrue(results.Count == 1);
            results[0].CheckNumberComp(0.001, 1e-10, "1‰");
        }

        /// <summary>
        /// Confirm That {-, 1, %} Evaluates To -1%
        /// </summary>
        [TestMethod]
        public void BasicMinusPercent()
        {
            var comps = new List<IComponent>() { unM, Vl(1), Pct };

            var results = comps.MergeConstants();
            Assert.IsTrue(results.Count == 1);
            results[0].CheckNumberComp(-0.01, 1e-10, "-1%");
        }

    }
}
