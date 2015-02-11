using System;

namespace JDunkerley.ExpressionParser
{
    /// <summary>
    /// Class Holding A Parsed Token
    /// </summary>
    public sealed class Token : IToken
    {
        private readonly TokenType _tokenType;
        private readonly string _text;

        /// <summary>
        /// Create A Token
        /// </summary>
        /// <param name="tokenType"></param>
        /// <param name="text"></param>
        public Token(TokenType tokenType, string text)
        {
            if (text == null) 
                throw new ArgumentNullException("text", "text cannot be NULL");

            _tokenType = tokenType;
            _text = text;
        }

        /// <summary>
        /// Type of Token
        /// </summary>
        public TokenType TokenType
        {
            get { return this._tokenType; }
        }

        /// <summary>
        /// Raw Text Of Token
        /// </summary>
        public string Text
        {
            get { return this._text; }
        }

        /// <summary>
        /// Are Two Tokens Equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Token && Equals((Token) obj);
        }
        private bool Equals(Token other)
        {
            return _tokenType == other._tokenType && string.Equals(_text, other._text);
        }

        /// <summary>
        /// Create A HashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)_tokenType * 397) ^ (_text != null ? _text.GetHashCode() : 0);
            }
        }
    }
}