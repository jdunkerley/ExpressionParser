namespace JDunkerley.ExpressionParser
{
    /// <summary>
    /// Interface Defining A Token
    /// </summary>
    public interface IToken
    {
        /// <summary>
        /// Type of Token
        /// </summary>
        TokenType TokenType { get; }

        /// <summary>
        /// Raw Text Of Token
        /// </summary>
        string Text { get; }
    }
}