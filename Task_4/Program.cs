using System;
using System.Text;

namespace Task_4
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.WriteLine("Вводьте послідовність виду 0 х, 1 х або 2 х (де х - число)");
            Console.WriteLine("0 -- sqrt(abs(x))");
            Console.WriteLine("1 -- x^3");
            Console.WriteLine("2 -- x + 3.5");

            Func<double, double>[] functions =
            {
                x => Math.Sqrt(Math.Abs(x)),
                x => Math.Pow(x, 3.0),
                x => x + 3.5
            };

            while (true)
            {
                try
                {
                    string[] input = Console.ReadLine().Trim().Split();
                    Console.WriteLine(functions[int.Parse(input[0])](double.Parse(input[1])));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Сталася помилка {ex.Message}");
                    Console.WriteLine("Щоб вийти, натисніть будь-яку кнопку");
                    Console.ReadKey();
                    break;
                }
            }
        }
    }
}