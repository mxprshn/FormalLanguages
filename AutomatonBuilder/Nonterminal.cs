using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatonBuilder
{
    public class Nonterminal : GrammarSymbol
    {
        private HashSet<Terminal> terminalProductions = new HashSet<Terminal>();
        private HashSet<(Terminal t, Nonterminal N)> nonterminalProductions = new HashSet<(Terminal t, Nonterminal N)>();

        public IReadOnlyCollection<Terminal> TerminalProductions => terminalProductions;
        public IReadOnlyCollection<(Terminal t, Nonterminal N)> NonterminalProductions => nonterminalProductions;

        public Nonterminal(string literal) : base(literal) { }

        public void AddTerminalProduction(Terminal terminal)
        {
            terminalProductions.Add(terminal);
        }

        public void AddNonterminalProduction(Terminal terminal, Nonterminal nonterminal)
        {
            nonterminalProductions.Add((terminal, nonterminal));
        }
    }
}
