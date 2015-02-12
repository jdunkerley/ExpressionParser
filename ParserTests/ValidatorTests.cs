using System.Collections.Generic;
using JDunkerley.Parser;
using JDunkerley.Parser.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class ValidatorTests : BaseTests
    {
        /// <summary>
        /// Confirm That {-, 1} Evaluates To -1
        /// </summary>
        [TestMethod]
        public void ValidUnaryTest()
        {
            Assert.IsTrue(new List<IComponent>() { unM, Vl(1) }.CheckOperators(), "-1");
            Assert.IsTrue(new List<IComponent>() { unP, Vl(1) }.CheckOperators(), "+1");
            Assert.IsTrue(new List<IComponent>() { Vl(1), Pct }.CheckOperators(), "1%");
            Assert.IsTrue(new List<IComponent>() { Vl(1), Mle }.CheckOperators(), "1‰");
            Assert.IsTrue(new List<IComponent>() { unM, Vl(1), Pct }.CheckOperators(), "-1%");
            Assert.IsTrue(new List<IComponent>() { unM, unM, Vl(1) }.CheckOperators(), "--1");
            Assert.IsTrue(new List<IComponent>() { unM, brO, Vl(1), brC }.CheckOperators(), "-(1)");
            Assert.IsTrue(new List<IComponent>() { brO, Vl(1), brC, Mle }.CheckOperators(), "(1)‰");
        }
    }
}
