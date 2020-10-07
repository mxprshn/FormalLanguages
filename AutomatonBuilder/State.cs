using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AutomatonBuilder
{
    public class State
    {
        public string Name { get; private set; }

        public State(string name)
        {
            Name = name;
        }

        private Dictionary<AutomatonSymbol, HashSet<State>> transitions = new Dictionary<AutomatonSymbol, HashSet<State>>();

        public IReadOnlyCollection<State> this[AutomatonSymbol symbol] =>
            transitions.TryGetValue(symbol, out var set) ? set : new HashSet<State>();
        
        public void AddTransition(AutomatonSymbol symbol, State state)
        {

            if (!transitions.TryGetValue(symbol, out var symbolTransitions))
            {
                transitions[symbol] = new HashSet<State> { state };
                return;
            }

            symbolTransitions.Add(state);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ((State)obj).Name == Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
