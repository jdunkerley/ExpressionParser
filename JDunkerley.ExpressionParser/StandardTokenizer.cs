using System.Collections.Generic;
using System.Linq;

namespace JDunkerley.ExpressionParser
{
    /// <summary>
    /// Standard Implementation of a Tokenizer 
    ///     Operator
    /// </summary>
    public class StandardTokenizer : ITokenizer
    {
        // " start a standard string
        // @" start a vb style string

        public const string WhiteSpaceCharactersConst = " \t\r\n";
        /// <summary>
        /// Standard Set of Characters for a Numeric Constant
        /// </summary>
        public const string NumericCharactersConst = "0123456789";
        /// <summary>
        /// Standard Set of Operators
        /// </summary>
        public const string OperatorCharactersConst = "+-/*^%‰=!<>|&?:$()";
        /// <summary>
        /// Stan
        /// </summary>
        public const string functionConst = "_abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private readonly Dictionary<char, bool> _whiteSpaceLookUp;
        private readonly Dictionary<char, bool> _operatorLookUp; 


        /// <summary>
        /// Create A Standard Tokenizer
        /// </summary>
        /// <param name="numericChars"></param>
        /// <param name="operatorChars"></param>
        /// <param name="funcationChars"></param>
        public StandardTokenizer(
            string numericChars = NumericCharactersConst,
            string operatorChars = OperatorCharactersConst,
            string funcationChars = functionConst,
            string whiteSpaceChars = WhiteSpaceCharactersConst)
        {
            _whiteSpaceLookUp = whiteSpaceChars.CreateCharacterLookUp();
            _operatorLookUp = operatorChars.CreateCharacterLookUp();
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
        public IEnumerable<IToken> Tokenize(string expression)
        {
            int idx = 0;

            var tokens = new List<IToken>();
            var cState = CurrentState.Neutral;
            while (idx < expression.Length)
            {
                char cur = expression[idx];

                switch (cState)
                {
                    case CurrentState.Neutral:
                        HandleNeutral(ref cState, tokens, cur);
                        break;
                    case CurrentState.InString:
                        break;
                    case CurrentState.InNumeric:
                        break;
                }

                idx++;
            }

            return tokens;
        }



        private void HandleNeutral(ref CurrentState cState, List<IToken> tokens, char character)
        {
            // White Space: Do Nothing
            if (_whiteSpaceLookUp.ContainsKey(character))
            {
                return;
            }
            
            // Operator
            if (_operatorLookUp.ContainsKey(character))
            {
                tokens.Add(TokenType.Operator.Create(character.ToString()));
                return;
            }

            // Numeric

            // String

            // Literal
        }
    }
}