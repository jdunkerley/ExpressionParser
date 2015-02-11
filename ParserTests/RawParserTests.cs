using System;
using System.Linq;
using JDunkerley.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTests
{
    /// <summary>
    ///This is a test class for ParserTest and is intended
    ///to contain all ParserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RawParserTests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// For Pure Number Tests How Many Iterations
        /// </summary>
        public const int TestIterations = 100;

        #region Positive Constants
        /// <summary>
        /// Check Positive Integers Are Parsed Correctly
        /// </summary>
        [TestMethod()]
        public void PositiveIntegerTests()
        {
            var rand = new System.Random();

            for (int i = 0; i < TestIterations; i++)
            {
                double n = Math.Round((rand.NextDouble()) * 10000000000, 0);
                string ntxt = n.ToString("0");

                var comps = JDunkerley.Parser.Parser.GetRawComponents(ntxt);
                Assert.IsTrue(comps.Count == 1, "Number of components incorrect for " + ntxt);
                comps[0].CheckNumberComp(n, 1e-8, ntxt);
            }
        }

        /// <summary>
        /// Check Positive Reals Are Parsed Correctly
        /// </summary>
        [TestMethod()]
        public void PositiveDoubleTests()
        {
            var rand = new System.Random();

            for (int i = 0; i < TestIterations; i++)
            {
                double n = Math.Round((rand.NextDouble()) * 10000000000, 8);
                string ntxt = Math.Floor(n).ToString("0");
                ntxt = ntxt + "." + (1e8 * (n % 1)).ToString("00000000");
                ntxt = ntxt.TrimEnd('0').TrimEnd('.');

                var comps = Parser.GetRawComponents(ntxt);
                Assert.IsTrue(comps.Count == 1, "Number of components incorrect for " + ntxt);
                comps[0].CheckNumberComp(n, 1e-8, ntxt);
            }
        }

        /// <summary>
        /// Check Positive Reals With A Pct Op Are Parsed Correctly
        /// </summary>
        [TestMethod()]
        public void PositiveDoublePctTests()
        {
            var rand = new System.Random();

            for (int i = 0; i < TestIterations; i++)
            {
                double n = Math.Round((rand.NextDouble()) * 10000000000, 8);
                string ntxt = Math.Floor(n).ToString("0");
                ntxt = ntxt + "." + (1e8 * (n % 1)).ToString("00000000");
                ntxt = ntxt.TrimEnd('0').TrimEnd('.');
                ntxt = ntxt + "%";

                var comps = Parser.GetRawComponents(ntxt);
                Assert.IsTrue(comps.Count == 2, "Number of components incorrect for " + ntxt);
                comps[0].CheckNumberComp(n, 1e-8, ntxt);
                comps[1].CheckTextComp(ComponentType.BackUnary, "%", ntxt);
            }
        }

        /// <summary>
        /// Check Positive Reals With A Mile Op Are Parsed Correctly
        /// </summary>
        [TestMethod()]
        public void PositiveDoubleMileTests()
        {
            var rand = new System.Random();

            for (int i = 0; i < TestIterations; i++)
            {
                double n = Math.Round((rand.NextDouble()) * 10000000000, 8);
                string ntxt = Math.Floor(n).ToString("0");
                ntxt = ntxt + "." + (1e8 * (n % 1)).ToString("00000000");
                ntxt = ntxt.TrimEnd('0').TrimEnd('.');
                ntxt = ntxt + "‰";

                var comps = Parser.GetRawComponents(ntxt);
                Assert.IsTrue(comps.Count == 2, "Number of components incorrect for " + ntxt);
                comps[0].CheckNumberComp(n, 1e-8, ntxt);
                comps[1].CheckTextComp(ComponentType.BackUnary, "‰", ntxt);
            }
        }
        #endregion
        #region Negative Constants
        /// <summary>
        /// Check Negative Integers Are Parsed Correctly
        /// </summary>
        [TestMethod()]
        public void NegativeIntegerTests()
        {
            var rand = new System.Random();

            for (int i = 0; i < TestIterations; i++)
            {
                double n = Math.Round((rand.NextDouble()) * 10000000000, 0);
                string ntxt = "-" + n.ToString("0");

                var comps = Parser.GetRawComponents(ntxt);
                Assert.IsTrue(comps.Count == 2, "Number of components incorrect for " + ntxt);
                comps[0].CheckTextComp(ComponentType.Unary, "-", ntxt);
                comps[1].CheckNumberComp(n, 1e-8, ntxt);
            }
        }

        /// <summary>
        /// Check Negative Reals Are Parsed Correctly
        /// </summary>
        [TestMethod()]
        public void NegativeDoubleTests()
        {
            var rand = new System.Random();

            for (int i = 0; i < TestIterations; i++)
            {
                double n = Math.Round((rand.NextDouble()) * 10000000000, 8);
                string ntxt = Math.Floor(n).ToString("0");
                ntxt = "-" + ntxt + "." + (1e8 * (n % 1)).ToString("00000000");
                ntxt = ntxt.TrimEnd('0').TrimEnd('.');

                var comps = Parser.GetRawComponents(ntxt);
                Assert.IsTrue(comps.Count == 2, "Number of components incorrect for " + ntxt);
                comps[0].CheckTextComp(ComponentType.Unary, "-", ntxt);
                comps[1].CheckNumberComp(n, 1e-8, ntxt);
            }
        }

        /// <summary>
        /// Check Negative Reals With A Pct Op Are Parsed Correctly
        /// </summary>
        [TestMethod()]
        public void NegativeDoublePctTests()
        {
            var rand = new System.Random();

            for (int i = 0; i < TestIterations; i++)
            {
                double n = Math.Round((rand.NextDouble()) * 10000000000, 8);
                string ntxt = Math.Floor(n).ToString("0");
                ntxt = "-" + ntxt + "." + (1e8 * (n % 1)).ToString("00000000");
                ntxt = ntxt.TrimEnd('0').TrimEnd('.');
                ntxt = ntxt + "%";

                var comps = Parser.GetRawComponents(ntxt);
                Assert.IsTrue(comps.Count == 3, "Number of components incorrect for " + ntxt);
                comps[0].CheckTextComp(ComponentType.Unary, "-", ntxt);
                comps[1].CheckNumberComp(n, 1e-8, ntxt);
                comps[2].CheckTextComp(ComponentType.BackUnary, "%", ntxt);
            }
        }

        /// <summary>
        /// Check Negative Reals With A Mile Op Are Parsed Correctly
        /// </summary>
        [TestMethod()]
        public void NegativeDoubleMileTests()
        {
            var rand = new System.Random();

            for (int i = 0; i < TestIterations; i++)
            {
                double n = Math.Round((rand.NextDouble()) * 10000000000, 8);
                string ntxt = Math.Floor(n).ToString("0");
                ntxt = "-" + ntxt + "." + (1e8 * (n % 1)).ToString("00000000");
                ntxt = ntxt.TrimEnd('0').TrimEnd('.');
                ntxt = ntxt + "‰";

                var comps = Parser.GetRawComponents(ntxt);
                Assert.IsTrue(comps.Count == 3, "Number of components incorrect for " + ntxt);
                comps[0].CheckTextComp(ComponentType.Unary, "-", ntxt);
                comps[1].CheckNumberComp(n, 1e-8, ntxt);
                comps[2].CheckTextComp(ComponentType.BackUnary, "‰", ntxt);
            }
        }
        #endregion
        #region Named Constants
        /// <summary>
        /// PI should evaluate to a constant
        /// </summary>
        [TestMethod()]
        public void ConstantPiTests()
        {
            foreach (var piName in new[] { "PI", "Pi", "pi" })
            {
                var comps = Parser.GetRawComponents(piName);
                Assert.IsTrue(comps.Count == 1, "Number of components incorrect for " + piName);
                comps[0].CheckNumberComp(Math.PI, 1e-8, piName);
            }
        }

        /// <summary>
        /// e should evaluate to a constant
        /// </summary>
        [TestMethod()]
        public void ConstantETests()
        {
            foreach (var piName in new[] { "e", "E" })
            {
                var comps = Parser.GetRawComponents(piName);
                Assert.IsTrue(comps.Count == 1, "Number of components incorrect for " + piName);
                comps[0].CheckNumberComp(Math.E, 1e-8, piName);
            }
        }
        #endregion
        #region Text Constant
        /// <summary>
        /// Simple Plain Text
        /// </summary>
        [TestMethod()]
        public void TextConstants()
        {
            _textTest("Hello World");
            _textTest("(a+b)");
            _textTest("\"(a+b)\"");
            _textTest("");
            _textTest("\"");
            _textTest("\"\"");
        }

        private void _textTest(string ntxt)
        {
            string express = "\"" + ntxt.Replace("\"", "\"\"") + "\"";
            var comps = Parser.GetRawComponents(express);
            Assert.IsTrue(comps.Count == 1, "Number of components incorrect for " + ntxt);
            Assert.IsTrue(comps[0].Type == ComponentType.Value, "Raw component should be a Value for " + ntxt);
            Assert.IsTrue(comps[0].Constant == true, "Raw component should be constant for " + ntxt);

            object eval = comps[0].Evaluate(null);
            Assert.IsTrue(eval is string, "Raw component should evaluate to string for " + ntxt);
            Assert.IsTrue(eval as string == ntxt, "Raw component should evaluate to " + ntxt);
        }
        #endregion
        #region Simple Operator Tests
        /// <summary>
        /// Check 1+2 Decoded Successfully
        /// </summary>
        [TestMethod()]
        public void RawParserSimpleAdditionTest()
        {
            var result = Parser.GetRawComponents("1+2");
            Assert.IsTrue(result.Count == 3, "Should return 3 parts");
            result[0].CheckNumberComp(1, 1e-10, "1+2, 1");
            Assert.IsTrue(result[1].Type == ComponentType.Binary && result[1].Text == "+", "Middle Part Should Be A Binary Operator +");
            result[2].CheckNumberComp(2, 1e-10, "1+2, 2");
        }

        /// <summary>
        /// Check 1+-2 Decoded Successfully
        /// </summary>
        [TestMethod()]
        public void RawParserSimpleAdditionNegativeTest()
        {
            var result = Parser.GetRawComponents("1+-2");
            Assert.IsTrue(result.Count == 4, "Should return 4 parts");
            result[0].CheckNumberComp(1, 1e-10, "1+-2, 1");
            Assert.IsTrue(result[1].Type == ComponentType.Binary && result[1].Text == "+", "Second Part Should Be A Binary Operator +");
            Assert.IsTrue(result[2].Type == ComponentType.Unary && result[2].Text == "-", "Third Part Should Be A Unary Operator -");
            result[3].CheckNumberComp(2, 1e-10, "1+-2, 2");
        }

        /// <summary>
        /// Check 1&lt;2 Decoded Successfully
        /// </summary>
        [TestMethod()]
        public void RawParserSimpleLessThanTest()
        {
            var result = Parser.GetRawComponents("1<2");
            Assert.IsTrue(result.Count == 3, "Should return 3 parts");
            result[0].CheckNumberComp(1, 1e-10, "1<2, 1");
            Assert.IsTrue(result[1].Type == ComponentType.Binary && result[1].Text == "<", "Middle Part Should Be A Binary Operator <");
            result[2].CheckNumberComp(2, 1e-10, "1<2, 2");
        }

        /// <summary>
        /// Check 1&lt;=2 Decoded Successfully
        /// </summary>
        [TestMethod()]
        public void RawParserSimpleLessThanEqualsTest()
        {
            var result = Parser.GetRawComponents("1<=2");
            Assert.IsTrue(result.Count == 3, "Should return 3 parts");
            result[0].CheckNumberComp(1, 1e-10, "1<=2, 1");
            Assert.IsTrue(result[1].Type == ComponentType.Binary && result[1].Text == "<=", "Middle Part Should Be A Binary Operator <=");
            result[2].CheckNumberComp(2, 1e-10, "1<=2, 2");
        }

        /// <summary>
        /// Check 1!=3 Decoded Successfully
        /// </summary>
        [TestMethod()]
        public void RawParserSimpleNotEqualsTest()
        {
            var result = Parser.GetRawComponents("1!=3");
            Assert.IsTrue(result.Count == 3, "Should return 3 parts");
            result[0].CheckNumberComp(1, 1e-10, "1!=3, 1");
            Assert.IsTrue(result[1].Type == ComponentType.Binary && result[1].Text == "!=", "Middle Part Should Be A Binary Operator !=");
            result[2].CheckNumberComp(3, 1e-10, "1!=3, 3");
        }

        /// <summary>
        /// Check 1&lt;&gt;3 Decoded Successfully
        /// </summary>
        [TestMethod()]
        public void RawParserSimpleNotEqualsVBTest()
        {
            var result = Parser.GetRawComponents("1<>3");
            Assert.IsTrue(result.Count == 3, "Should return 3 parts");
            result[0].CheckNumberComp(1, 1e-10, "1!=3, 1");
            Assert.IsTrue(result[1].Type == ComponentType.Binary && result[1].Text == "!=", "Middle Part Should Be A Binary Operator !=");
            result[2].CheckNumberComp(3, 1e-10, "1!=3, 3");
        }
        #endregion
        #region Simple Bracket Tests
        /// <summary>
        /// Check (1) Decoded Successfully
        /// </summary>
        [TestMethod()]
        public void RawParserSimpleBracketTest()
        {
            var result = Parser.GetRawComponents("(1)");
            Assert.IsTrue(result.Count == 3, "Should return 3 parts");
            Assert.IsTrue(result[0].Type == ComponentType.BracketOpen && result[0].Text == "(", "First Part Should Be A Bracket Open (");
            result[1].CheckNumberComp(1, 1e-10, "(1)");
            Assert.IsTrue(result[2].Type == ComponentType.BracketClose && result[2].Text == ")", "First Part Should Be A Bracket Close )");
        }

        /// <summary>
        /// Check (1+3) Decoded Successfully
        /// </summary>
        [TestMethod()]
        public void RawParserSimpleBracketExpressionTest()
        {
            var result = Parser.GetRawComponents("(1+3)");
            Assert.IsTrue(result.Count == 5, "Should return 5 parts");
            Assert.IsTrue(result[0].Type == ComponentType.BracketOpen && result[0].Text == "(", "First Part Should Be A Bracket Open (");
            result[1].CheckNumberComp(1, 1e-10, "(1+3)");
            Assert.IsTrue(result[2].Type == ComponentType.Binary && result[2].Text == "+", "Middle Part Should Be A Binary Operator +");
            result[3].CheckNumberComp(3, 1e-10, "(1+3)");
            Assert.IsTrue(result[4].Type == ComponentType.BracketClose && result[4].Text == ")", "First Part Should Be A Bracket Close )");
        }
        #endregion
    }
}
