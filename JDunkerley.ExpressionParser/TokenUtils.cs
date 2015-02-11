using System.Collections.Generic;
using System.Linq;

namespace JDunkerley.ExpressionParser
{
    public static class TokenUtils
    {
        /// <summary>
        /// Creste A New Token
        /// </summary>
        /// <param name="tokenType"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Token Create(this TokenType tokenType, string text)
        {
            return new Token(tokenType, text);
        }

        /// <summary>
        /// Helper Function To Create A Character Lookup Dictionary
        /// </summary>
        /// <param name="charsString"></param>
        /// <returns></returns>
        internal static Dictionary<char, bool> CreateCharacterLookUp(this string charsString)
        {
            return charsString.ToCharArray().ToDictionary(c => c, c => true);
        }
    }
}