using System;

namespace ConsoleApplication1
{
    static class TreeCheck
    {
    
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Eingabeoptionen:");
                Console.WriteLine("  x                                        =>  beendet Programm");
                Console.WriteLine("  treecheck filename1.txt [filename2.txt]  =>  überprüft trees");
                Console.WriteLine("  test                                     =>  führt testfaelle aus");
                Console.WriteLine();
                Console.Write("Eingabe: ");

                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    continue;

                if (input.Equals("x"))
                    return;

                if (input.Equals("test"))
                {
                    TestAll();
                    continue;
                }


                if (!input.StartsWith("treecheck "))
                    continue;

                string fileName = input.Substring("treecheck ".Length).Trim();

                if (string.IsNullOrEmpty(fileName))
                    continue;

                // Split filenames
                string[] fileNames = fileName.Split(' ');
                if (fileNames.Length < 1)
                    continue;

                Tree tree = FileHandler1.GetTreeFromFile(fileNames[0]);
                if (tree == null)
                    continue;

                // Search subtree
                if (fileNames.Length >= 2)
                {
                    Tree searchTree = FileHandler1.GetTreeFromFile(fileNames[1]);
                    if (searchTree == null)
                        continue;

                    tree.SearchSubtree(searchTree);
                }

                tree.PrintInorderRec(tree.Root);
                tree.IsAvlTree(tree.Root);

                // Berechnung von Minimum, Maximum und Durchschnitt
                //Entfernen reduntanter Code
                tree.CalculateMinMaxAverage(out int minimum, out int maximum, out int sum, out int count);

                double average = (double)sum / count;
                Console.WriteLine($"Minimum: {minimum}");
                Console.WriteLine($"Maximum: {maximum}");
                Console.WriteLine($"Durchschnitt: {average}");
                Console.WriteLine();
            }
        }

        static void TestAll()
        {
            Tree tree = FileHandler1.GetTreeFromFile("input.txt");
            Tree searchNode = FileHandler1.GetTreeFromFile("singleSearch.txt");
            Tree searchTree = FileHandler1.GetTreeFromFile("subtreeSearch.txt");
            tree.SearchSubtree(searchNode);
            tree.SearchSubtree(searchTree);
            tree.PrintInorderRec(tree.Root);
            tree.IsAvlTree(tree.Root);

            // Berechnung von Minimum, Maximum und Durchschnitt
            // Entfernen von reduntantem Code
            tree.CalculateMinMaxAverage(out int minimum, out int maximum, out int sum, out int count);

            double average = (double)sum / count;
            Console.WriteLine($"Minimum: {minimum}");
            Console.WriteLine($"Maximum: {maximum}");
            Console.WriteLine($"Durchschnitt: {average}");
            Console.WriteLine();
        }
    }
}