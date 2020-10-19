using System;

namespace Squares
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Console.Write("Введите размер квадрата: ");
            var n = int.Parse(Console.ReadLine());

            int sum = 0;

            for (int i = 1; i <= n; ++i)
            {
                for (int j = 1; j <= n; ++j)
                {
                    sum += (n - i + 1) * (n - j + 1);
                }
            }

            Console.WriteLine($"Ответ: {sum}");
        }
    }
}
