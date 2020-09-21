using System;
using System.IO;
using System.Collections.Generic;

namespace CommonSymbols
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            var firstText = "";
            var secondText = "";

            if (args.Length == 2 && File.Exists(args[0]) && File.Exists(args[1]))
            {
                firstText = File.ReadAllText(args[0]);
                secondText = File.ReadAllText(args[1]);
            }
            else
            {
                Console.Write("Введите первый текст: ");
                firstText = Console.ReadLine();

                Console.Write("Введите второй текст: ");
                secondText = Console.ReadLine();
            }

            var firstTextSymbols = new HashSet<char>();
            var secondTextSymbols = new HashSet<char>();

            foreach (var symbol in firstText)
            {
                firstTextSymbols.Add(symbol);
            }

            foreach (var symbol in secondText)
            {
                secondTextSymbols.Add(symbol);
            }

            var intersection = new HashSet<char>(firstTextSymbols);
            intersection.IntersectWith(secondTextSymbols);

            var difference = new HashSet<char>(firstTextSymbols);
            difference.ExceptWith(intersection);

            Console.WriteLine("\nОбщие символы в текстах:");

            foreach (var symbol in intersection)
            {
                Console.WriteLine($"{symbol}");
            }

            Console.WriteLine("\nСимволы, встречающиеся только в первом тексте:");

            foreach (var symbol in difference)
            {
                Console.WriteLine($"{symbol}");
            }
        }
    }
}