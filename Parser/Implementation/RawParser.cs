using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JDunkerley.Parser.Implementation
{
    /// <summary>
    /// Breaks A String Into Raw Parts
    /// First Stage Of Parsing String Into Components
    /// </summary>
    public static class RawParser
    {
        private const string numericConst = "0123456789.";
        private static readonly Dictionary<char, bool> numericChars;
        private const string operatorConst = "+-/*^%‰=!<>|&?:$";
        private static readonly Dictionary<char, bool> operatorChars;
        private const string functionConst = "_abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly Dictionary<char, bool> functionChars;

        static RawParser()
        {
            numericChars = CreateDict(numericConst);
            operatorChars = CreateDict(operatorConst);
            functionChars = CreateDict(functionConst);
        }

        /// <summary>
        /// Parses the string into raw components
        /// </summary>
        /// <param name="input"></param>
        /// <param name="errorMessage">Error Message If Parser Fails</param>
        /// <returns></returns>
        public static List<IComponent> Parse(string input, out string errorMessage)
        {
            List<IComponent> components = new List<IComponent>();
            errorMessage = "";

            int idx = 0;
            int length = input.Length;

            while (idx < length)
            {
                char c = input[idx];
                switch (c)
                {
                    case '(': idx++; components.Add(new Component(ComponentType.BracketOpen, "(")); break;
                    case '[': idx++; components.Add(new Component(ComponentType.IndexerOpen, "[")); break;
                    case ')': idx++; components.Add(new Component(ComponentType.BracketClose, ")")); break;
                    case ']': idx++; components.Add(new Component(ComponentType.IndexerClose, "]")); break;
                    case ',': idx++; components.Add(new Component(ComponentType.Comma, ",")); break;
                    case '"': idx += GetTextConstant(components, input, length, idx); break;
                    //case '#':  break; // Date Constant
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n': idx++; break;
                    default:
                        if (numericChars.ContainsKey(c))
                            idx += GetNumeric(components, input, length, idx);
                        else if (operatorChars.ContainsKey(c))
                            idx += GetOperator(components, input, length, idx);
                        else if (functionChars.ContainsKey(c))
                            idx += GetVariable(components, input, length, idx);
                        else
                        {
                            errorMessage = "Unsupported Character In Text: " + c;
                            return null; 
                        }
                        break;
                }

                if (components.Count > 1 && components[components.Count - 2].Type == ComponentType.Operator)
                    SolveBackUnaryOperators(components, components.Count - 2);
            }

            if (components.Count > 0 && components.Last().Type == ComponentType.Operator)
                SolveBackUnaryOperators(components, components.Count - 1);

            return components;
        }

        private static Dictionary<char, bool> CreateDict(string chars)
        {
            var output = new Dictionary<char, bool>();
            for (int i = 0; i < chars.Length; i++)
                output[chars[i]] = true;
            return output;
        }

        private static int GetNumeric(List<IComponent> components, string input, int length, int idx)
        {
            int len = 1;
            while (idx + len < length && numericChars.ContainsKey(input[idx + len]))
                len++;

            string txt = input.Substring(idx, len);
            if (txt.StartsWith(".")) txt = "0" + txt;

            double val;
            if (!double.TryParse(txt, out val))
                throw new FormatException("Unable to Parse Value:" + txt);
            components.Add(new Components.NumericalValue(val));

            return len;
        }

        private static int GetVariable(List<IComponent> components, string input, int length, int idx)
        {
            int len = 1;
            while (idx + len < length &&
                ((numericChars.ContainsKey(input[idx + len]) && input[idx + len] != '.') ||
                functionChars.ContainsKey(input[idx + len])))
                len++;

            string varName = input.Substring(idx, len);
            if (varName.ToLower() == "pi")
                components.Add(new Components.NumericalValue(Math.PI));
            else if (varName.ToLower() == "e")
                components.Add(new Components.NumericalValue(Math.E));
            else if (varName.ToLower() == "true")
                components.Add(new Components.EvaluatedBlock(true));
            else if (varName.ToLower() == "false")
                components.Add(new Components.EvaluatedBlock(false));
            else
                components.Add(new Component(ComponentType.Variable, varName));
            return len;
        }

        private static int GetOperator(List<IComponent> components, string input, int length, int idx)
        {
            // Operator
            int len = 1;
            char c = input[idx];
            char d = (idx + 1 < length ? input[idx + 1] : (char)0);
            if ((d == '=' && (c == '=' || c == '!' || c == '<' || c == '>')) ||
                (d == '>' && c == '<') ||
                (d == '|' && c == '|') ||
                (d == '&' && c == '&'))
                len = 2;
            string op = input.Substring(idx, len);

            // Operator Type
            ComponentType opType;
            switch (op)
            {
                case "%": opType = ComponentType.Operator; break; // Unknown at this point (Binary or BackUnary)
                case "‰": opType = ComponentType.BackUnary; break; // Must be mile operator
                case "!": opType = ComponentType.Unary; break; // Not Operator
                case "<>": op = "!="; opType = ComponentType.Binary; break; // Switch <> to !=
                case "+":
                case "-":
                    if (components.Count == 0 || components.Last().Type.isUnary())
                        opType = ComponentType.Unary;
                    else
                        opType = ComponentType.Binary;
                    break;
                default: opType = ComponentType.Binary; break;
            }

            components.Add(new Component(opType, op));
            return len;
        }

        private static void SolveBackUnaryOperators(List<IComponent> c, int i)
        {
            int j = i + 1;
            var opType = (j < c.Count ? c[j].Type : ComponentType.Comma).isBackUnary() ? ComponentType.BackUnary : ComponentType.Binary;
            c[i] = new Component(opType, c[i].Text);
        }

        private static int GetTextConstant(List<IComponent> components, string input, int length, int idx)
        {
            int end = idx + 1;
            while (end < length)
            {
                if (input[end] != '"' || (end + 1 < length && input[end + 1] == '"'))
                {
                    end += input[end] == '"' ? 2 : 1;
                    continue;   
                }

                components.Add(new Components.TextValue(input.Substring(idx + 1, end - idx - 1).Replace("\"\"","\"")));
                return end - idx + 1;
            }

            throw new FormatException("Unable to read text constant:" + input.Substring(idx));
        }
    }
}