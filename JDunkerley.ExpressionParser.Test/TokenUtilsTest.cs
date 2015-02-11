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
            Assert.IsNotNull(token, "Token should not be null");
            Assert.IsInstanceOfType(token, typeof(IToken), "Token should be an IToken");
            Assert.AreEqual("Test", token.Text, "Text should be set to original value");
            Assert.AreEqual(TokenType.StringLiteral, token.TokenType, "TokenType should be set to StringLiteral");
        }
    }
}
