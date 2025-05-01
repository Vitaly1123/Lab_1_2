using System;
using System.Linq;
using System.Text;

namespace Task_2
{
    class Program
    {
        delegate int[] FilterDelegate(int[] array, int k);

        static int[] FilterWithWhere(int[] array, int k) => array.Where(x => x % k == 0).ToArray();

        static int[] FilterCustom(int[] array, int k)
        {
            int[] temp = new int[array.Length];
            int count = 0;

            foreach (var num in array)
                if (num % k == 0)
                    temp[count++] = num;

            int[] result = new int[count];
            Array.Copy(temp, result, count);
            return result;
        }

        static void Main()
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.Write("Введіть число k: ");
            int k = int.Parse(Console.ReadLine());
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            FilterDelegate filter = FilterWithWhere;
            Console.WriteLine("Через Where: " + string.Join(", ", filter(array, k)));

            filter = FilterCustom;
            Console.WriteLine("Кастомний: " + string.Join(", ", filter(array, k)));
        }
    }
}
