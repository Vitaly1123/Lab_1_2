using System;
using System.Diagnostics;
using System.Linq;


delegate bool Filter(int number);
class Program
{
    static void Main(string[] args)
    {
        int[] array = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
        Console.Write("k: ");
        int k = int.Parse(Console.ReadLine());
        List<int> ints = new List<int>();

        Filter condition = num => num % k == 0;

        int[] filteredArr1 = array.Where(num => condition(num)).ToArray();
        Console.WriteLine("Using Where: " + string.Join(", ", filteredArr1));

        int[] filteredArr2 = FilterArray(array, condition);
        Console.WriteLine("Manual: " + string.Join(", ", filteredArr2));
    }

    static int[] FilterArray(int[] arr, Filter condition)
    {
        int count = 0;
        foreach (var num in arr)
            if (condition(num)) count++;

        int[] result = new int[count];
        int index = 0;
        foreach (var num in arr)
            if (condition(num))
                result[index++] = num;

        return result;
    }

}