using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatonBuilder
{
    public abstract class GrammarSymbol
    {
        public string Literal { get; private set; }

        public GrammarSymbol(string literal)
        {
            Literal = literal;
        }        
    }
}
