namespace JDunkerley.ExpressionParser
{
    /// <summary>
    /// Delegate For A Token Factory
    /// </summary>
    /// <param name="tokenType"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public delegate IToken TokenFactoryDelegate(TokenType tokenType, string text);
}