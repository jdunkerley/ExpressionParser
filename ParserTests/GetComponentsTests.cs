using JDunkerley.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    [TestClass]
    public class GetComponentsTests 
    {
        /// <summary>
        /// Confirm -1+3 Parsed Correctly
        /// </summary>
        [TestMethod]
        public void SimpleAdditionTest()
        {
            IComponent[] comps;
            Assert.IsTrue(Parser.GetComponents("-1+3", out comps));
            Assert.IsTrue(comps.Length == 3);
            comps[0].CheckNumberComp(-1, 1e-10, "-1+3");
            comps[1].CheckTextComp(ComponentType.Binary, "+", "-1+3");
            comps[2].CheckNumberComp(3, 1e-10, "-1+3");
        }


    }
}
