using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTables;
using System.Linq;

namespace FormalLangLib
{
    public static class Formatter
    {
        public static void Print(this DFA dfa)
        {
            var states = dfa.States.ToArray();
            var symbols = dfa.Symbols.ToArray();

            var table = new ConsoleTable(symbols.Prepend("").ToArray());

            for (var i = 0; i < states.Length; ++i)
            {
                table.AddRow(symbols.Select(s => dfa[states[i], s]?.Name ?? "").Prepend(states[i].Name).ToArray());
            }

            table.Write(Format.Alternative);
        }

        public static void Print(this bool[,] values, State[] headers)
        {
            var table = new ConsoleTable(headers.Select(h => h.Name).Prepend("").ToArray());

            for (var i = 0; i < headers.Length; ++i)
            {
                table.AddRow(Enumerable.Range(0, values.GetLength(0)).Select(j => values[i, j] ? "+" : "-").Prepend(headers[i].Name).ToArray());
            }

            table.Write(Format.Alternative);
        }
    }
}
