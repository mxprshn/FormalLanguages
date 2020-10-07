using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AutomatonBuilder
{
    public class Grammar
    {
        private HashSet<Nonterminal> nonterminals = new HashSet<Nonterminal>();
        public IReadOnlyCollection<Nonterminal> Nonterminals => nonterminals;

        private HashSet<Terminal> terminals = new HashSet<Terminal>();
        public IReadOnlyCollection<Terminal> Terminals => terminals;

        public void AddNonterminal(Nonterminal nonterminal)
        {
            nonterminals.Add(nonterminal);
        }

        public void AddTerminal(Terminal terminal)
        {
            terminals.Add(terminal);
        }

        public Nonterminal AddOrGetNonterminal(string literal)
        {
            var existing = nonterminals.Where(nt => nt.Literal == literal).FirstOrDefault();

            if (existing == null)
            {
                var newNonterminal = new Nonterminal(literal);
                AddNonterminal(newNonterminal);
                return newNonterminal;
            }

            return existing;
        }

        public Terminal AddOrGetTerminal(string literal)
        {
            var existing = terminals.Where(nt => nt.Literal == literal).FirstOrDefault();

            if (existing == null)
            {
                var newTerminal = new Terminal(literal);
                AddTerminal(newTerminal);
                return newTerminal;
            }

            return existing;
        }

        public Automaton ToAutomaton()
        {
            var automaton = new Automaton();

            foreach (var terminal in terminals)
            {
                automaton.AddSymbol(new AutomatonSymbol(terminal.Literal));
            }

            var finalState = new State("A-FINAL");
            automaton.AddState(finalState);

            foreach (var nonterminal in nonterminals)
            {
                var state = automaton.AddOrGetState(nonterminal.Literal);

                foreach (var terminal in nonterminal.TerminalProductions)
                {
                    var automatonSymbol = automaton.AddOrGetSymbol(terminal.Literal);
                    state.AddTransition(automatonSymbol, finalState);
                }

                foreach (var nonterminalProduction in nonterminal.NonterminalProductions)
                {
                    var automatonSymbol = automaton.AddOrGetSymbol(nonterminalProduction.t.Literal);
                    var nextState = automaton.AddOrGetState(nonterminalProduction.N.Literal);
                    state.AddTransition(automatonSymbol, nextState);
                }
            }

            return automaton;
        }
    }
}