using System;
using System.Collections.Generic;
using System.Linq;

namespace task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите число n: ");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите число m: ");
            int m = Convert.ToInt32(Console.ReadLine());

            List<int> circularArray = Enumerable.Range(1, n).ToList();
            List<int> path = new List<int>();

            int currentIndex = 0;
            while (true)
            {
                path.Add(circularArray[currentIndex]);
                currentIndex = (currentIndex + m - 1) % n;
                if (currentIndex == 0) break;
            }

            Console.WriteLine("Путь:");
            foreach (var element in path)
            {
                Console.Write(element + " ");
            }
        }
    }
}
