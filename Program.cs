using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string[] inputTxtFile = File.ReadAllLines(@"input.txt", Encoding.Default);
            List<string> clearInputTxtFIle = new List<string>();

            foreach (string line in inputTxtFile)
            {
                string[] splitLine = line.ToLower().Split(' ');

                for (int i = 0; i < splitLine.Count(); i++)
                {
                    string word = splitLine[i];
                    if (!string.IsNullOrEmpty(word))
                    {
                        word = word.Where(c => !char.IsPunctuation(c)).Aggregate("", (current, c) => current + c);
                        if (!string.IsNullOrEmpty(word))
                            clearInputTxtFIle.Add(word);
                    }                   
                }
            }

            Dictionary<string,int> dictionaryWordAndCount = new Dictionary<string,int>();

            foreach (var grp in clearInputTxtFIle.GroupBy(i => i))
                dictionaryWordAndCount.Add(grp.Key, grp.Count());

            using (StreamWriter file = new StreamWriter("result.txt"))
                foreach (var entry in dictionaryWordAndCount.OrderByDescending(key => key.Value))
                    file.WriteLine("[{0} {1}]", entry.Key, entry.Value);


            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
