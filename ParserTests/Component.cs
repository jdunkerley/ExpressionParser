using System;
using JDunkerley.Parser;

namespace ParserTests
{
    /// <summary>
    /// Component Test Class : Only Type and Text Implemented
    /// </summary>
    public class Component : IComponent
    {
        private readonly ComponentType _type;
        private readonly string _text;

        public Component(ComponentType Type, string Text)
        {
            this._type = Type;
            this._text = Text;
        }

        public ComponentType Type
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
