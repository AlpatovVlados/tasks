using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace task4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var path = args.Length < 1 ? ReadPath("чисел") : args[0];
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден");
                return;
            }

            var content = ReadFile(path);
            var numbers = content.ToList().Select(int.Parse).ToArray();
            var median = numbers.OrderBy(x => x).ElementAt(numbers.Length / 2);

            var steps = numbers.Sum(num => Math.Abs(num - median));
            Console.WriteLine(steps);

            Console.ReadLine();
        }

        private static string ReadPath(string name)
        {
            while (true)
            {
                Console.Write($"Укажите путь к файлу {name}: ");
                var path = Console.ReadLine();
                if (File.Exists(path)) return path;

                Console.WriteLine("Путь указан неверно, попробуйте еще раз.");
            }
        }

        private static string[] ReadFile(string path)
        {
            try
            {
                return File.ReadAllLines(path);
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
