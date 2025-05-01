using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SortTestingLab
{
    class Testing
    {
        // Делегат для визначення методу сортування
        public delegate void SortMethod<T>(T[] array) where T : IComparable<T>;

        // Делегат для перевірки часу виконання сортування
        public delegate bool TimeVerifier(long etalonTime, long studentTime);

        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            GenerateTestArrays();// Генеруємо тестові масиви

            var testArrays = ReadTestArrays(); // Зчитуємо тестові масиви

            Console.WriteLine("\nСортування вибором:");
            VerifySorting(
                SelectionSortEtalon,
                SelectionSortStudent,
                testArrays,
                IsTimeAcceptable
            );

            Console.WriteLine("\nСортування змішуванням:");
            VerifySorting(
                ShakerSortEtalon,
                ShakerSortStudent,
                testArrays,
                IsTimeAcceptable
            );
        }
        // Еталонний алгоритм сортування вибором
        static void SelectionSortEtalon<T>(T[] array) where T : IComparable<T>
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j].CompareTo(array[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    T temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                }
            }
        }
        // Еталонний алгоритм сортування шейкерним методом (двостороннє сортування)
        static void ShakerSortEtalon<T>(T[] array) where T : IComparable<T>
        {
            int left = 0;
            int right = array.Length - 1;

            while (left < right)
            {
                for (int i = left; i < right; i++)
                {
                    if (array[i].CompareTo(array[i + 1]) > 0)
                    {
                        T temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                    }
                }
                right--;

                for (int i = right; i > left; i--)
                {
                    if (array[i].CompareTo(array[i - 1]) < 0)
                    {
                        T temp = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = temp;
                    }
                }
                left++;
            }
        }
        // Варіант сортування студента (включає штучну затримку)
        static void SelectionSortStudent<T>(T[] array) where T : IComparable<T>
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j].CompareTo(array[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    T temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                }

            }
            System.Threading.Thread.Sleep(4);

        }
        // Некоректний варіант шейкерного сортування (зміна напрямку)
        static void ShakerSortStudent<T>(T[] array) where T : IComparable<T>
        {
            int left = 0;
            int right = array.Length - 1;


            while (left < right)
            {
                // desc order
                for (int i = left; i < right; i++)
                {
                    if (array[i].CompareTo(array[i + 1]) < 0) // Зміна напряму сортування
                    {
                        T temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                    }
                }
                right--;

                for (int i = right; i > left; i--)
                {
                    if (array[i].CompareTo(array[i - 1]) > 0)
                    {
                        T temp = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = temp;
                    }
                }
                left++;
            }
        }

        static void GenerateTestArrays()
        {
            // Створює директорію для збереження тестових масивів
            Directory.CreateDirectory("TestArrays");

            // Генерує масиви різних розмірів і зберігає їх у файлах
            int[] small = GenerateRandomArray(20, 0, 100);
            File.WriteAllText("TestArrays/small_random.txt", string.Join(" ", small));

            int[] medium = GenerateRandomArray(500, 0, 1000);
            File.WriteAllText("TestArrays/medium_random.txt", string.Join(" ", medium));

            int[] large = GenerateRandomArray(5000, 0, 10000);
            File.WriteAllText("TestArrays/large_random.txt", string.Join(" ", large));

            int[] almostSorted = GenerateAlmostSortedArray(1000, 10);
            File.WriteAllText("TestArrays/almost_sorted.txt", string.Join(" ", almostSorted));

            int[] reverseSorted = GenerateReverseSortedArray(1000);
            File.WriteAllText("TestArrays/reverse_sorted.txt", string.Join(" ", reverseSorted));
        }

        static int[] GenerateRandomArray(int size, int min, int max)
        {
            // Генерує випадковий масив заданого розміру в межах мінімального та максимального значення
            Random random = new Random();
            int[] array = new int[size];

            for (int i = 0; i < size; i++)
                array[i] = random.Next(min, max);

            return array;
        }

        static int[] GenerateAlmostSortedArray(int size, int swapCount)
        {
            // Генерує майже відсортований масив і випадково змінює деякі елементи
            Random random = new Random();
            int[] array = new int[size];

            for (int i = 0; i < size; i++)
                array[i] = i;

            for (int i = 0; i < swapCount; i++)
            {
                int index1 = random.Next(size);
                int index2 = random.Next(size);

                int temp = array[index1];
                array[index1] = array[index2];
                array[index2] = temp;
            }

            return array;
        }

        static int[] GenerateReverseSortedArray(int size)
        {
            // Генерує масив у зворотному порядку
            int[] array = new int[size];

            for (int i = 0; i < size; i++)
                array[i] = size - i - 1;

            return array;
        }

        static Dictionary<string, int[]> ReadTestArrays()
        {
            // Зчитує збережені тестові масиви з файлів
            Dictionary<string, int[]> testArrays = new Dictionary<string, int[]>();

            foreach (string file in Directory.GetFiles("TestArrays"))
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string content = File.ReadAllText(file);
                int[] array = content.Split(' ')
                    .Select(s => int.Parse(s))
                    .ToArray();
                testArrays.Add(fileName, array);
            }

            return testArrays;
        }

        static bool IsTimeAcceptable(long etalonTime, long studentTime) => studentTime < (etalonTime * 1.5);


        static long MeasureSortingTime<T>(SortMethod<T> sortMethod, T[] array) where T : IComparable<T>
        {
            // Вимірює час виконання сортування
            T[] arrayCopy = (T[])array.Clone();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                sortMethod(arrayCopy);
                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Console.WriteLine($"Помилка: {ex.Message}");
                return -1;
            }
        }

        static bool AreArraysEqual<T>(T[] array1, T[] array2) where T : IComparable<T>
        {
            // Порівнює два масиви на рівність
            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i].CompareTo(array2[i]) != 0)
                    return false;
            }

            return true;
        }

        static T[] PerformSorting<T>(SortMethod<T> sortMethod, T[] array) where T : IComparable<T>
        {
            // Виконує сортування та повертає відсортований масив
            T[] arrayCopy = (T[])array.Clone();

            try
            {
                sortMethod(arrayCopy);
                return arrayCopy;
            }
            catch (Exception)
            {
                return null;
            }
        }
        // Метод для перевірки сортувань
        static void VerifySorting<T>(
            SortMethod<T> etalonMethod,
            SortMethod<T> studentMethod,
            Dictionary<string, T[]> testArrays,
            TimeVerifier timeVerifier) where T : IComparable<T>
        {
            foreach (var testCase in testArrays)
            {
                Console.WriteLine($"\n Набір даних : {testCase.Key}");

                // Вимірюємо час виконання еталонного сортування
                long etalonTime = MeasureSortingTime(etalonMethod, testCase.Value);

                if (etalonTime == -1)
                {
                    Console.WriteLine("Помилка виконання в стандартному сортуванні. Тест пропущено.");
                    continue;
                }

                // Вимірюємо час виконання студентського сортування
                long studentTime = MeasureSortingTime(studentMethod, testCase.Value);

                if (studentTime == -1)
                {
                    Console.WriteLine("Результат: помилка виконання");
                    continue;
                }

                // Отримуємо результати сортувань
                T[] etalonResult = PerformSorting(etalonMethod, testCase.Value);
                T[] studentResult = PerformSorting(studentMethod, testCase.Value);

                // Перевіряємо правильність результатів
                bool resultsMatch = AreArraysEqual(etalonResult, studentResult);
                bool timeAcceptable = timeVerifier(etalonTime, studentTime);

                // Виводимо час роботи алгоритмів
                Console.WriteLine($"Стандартний час сортування: {etalonTime} мс");
                Console.WriteLine($"Студентський час сортування: {studentTime} мс");

                // Визначаємо підсумковий результат
                string resultMessage = "Прийнято";
                if (!resultsMatch)
                {
                    resultMessage = "Неправильна відповідь";
                }
                else if (!timeAcceptable)
                {
                    resultMessage = "Час перевищено";
                }

                Console.WriteLine($"Результат: {resultMessage}");
            }
        }
    }
}