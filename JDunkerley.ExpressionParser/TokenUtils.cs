using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace JDunkerley.ExpressionParser
{
    /// <summary>
    /// Extension Methods To Help Build Tokenizer
    /// </summary>
    public static class TokenUtils
    {
        /// <summary>
        /// Token Factory Func
        /// Set to NULL to use default constructor
        /// </summary>
        public static TokenFactoryDelegate TokenFactoryFunc { get; set; } 

        /// <summary>
        /// Creste A New Token
        /// </summary>
        /// <param name="tokenType"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IToken Create(this TokenType tokenType, string text)
        {
            var func = TokenFactoryFunc;
            if (func != null) return func(tokenType, text);
            return new Token(tokenType, text);
        }

        /// <summary>
        /// Helper Function To Create A Character Lookup Dictionary
        /// </summary>
        /// <param name="charsString"></param>
        /// <returns></returns>
        internal static IReadOnlyDictionary<char, bool> CreateCharacterLookUp(this string charsString)
        {
            var dict = charsString.ToCharArray().ToDictionary(c => c, c => true);
            return new ReadOnlyDictionary<char, bool>(dict);
        }
    }
}