using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatonBuilder
{
    public class Terminal : GrammarSymbol
    {
        public Terminal(string literal) : base(literal) { }

        public override bool Equals(object obj)
        {
            return ((Terminal)obj).Literal == Literal;
        }
    }
}
