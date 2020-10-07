using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutomatonBuilder
{
    public class Automaton
    {
        private HashSet<State> states = new HashSet<State>();
        public IReadOnlyCollection<State> States => states;

        private HashSet<AutomatonSymbol> symbols = new HashSet<AutomatonSymbol>();
        public IReadOnlyCollection<AutomatonSymbol> Symbols => symbols;

        public void AddState(State state)
        {
            states.Add(state);
        }

        public void AddSymbol(AutomatonSymbol symbol)
        {
            symbols.Add(symbol);
        }

        public State AddOrGetState(string name)
        {
            var existing = states.Where(s => s.Name == name).FirstOrDefault();

            if (existing == null)
            {
                var newState = new State(name);
                AddState(newState);
                return newState;
            }

            return existing;
        }

        public AutomatonSymbol AddOrGetSymbol(string literal)
        {
            var existing = symbols.Where(s => s.Literal == literal).FirstOrDefault();

            if (existing == null)
            {
                var newSymbol = new AutomatonSymbol(literal);
                AddSymbol(newSymbol);
                return newSymbol;
            }

            return existing;
        }
    }
}
