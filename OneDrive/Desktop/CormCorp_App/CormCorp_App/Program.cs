using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace CormCorp_App
{
    class Program
    {
       

        static void Main(string[] args)
        {
            var watch = new Stopwatch(); //Creating the stop watch to get the time taken to execute the code
            string novelFile = ""; //the novel file to assign the novel to
            watch.Start();
            //
            try
            {
                string inFileName = @"Docs\2600-0.txt";
                StreamReader reader = new StreamReader(inFileName);
                novelFile = File.ReadAllText(inFileName); // Read every Word in the novelFile
               
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Regex lookforwords = new Regex("[a-zA-Z0-9]"); //This Regex check for every valid word in the novelFile

            string text = novelFile.ToLower();   //Every word in the novelFile is converted to lowercase for comparison

            string value = (@"\b[a-zA-Z]{1,}\b"); //Get the valid words from the novelFile, min words is 1 letter word such as i and a

            MatchCollection matches = Regex.Matches(text, @value); 

            List<string> novelFileList = new List<string>(); //generic list of novelFile list
            for (int i = 0; i < matches.Count; i++)
            {
                novelFileList.Add(matches[i].ToString()); //from the nodelList Add every valid word to the novellList
            }

            List<string> distinct = novelFileList.Distinct().ToList();  //new list for distinct words

            int distinctValueCount = distinct.Count;  
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>(); //Create the generic Dictionary to add every dinstict word and count
            int count = 0;
            for (int i = 0; i < distinctValueCount; i++)
             {
                for (int a = 0; a < novelFileList.Count; a++)

                {
                    if (distinct[i] == novelFileList[a])

                    {
                       count++; //Count occurents of distinct word in the Novel list
                    }

                }
                keyValuePairs.Add(distinct[i], count); 
                count = 0; //initialise the count after every distinct word count

            }


            var WordCount = (from p in keyValuePairs
                             orderby p.Value descending
                             select p);//use LINQ to select words with highest word count
           var listTop50 = WordCount.Take(50); //Take first top 50 highest word count
           
            Console.WriteLine("---The top 50 words by count---\n");
            listTop50.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);

            Console.WriteLine("----top 50 words by count where the word length is longer than 6 characters----\n");
            
            var listLength6 = WordCount.Where(x => x.Key.Length > 6).Take(50); //take first top 50 with length >6


           listLength6.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);
           watch.Stop();
           Console.WriteLine($"Execution Time: {(watch.ElapsedMilliseconds) / 1000} s"); //Display the execution time
           Console.ReadLine(); //Read
            
        }
    }
}
