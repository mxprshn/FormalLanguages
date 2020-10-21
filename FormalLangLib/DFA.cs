using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FormalLangLib
{
    public class DFA
    {
        private HashSet<State> states = new HashSet<State>();
        public IReadOnlyCollection<State> States => states;

        private HashSet<string> symbols = new HashSet<string>();
        public IReadOnlyCollection<string> Symbols => symbols;

        private Dictionary<(State, string), State> transitions = new Dictionary<(State, string), State>();

        public State this[State state, string symbol]
        {
            get => transitions.GetValueOrDefault((state, symbol));

            set
            {
                transitions.TryAdd((state, symbol), value);
                states.Add(state);
                symbols.Add(symbol);
            } 
        }

        public State AddState(string name, bool isFinal)
        {
            var newState = new State(name, isFinal);
            states.Add(newState);
            return newState;
        }            

        public (DFA minimal, bool wasMinimal) Minimize()
        {
            var statesArray = states.ToArray();
            var indexes = Enumerable.Range(0, statesArray.Length).ToDictionary(i => statesArray[i]);

            var table = new bool[statesArray.Length, statesArray.Length];

            for (var i = 0; i < statesArray.Length; ++i)
            {
                for (var j = 0; j < i + 1; ++j)
                {
                    if (statesArray[i].IsFinal ^ statesArray[j].IsFinal)
                    {
                        table[i, j] = true;
                        table[j, i] = true;
                    }
                }
            }

            Console.WriteLine("Исходная таблица -- отмечены заведомо различимые пары состояний:");

            table.Print(statesArray);
            Console.WriteLine();

            while (true)
            {
                var distinguishable = new List<(int, int)>();

                foreach (var symbol in symbols)
                {
                    for (var i = 0; i < statesArray.Length; ++i)
                    {
                        for (var j = 0; j < i + 1; ++j)
                        {
                            var iTransition = this[statesArray[i], symbol];
                            var jTransition = this[statesArray[j], symbol];

                            if (iTransition == null && jTransition == null) continue;

                            if ((iTransition == null ^ jTransition == null || table[indexes[iTransition], indexes[jTransition]]) 
                                && !table[i, j] && !distinguishable.Contains((i, j)))
                            {
                                distinguishable.Add((i, j));
                            }
                        }
                    }
                }

                if (distinguishable.Count == 0) break;

                foreach (var (i, j) in distinguishable)
                {
                    table[i, j] = true;
                    table[j, i] = true;
                }

                Console.WriteLine($"Найдено {distinguishable.Count} различимых пар:");

                table.Print(statesArray);
                Console.WriteLine();
            }

            var minimal = new DFA();
            var combinedStates = new List<HashSet<State>>();

            for (var i = 0; i < statesArray.Length; ++i)
            {
                for (var j = 0; j < i + 1; ++j)
                {
                    if (!table[i, j])
                    {
                        var combined = combinedStates.Find(set => set.Contains(statesArray[i]) || set.Contains(statesArray[j]));

                        if (combined == null)
                        {
                            combinedStates.Add(new HashSet<State> { statesArray[i], statesArray[j] });
                        }
                        else
                        {
                            combined.Add(statesArray[i]);
                            combined.Add(statesArray[j]);
                        }
                    }
                }
            }

            var wasMinimal = combinedStates.Count == statesArray.Length;

            var combinedTransformed = combinedStates
                .Select((set, i) => (minimal.AddState($"q_{i}", set.Any(s => s.IsFinal)), set));

            foreach (var combined in combinedTransformed)
            {
                foreach (var symbol in symbols)
                {
                    var transition = combinedTransformed.FirstOrDefault(ct => ct.set.Contains(this[combined.set.First(), symbol])).Item1;
                    if (transition != null) minimal[combined.Item1, symbol] = transition;
                }
            }

            return (minimal, wasMinimal);
        }
    }
}
