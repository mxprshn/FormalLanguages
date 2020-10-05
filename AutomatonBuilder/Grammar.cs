using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatonBuilder
{
    public class Grammar
    {
        private HashSet<Nonterminal> nonterminals = new HashSet<Nonterminal>();
        public IReadOnlyCollection<Nonterminal> Nonterminals => nonterminals;

        public void AddNonterminal(Nonterminal nonterminal)
        {
            nonterminals.Add(nonterminal);
        }
    }
}