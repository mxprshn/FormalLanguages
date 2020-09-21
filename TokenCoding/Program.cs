using System;
using System.IO;

namespace TokenCoding
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            if (args.Length != 1 || !File.Exists(args[0]))
            {
                Console.WriteLine("Путь к файлу не указан или файл не найден.");
                return;
            }

            var source = File.ReadAllText(args[0]);

            var result = Coder.Encode(source);

            Console.WriteLine("Терминалы:");

            foreach (var terminal in result.Terminals)
            {
                Console.WriteLine($"{terminal.Key, 20} | {terminal.Value, -4}");
            }

            Console.WriteLine("\nНетерминалы:");

            foreach (var nonTerminal in result.NonTerminals)
            {
                Console.WriteLine($"{nonTerminal.Key, 20} | {nonTerminal.Value, -4}");
            }

            Console.WriteLine("\nСемантики:");

            foreach (var semantics in result.Semantics)
            {
                Console.WriteLine($"{semantics.Key, 20} | {semantics.Value, -4}");
            }

            Console.WriteLine($"\nИсходный текст:\n{source}");
            Console.WriteLine($"\nРезультат:\n{result.Encoded}");
        }
    }
}