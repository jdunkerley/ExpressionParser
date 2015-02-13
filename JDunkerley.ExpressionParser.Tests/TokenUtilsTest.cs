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

        [TestMethod]
        public void Test_CreateCharacterLookup_CanCreateAnEmptyOne()
        {
            var test = "".CreateCharacterLookUp();
            Assert.IsNotNull(test, "Should not be null");
            Assert.AreEqual(0, test.Count, "Dictionary should be empty");
        }

        [TestMethod]
        public void Test_CreateCharacterLookup_SingleCharacterWorks()
        {
            var test = "A".CreateCharacterLookUp();
            Assert.IsNotNull(test, "Should not be null");
            Assert.AreEqual(1, test.Count, "Dictionary should contain one element");
            Assert.IsTrue(test.ContainsKey('A'), "Dictionary should contain 'A'");
            Assert.IsFalse(test.ContainsKey('a'), "Dictionary should not contain 'a'");
        }

        [TestMethod]
        public void Test_CreateCharacterLookup_DuplicateCharactersMerge()
        {
            var test = "AAAA".CreateCharacterLookUp();
            Assert.IsNotNull(test, "Should not be null");
            Assert.AreEqual(1, test.Count, "Dictionary should contain one element");
            Assert.IsTrue(test.ContainsKey('A'), "Dictionary should contain 'A'");
            Assert.IsFalse(test.ContainsKey('a'), "Dictionary should not contain 'a'");
        }

    }
}
