using System;
using System.IO;

namespace Concordance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            if (args.Length < 1 || !File.Exists(args[0]))
            {
                Console.WriteLine("Путь к файлу не указан или файл не найден.");
                return;
            }

            var removeWordConnectors = false;

            if (args.Length == 2 && args[1] == "-r")
            {
                removeWordConnectors = true;
            }

            var source = File.ReadAllText(args[0]);

            var concordance = new Concordance();
            concordance.ReadText(source, removeWordConnectors);

            Console.WriteLine($"Исходный текст:\n\n{source}\n");

            concordance.Print();
        }
    }
}
