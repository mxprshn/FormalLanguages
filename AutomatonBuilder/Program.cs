using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AutomatonBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1) throw new ArgumentException();

            var grammar = GrammarParser.FromFile(args[0]);
            foreach (var nonterminal in grammar.Nonterminals)
            {
                foreach (var rule in nonterminal.TerminalProductions)
                {
                    Console.WriteLine($"{nonterminal} -> {rule.Literal}");
                }

                foreach (var rule in nonterminal.NonterminalProductions)
                {
                    Console.WriteLine($"{nonterminal} -> {rule.t.Literal}{rule.N.Literal}");
                }
            }

            var automaton = grammar.ToAutomaton();

            var symbols = automaton.Symbols.ToArray();
            var states = automaton.States.ToArray();

            for (var i = 0; i < states.Length; ++i)
            {
                var chunk = new State[1] { states[i] };
                PrintTable(symbols, chunk);
            }

            //File.WriteAllText("C:\\Users\\Max\\Desktop\\automaton.txt", tableText.ToString());
        }

        private static void PrintTable(AutomatonSymbol[] symbols, State[] states)
        {
            var divider = new string('-', 40);
            var table = new string[symbols.Length + 1, states.Length + 1];

            table[0, 0] = "символ\\состояние";

            for (var i = 0; i < symbols.Length; ++i)
            {
                table[i + 1, 0] = symbols[i].Literal;
            }

            for (var i = 0; i < states.Length; ++i)
            {
                table[0, i + 1] = states[i].Name;

                for (var j = 0; j < symbols.Length; ++j)
                {
                    var cell = new StringBuilder("{");

                    foreach (var nextState in states[i][symbols[j]])
                    {
                        cell.Append($" {nextState}");
                    }

                    cell.Append(" }");
                    table[j + 1, i + 1] = cell.ToString();
                }
            }

            for (var i = 0; i <= symbols.Length; ++i)
            {
                for (var j = 0; j <= states.Length; ++j)
                {
                    Console.Write($"{table[i, j],40} |");
                }

                Console.WriteLine();

                if (i != 0 && i != symbols.Length) continue;
                for (var j = 0; j <= states.Length; ++j)
                {
                    Console.Write($"{divider}-+");
                }

                Console.WriteLine();
            }
        }
    }
}
