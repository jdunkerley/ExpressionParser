using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JDunkerley.ExpressionParser
{
    // ToDo: What about , and () for accounting?

    /// <summary>
    /// Standard Implementation of a Tokenizer:
    /// 
    /// Ignores White Space
    /// Strings Start With " 
    /// VB Style String Start With @"
    /// </summary>
    public class StandardTokenizer : ITokenizer
    {
        /// <summary>
        /// White Space Characters
        /// </summary>
        public const string WhiteSpaceCharactersConst = " \t\r\n";
        /// <summary>
        /// Standard Set of Characters for a Numeric Constant
        /// Once started allow .
        /// </summary>
        public const string NumericCharactersConst = "0123456789";
        /// <summary>
        /// Standard Set of Operators
        /// </summary>
        public const string OperatorCharactersConst = "+-/*^%‰=!<>|&?:$()";
        /// <summary>
        /// Standard Set of Function Name Constants
        /// Post First Character Allow Numeric As Well
        /// </summary>
        public const string IdentifierCharactersConst = "_abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private readonly IReadOnlyDictionary<char, bool> _numericLoookUp; 
        private readonly IReadOnlyDictionary<char, bool> _operatorLookUp;
        private readonly IReadOnlyDictionary<char, bool> _identifierLookUp; 
        private readonly IReadOnlyDictionary<char, bool> _whiteSpaceLookUp;

        /// <summary>
        /// Create A Standard Tokenizer
        /// </summary>
        /// <param name="numericChars"></param>
        /// <param name="operatorChars"></param>
        /// <param name="identifierChars"></param>
        /// <param name="whiteSpaceChars"></param>
        public StandardTokenizer(
            string numericChars = NumericCharactersConst,
            string operatorChars = OperatorCharactersConst,
            string identifierChars = IdentifierCharactersConst,
            string whiteSpaceChars = WhiteSpaceCharactersConst)
        {
            _numericLoookUp = numericChars.CreateCharacterLookUp();
            _operatorLookUp = operatorChars.CreateCharacterLookUp();
            _identifierLookUp = identifierChars.CreateCharacterLookUp();
            _whiteSpaceLookUp = whiteSpaceChars.CreateCharacterLookUp();
        }

        private enum CurrentState
        {
            Neutral,
            InNumeric,
            InString,
            InIdentifier
        }

        /// <summary>
        /// Break an expression into tokens
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IReadOnlyList<IToken> Tokenize(string expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            int idx = 0;
            var output = new List<IToken>();

            bool finished = false;
            while (!finished)
            {
                var readNext = ReadNextToken(expression, idx);

                if (readNext.Item1 == null && readNext.Item2 == 0)
                {
                    finished = true;
                    continue;
                }

                if (readNext.Item1 != null)
                {
                    output.Add(readNext.Item1);   
                }

                idx = readNext.Item2;
            }

            return output;
        }

        /// <summary>
        /// Read Next Token
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public Tuple<IToken, int> ReadNextToken(string expression, int idx)
        {
            // End of string
            if (idx == expression.Length)
            {
                return new Tuple<IToken, int>(null, 0);
            }

            // Whitespace
            var c = expression[idx];
            if (_whiteSpaceLookUp.ContainsKey(c))
            {
                return ReadNextToken(expression, idx + 1);
            }

            // Operators
            if (_operatorLookUp.ContainsKey(c))
            {
                return Tuple.Create(TokenType.Operator.Create(c.ToString()), idx + 1);
            }

            // Number
            if (_numericLoookUp.ContainsKey(c))
            {
                return ReadNumericToken(expression, idx);
            }

            // String
            if (c == '"')
            {
                return ReadStringToken(expression, idx);
            }

            // VB String
            if (c == '@')
            {
                
            }

            // Identifier

            // Malformed
            throw new FormatException("expression is not of a valid format");
        }

        /// <summary>
        /// Read Numeric Token
        /// </summary>
        /// <returns></returns>
        public Tuple<IToken, int> ReadNumericToken(string expression, int idx)
        {
            int cIdx = idx;
            while (cIdx < expression.Length && (_numericLoookUp.ContainsKey(expression[cIdx]) || expression[cIdx] == '.'))
            {
                cIdx++;
            }
            return Tuple.Create(
                TokenType.NumericLiteral.Create(expression.Substring(idx, cIdx - idx)), 
                cIdx);
        }

        /// <summary>
        /// Read String Token
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public Tuple<IToken, int> ReadStringToken(string expression, int idx)
        {
            if (expression[idx] != '"')
            {
                throw new FormatException("expression @ " + idx + " is not \"");
            }

            int cIdx = idx + 1;
            while (cIdx < expression.Length)
            {
                var c = expression[cIdx];
                if (c == '"')
                {
                    return Tuple.Create(
                        TokenType.StringLiteral.Create(expression.Substring(idx + 1, cIdx - idx - 2)),
                        cIdx + 1);
                }

                if (c == '\\' && cIdx + 1 < expression.Length && expression[cIdx + 1] == '"')
                {
                    cIdx++;
                }
                cIdx++;
            }

            throw new FormatException("Unable to find end of string literal");
        }

        /// <summary>
        /// Read String Token
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public Tuple<IToken, int> ReadExcelStringToken(string expression, int idx)
        {
            int cIdx = idx + 2;
            while (cIdx < expression.Length)
            {
                var c = expression[cIdx];
                if (c == '"')
                {
                    if (cIdx + 1 == expression.Length || expression[cIdx + 1] != '"')
                    {
                        return Tuple.Create(
                            TokenType.StringLiteral.Create(expression.Substring(idx + 1, cIdx - idx - 2)),
                            cIdx + 1);                        
                    }

                    cIdx++;
                }

                cIdx++;
            }

            throw new FormatException("Unable to find end of string literal");
        }
    }
}