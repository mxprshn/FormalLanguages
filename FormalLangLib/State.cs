using System;
using System.Collections.Generic;
using System.Text;

namespace FormalLangLib
{
    public class State
    {
        public string Name { get; private set; }
        public bool IsFinal { get; private set; }

        public State(string name, bool isFinal)
        {
            Name = name;
            IsFinal = isFinal;
        }

        public override bool Equals(object obj)
        {
            return ((State)obj).Name == Name;
        }

        public override string ToString() => Name;

        public override int GetHashCode() => Name.GetHashCode();
    }
}
