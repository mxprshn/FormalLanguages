using System;
using FormalLangLib;

namespace MinimalAutomaton
{
    class Program
    {
        static void Main(string[] args)
        {
            var automaton = new DFA();

            var q0 = automaton.AddState("q0", true);
            var q1 = automaton.AddState("q1", false);
            var q2 = automaton.AddState("q2", false);
            var q3 = automaton.AddState("q3", false);
            var q4 = automaton.AddState("q4", false);
            var q5 = automaton.AddState("q5", false);
            var q6 = automaton.AddState("q6", false);

            automaton[q0, "d"] = q1;

            automaton[q1, "d"] = q1;
            automaton[q1, "."] = q2;
            automaton[q1, "e"] = q3;
            automaton[q1, "eps"] = q0;

            automaton[q2, "d"] = q4;

            automaton[q3, "d"] = q5;
            automaton[q3, "+-"] = q6;

            automaton[q4, "d"] = q4;
            automaton[q4, "e"] = q3;
            automaton[q4, "eps"] = q0;

            automaton[q5, "d"] = q5;
            automaton[q5, "eps"] = q0;

            automaton[q6, "d"] = q5;

            //var minimal = automaton.Minimize();

            var automaton2 = new DFA();
            var a = automaton2.AddState("a", false);
            var b = automaton2.AddState("b", false);
            var c = automaton2.AddState("c", true);
            var d = automaton2.AddState("d", false);
            var e = automaton2.AddState("e", true);

            automaton2[a, "0"] = b;
            automaton2[a, "1"] = d;
            automaton2[b, "0"] = b;
            automaton2[b, "1"] = c;
            automaton2[c, "0"] = d;
            automaton2[c, "1"] = e;
            automaton2[d, "0"] = d;
            automaton2[d, "1"] = e;
            automaton2[e, "0"] = b;
            automaton2[e, "1"] = c;

            Console.WriteLine("Исходный автомат:");
            automaton.Print();
            var minimal = automaton.Minimize();

            if (minimal.wasMinimal)
            {
                Console.WriteLine("Автомат уже был минимальным, минимизация невозможна.");
            }
            else
            {
                Console.WriteLine("Минимальный автомат:");
                minimal.minimal.Print();
            }
        }
    }
}
