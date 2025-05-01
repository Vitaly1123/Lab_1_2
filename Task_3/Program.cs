using System;
using System.Text;

namespace Task_3
{
    delegate double dlgGetSeries(int i);
    class Program
    {


        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.Write("Введіть кількість знаків після коми: ");
            int decimalPlaces = Convert.ToInt32(Console.ReadLine());
            double precision = Math.Pow(10, -decimalPlaces);

            dlgGetSeries Sum1 = GetSeriesEl1;
            dlgGetSeries Sum2 = GetSeriesEl2;
            dlgGetSeries Sum3 = GetSeriesEl3;

            Console.WriteLine("Сума 1-го ряду: " + Calculate(Sum1, precision));
            Console.WriteLine("Сума 2-го ряду: " + Calculate(Sum2, precision));
            Console.WriteLine("Сума 3-го ряду: " + Calculate(Sum3, precision));
        }

        static double Calculate(dlgGetSeries dlg, double precision)
        {
            double sum = 0;
            int i = 1;
            double current;
            do
            {
                current = dlg(i);
                sum += current;
                i++;
            }
            while (Math.Abs(current) > precision);

            return sum;
        }

        static double GetSeriesEl1(int i)
        {
            return 1.0 / Math.Pow(2, i - 1);
        }

        static double GetSeriesEl2(int i)
        {
            return 1.0 / Factorial(i);
        }

        static double GetSeriesEl3(int i)
        {
            return Math.Pow(-1, i) * 1.0 / Math.Pow(2, i - 1);
        }
        static double Factorial(int n)
        {
            double result = 1;
            for (int i = 2; i <= n; i++) result *= i;
            return result;
        }
    }
}
