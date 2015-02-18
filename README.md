#Expression Parser
A library designed to read user entered string and evaluate it as an math expression. 
- Able to understand all the operators that Excel users are used to. 
- Expose the System.Math functions, but allow additional functions to be added by API 
- Allow variables access via delegate
- A portable class library targetting .Net 4.5, Win Phone, Modern UI Apps

##Quick Start

##Quick Evaluation of a String

To evaluate a single expression and return a value:

    object output = JDunkerley.Parser.Parser.Evaluate("1+6%*(90/365)");
  
By default, the System.Math functions are not included.  To register these you need to call:

    JDunkerley.Parser.Parser.RegisterMathFunctions();
    object output = JDunkerley.Parser.Parser.Evaluate("Sin(90/360*2*PI)");

