﻿using System;
using System.IO;


namespace ConsoleApplication1
{
    public class FileHandler1
    {
        public static Tree GetTreeFromFile(string inputFile)
        {
            string inputPath = Path.Combine("../../InputFiles/", inputFile);
            
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("File not found!");
                return null;
            }

            Tree tree = new Tree();
            
            using (StreamReader sr = File.OpenText(inputPath))
            {
                string line;
                int lineCounter = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    //Correction Redundanzen
                    if (int.TryParse(line, out int input))
                    {
                        tree.InsertInRoot(input);
                    }
                    else
                    {
                        Console.WriteLine("Line " + lineCounter + " is not an integer");
                        return null;
                    }
                    lineCounter++;
                }
            }

            return tree;
        }
    }
}