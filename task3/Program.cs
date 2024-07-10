using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.IO;
using System;

namespace task3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var filesPath = args.Length < 1 ? ReadPath() : args[0];

            var testsPath = Path.Combine(filesPath, Files[0]);
            var valuesPath = Path.Combine(filesPath, Files[1]);

            var values = ParseTestResults(valuesPath);
            var tests = ParseTests(testsPath);

            tests.Tests = tests.Tests.ToList().Select(test => UpdateTestValue(test, values)).ToArray();
            WriteReport(tests, Path.Combine(filesPath, ResultFileName));
        }

        private static Test UpdateTestValue(Test test, TestResultContainer values)
        {
            var result = values.Values.FirstOrDefault(v => v.Id == test.Id);
            test.Value = result.Equals(default(TestResult)) ? string.Empty : result.Value;
            if (test.Values == null) return test;

            test.Values = test.Values.ToList().Select(c => UpdateTestValue(c, values)).ToArray();
            return test;
        }

        private static TestContainer ParseTests(string filePath)
        {
            var content = ReadFile(filePath);
            return JsonSerializer.Deserialize<TestContainer>(content);
        }

        private static TestResultContainer ParseTestResults(string filePath)
        {
            var content = ReadFile(filePath);
            return JsonSerializer.Deserialize<TestResultContainer>(content);
        }

        private static void WriteReport(TestContainer tests, string filePath)
        {
            var content = JsonSerializer.Serialize(tests, SerializeOptions);
            File.WriteAllText(filePath, content);
        }

        private static string ReadPath()
        {
            while (true)
            {
                var fileNames = string.Join(", ", Files);
                Console.Write($"Укажите путь к файлам {fileNames}: ");
                var path = Console.ReadLine();
                if (!Directory.Exists(path))
                {
                    Console.WriteLine("Путь указан неверно, попробуйте еще раз.");
                    continue;
                }

                var files = Directory.GetFiles(path);
                var contains = Files.All(file => files.Contains(Path.Combine(path, file)));
                if (contains) return path;

                Console.WriteLine("В папке отсутствуют необходимые файлы, попробуйте еще раз.");
            }
        }

        private static string ReadFile(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ошибка при чтении файла {filePath}: {e.Message}");
                return string.Empty;
            }
        }

        private const string ResultFileName = "report.json";
        private static readonly List<string> Files = new List<string>() { "tests.json", "values.json" };
        private static readonly JsonSerializerOptions SerializeOptions = new JsonSerializerOptions() { WriteIndented = true };
    }

    public struct Test
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("values")]
        public Test[] Values { get; set; }
    }

    public struct TestResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public struct TestContainer
    {
        [JsonPropertyName("tests")]
        public Test[] Tests { get; set; }
    }

    public struct TestResultContainer
    {
        [JsonPropertyName("values")]
        public TestResult[] Values { get; set; }
    }
}