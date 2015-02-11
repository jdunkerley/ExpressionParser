using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserTests
{
    /// <summary>
    /// Component Test Class : Only Type and Text Implemented
    /// </summary>
    public class Component : JDunkerley.Parser.IComponent
    {
        private readonly JDunkerley.Parser.ComponentType _type;
        private readonly string _text;

        public Component(JDunkerley.Parser.ComponentType Type, string Text)
        {
            this._type = Type;
            this._text = Text;
        }

        public JDunkerley.Parser.ComponentType Type
        {
            get { return this._type; }
        }

        public string Text
        {
            get { return this._text; }
        }

        public bool Constant
        {
            get { throw new NotImplementedException(); }
        }

        public string[] Variables
        {
            get { throw new NotImplementedException(); }
        }

        public object Evaluate(Func<string, object> VariableCallBack)
        {
            throw new NotImplementedException();
        }

        public void EvaluateConstants()
        {
            throw new NotImplementedException();
        }
    }
}
