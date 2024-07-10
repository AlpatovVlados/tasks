using System;
using System.IO;

namespace task2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var paths = new string[2];
            switch (args.Length)
            {
                case 0:
                    paths[0] = ReadPath("круга");
                    paths[1] = ReadPath("точек");
                    break;
                case 1:
                    paths[0] = args[0];
                    paths[1] = ReadPath("точек");
                    break;
                case 2:
                    paths = args;
                    break;
                default:
                    Console.WriteLine("Использование: task2.exe <circle_file> <points_file>");
                    return;
            }

            if (!File.Exists(paths[0]) || !File.Exists(paths[1]))
            {
                Console.WriteLine("Использование: task2.exe <circle_file> <points_file>");
                return;
            }

            var circle = ReadFile(paths[0]);
            var points = ReadFile(paths[1]);

            var circleCenterX = Convert.ToInt32(circle[0].Split(' ')[0]);
            var circleCenterY = Convert.ToInt32(circle[0].Split(' ')[1]);
            var circleRadius = Convert.ToInt32(circle[1]);



            foreach (var point in points)
            {
                var pointX = Convert.ToInt32(point.Split(' ')[0]);
                var pointY = Convert.ToInt32(point.Split(' ')[1]);

                var distance = Math.Sqrt(Math.Pow(pointX - circleCenterX, 2) + Math.Pow(pointY - circleCenterY, 2));

                Console.WriteLine(
                    Math.Abs(distance - circleRadius) < 0.0001 ? 0
                    : distance < circleRadius ? 1
                    : 2);
            }
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


