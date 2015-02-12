using JDunkerley.Parser;
using JDunkerley.Parser.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class OperatorOrderTests : BaseTests
    {
        /// <summary>
        /// Check +1 finds index 0
        /// </summary>
        [TestMethod]
        public void SimpleUnaryP3()
        {
            var test = new IComponent[] { unP, Vl(3) };
            Assert.IsTrue(test.FindLowestPart() == 0); 
        }

        /// <summary>
        /// Check 1+3 finds index 1
        /// </summary>
        [TestMethod]
        public void SimpleBinary1P3()
        {
            var test = new IComponent[] { Vl(1), opP, Vl(3) };
            Assert.IsTrue(test.FindLowestPart() == 1);
        }

        /// <summary>
        /// Check 1+3+5 finds index 3
        /// </summary>
        [TestMethod]
        public void MultiBinary1P3P5()
        {
            var test = new IComponent[] { Vl(1), opP, Vl(3), opP, Vl(5) };
            Assert.IsTrue(test.FindLowestPart() == 3);
        }

        /// <summary>
        /// Check 1+3M5 finds index 1
        /// </summary>
        [TestMethod]
        public void MultiBinary1P3M5()
        {
            var test = new IComponent[] { Vl(1), opP, Vl(3), opM, Vl(5) };
            Assert.IsTrue(test.FindLowestPart() == 1);
        }
    }
}
