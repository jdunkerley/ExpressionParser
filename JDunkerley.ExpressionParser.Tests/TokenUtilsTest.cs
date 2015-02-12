using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JDunkerley.ExpressionParser.Test
{
    [TestClass]
    public class TokenUtilsTest
    {
        [TestMethod]
        public void Test_TokenFactory_CreateStringLiteral()
        {
            const string text = "Test";
            var token = TokenType.StringLiteral.Create(text);
            Assert.IsNotNull(token, "Token should not be null");
            Assert.IsInstanceOfType(token, typeof(IToken), "Token should be an IToken");
            Assert.AreEqual(text, token.Text, "Text should be set to original value");
            Assert.AreEqual(TokenType.StringLiteral, token.TokenType, "TokenType should be set to StringLiteral");
        }

        [ExpectedException(typeof(ArgumentNullException), "Null is not an acceptable literal")]
        [TestMethod]
        public void Test_TokenFactory_ThrowsNullArgumentExceptionIfTextNull()
        {
            var token = TokenType.StringLiteral.Create(null);
        }
    }
}
