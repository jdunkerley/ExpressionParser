using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace JDunkerley.ExpressionParser.Test
{
    [TestClass]
    public class TokenUtilsTest
    {
        [TestMethod]
        public void TestCreate_StringLiteral()
        {
            var token = TokenType.StringLiteral.Create("Test");
            Assert.IsNotNull(token);
            Assert.IsInstanceOfType(token, typeof(Token));
        }
    }
}
