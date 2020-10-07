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
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            if (args.Length != 1) throw new ArgumentException();

            Grammar grammar = null;

            try
            {
                grammar = GrammarParser.FromFile(args[0]);
            }
            catch (ParserException e)
            {
                Console.WriteLine($"ОШИБКА: некорректная грамматика -- {e.Message}");
                return;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("ОШИБКА: файл не найден");
                return;
            }

            Console.WriteLine("Грамматика, полученая из файла:\n");

            Console.Write("Нетерминалы: ");

            foreach (var nonterminal in grammar.Nonterminals)
            {
                Console.Write($" {nonterminal}");
            }

            Console.WriteLine("\n");

            Console.Write("Терминалы: ");

            foreach (var terminal in grammar.Terminals)
            {
                Console.Write($" {terminal}");
            }

            Console.WriteLine("\n");

            Console.WriteLine("Правила:\n");

            foreach (var nonterminal in grammar.Nonterminals)
            {
                foreach (var rule in nonterminal.TerminalProductions)
                {
                    Console.WriteLine($"{nonterminal} -> {rule.Literal}");
                }

                foreach (var rule in nonterminal.NonterminalProductions)
                {
                    Console.WriteLine($"{nonterminal} -> {rule.t.Literal} {rule.N.Literal}");
                }
            }

            var automaton = grammar.ToAutomaton();

            var symbols = automaton.Symbols.ToArray();
            var states = automaton.States.ToArray();

            Console.WriteLine("\nКонечный автомат, полученный по грамматике:\n");

            Console.Write("Состояния: ");

            foreach (var state in automaton.States)
            {
                Console.Write($" {state}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("Таблица переходов:\n");

            for (var i = 0; i < states.Length; ++i)
            {
                var chunk = new State[1] { states[i] };
                PrintTable(symbols, chunk);
            }

            //File.WriteAllText("C:\\Users\\Max\\Desktop\\automaton.txt", tableText.ToString());
        }

        private static void PrintTable(AutomatonSymbol[] symbols, State[] states)
        {
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

            var maxLength = table.Cast<string>().Max(s => s.Length);

            for (var i = 0; i <= symbols.Length; ++i)
            {
                if (i == 0 || i == 1)
                {
                    for (var j = 0; j <= states.Length; ++j)
                    {
                        var length = j == 0 ? 17 : maxLength;
                        var divider = new string('-', length);
                        Console.Write($"{divider}-+");
                    }

                    Console.WriteLine();
                }

                for (var j = 0; j <= states.Length; ++j)
                {
                    var length = j == 0 ? 17 : maxLength;
                    Console.Write($"{{0,{length}}} |", table[i, j]);
                }

                Console.WriteLine();                
            }

            for (var j = 0; j <= states.Length; ++j)
            {
                var length = j == 0 ? 17 : maxLength;
                var divider = new string('-', length);
                Console.Write($"{divider}-+");
            }

            Console.WriteLine();
        }
    }
}
