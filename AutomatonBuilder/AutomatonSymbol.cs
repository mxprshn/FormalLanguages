using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatonBuilder
{
    public class AutomatonSymbol
    {
        public string Literal { get; private set; }

        public AutomatonSymbol(string literal)
        {
            Literal = literal;
        }

        public override int GetHashCode()
        {
            return Literal.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ((AutomatonSymbol)obj).Literal == Literal;
        }

        public override string ToString()
        {
            return Literal;
        }
    }
}
