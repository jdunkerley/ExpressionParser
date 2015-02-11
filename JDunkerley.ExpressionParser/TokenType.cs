using System;

namespace JDunkerley.ExpressionParser
{
    /// <summary>
    /// Type of Parsed Tokens
    /// </summary>
    public enum TokenType
    {   
        /// <summary>
        /// Text With Escape Characters
        /// </summary>
        StringLiteral,
        /// <summary>
        /// VB / Excel Style Text
        /// </summary>
        StringExcelStyleLiteral,
        /// <summary>
        /// Numeric Literal
        /// </summary>
        NumericLiteral,
        /// <summary>
        /// Date Literal
        /// </summary>
        DateLiteral,
        /// <summary>
        /// One of the Operators
        /// </summary>
        Operator,
        /// <summary>
        /// Identifier (Keyword, Variable, Function, Constant)
        /// </summary>
        Identifier
    }
}
