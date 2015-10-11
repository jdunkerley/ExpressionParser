using System.Collections.Generic;

namespace JDunkerley.ExpressionParser
{
    /// <summary>
    /// Interface for Tokenizer
    /// Goal is to break string up into sections
    /// </summary>
    public interface ITokenizer
    {
        /// <summary>
        /// Given A String Break Into Raw Tokens
        /// May Need To Merge Some After This
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IReadOnlyList<IToken> Tokenize(string expression);
    }
}