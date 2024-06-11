using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class Tree
    {
        public TreeNode Root;

        public TreeNode InsertInRoot(int data)
        {
            Root = InsertRec(Root, data);
            return Root;
        }

        public TreeNode InsertRec(TreeNode root, int data)
        {
            if (root == null)
            {
                root = new TreeNode(data);
                return root;
            }

            //Der erstellte Knoten wird regelgerecht entweder rechts oder links eingefügt oder verworfen falls bereits vorhanden
            if (data < root.Data)
                root.Left = InsertRec(root.Left, data);
            else if (data > root.Data)
                root.Right = InsertRec(root.Right, data);
            else if (data == root.Data)
                Console.WriteLine("Wert " + root.Data + " doppelt, wurde nicht gespeichert!");

            return root;
        }

        public void PrintInorderRec(TreeNode root)
        {
            if (root != null)
            {
                BTreePrinter.Print(root);
            }
        }

        public void IsAVLTree(TreeNode node)
        {
            bool AVLTree= true;
            AVLTree= IsAVLTreeRec(node, AVLTree);
            
            //Gibt aus ob es generell ein AVL Baum ist
            if (AVLTree)
            {
                Console.WriteLine("AVL: yes");
            }
            else
            {
                Console.WriteLine("AVL: no");
            }
        }

        private bool IsAVLTreeRec(TreeNode node, bool AVLTree)
        {
            if (node == null)
                return AVLTree;

            //Niveauunterschied wird berechnet und Balancefaktoren ausgegeben
            int balanceFactor = node.GetBalance(node);
            if (balanceFactor < -1 || balanceFactor > 1)
            {
                Console.WriteLine("bal(" + node.Data + ") = " + balanceFactor + " (AVL violation!)");
                AVLTree = false;
            }
            else
            {
                Console.WriteLine("bal(" + node.Data + ") = " + balanceFactor);
            }

            //Rekursive Überprüfung der weiteren Knoten
            IsAVLTreeRec(node.Left, AVLTree);
            IsAVLTreeRec(node.Right, AVLTree);
            return AVLTree;
        }

        public void SearchSubtree(Tree subtree)
        {
            TreeNode root = subtree.Root;
            // If tree consists of only one node
            if (root.Left == null && root.Right == null)
            {
                SearchSingleNode(Root, root.Data, new List<int>(), Root.Data);
            }
            // Search in subtree
            else
            {
                Console.WriteLine("Search tree:");
                subtree.PrintInorderRec(subtree.Root);
                SearchTree(Root, subtree.Root, Root.Data);
            }
        }

        // history is a list of the nodes that lead to the searchNode
        // returns if searchData was found in tree
        private bool SearchSingleNode(TreeNode currentNode, int searchData, List<int> history, int rootData)
        {
            // Node not found in this path
            if (currentNode == null)
                return false;

            // Found node, print all nodes in the path to the searchNode
            if (searchData == currentNode.Data)
            {
                Console.Write(searchData + " found ");
                foreach (int data in history)
                {
                    Console.Write(data + ", ");
                }

                Console.WriteLine(searchData);
                return true;
            }

            history.Add(currentNode.Data);

            // If current node is smaller/bigger than search node, search in the left/right subtree next
            TreeNode nextNode = (searchData < currentNode.Data) ? currentNode.Left : currentNode.Right;
            bool foundNode = SearchSingleNode(nextNode, searchData, history, rootData);

            if (foundNode)
                return true;

            // It's the root node and the node was nowhere found
            if (currentNode.Data == rootData)
            {
                Console.WriteLine(searchData + " not found!");
            }

            return false;
        }

        private bool SearchTree(TreeNode currentNode, TreeNode currentSearchNode, int rootData)
        {
            if (currentNode == null)
                return false;

            bool found = false;
            TreeNode nextNode;
            // Current search node found
            if (currentNode.Data == currentSearchNode.Data)
            {
                // If this is the last node to be found in this path
                if (currentSearchNode.Left == null && currentSearchNode.Right == null)
                {
                    return true;
                }

                // Default true, because if children are null found should be true
                bool leftFound = true, rightFound = true;

                // If currentSearchNode has children, search for those children as well
                if (currentSearchNode.Left != null)
                {
                    TreeNode leftChildSearchNode = currentSearchNode.Left;
                    nextNode = (leftChildSearchNode.Data < currentNode.Data) ? currentNode.Left : currentNode.Right;
                    leftFound = SearchTree(nextNode, leftChildSearchNode, rootData);

                }

                if (currentSearchNode.Right != null)
                {
                    TreeNode rightChildSearchNode = currentSearchNode.Right;
                    nextNode = (rightChildSearchNode.Data < currentNode.Data) ? currentNode.Left : currentNode.Right;
                    rightFound = SearchTree(nextNode, rightChildSearchNode, rootData);
                }

                if (leftFound && rightFound)
                {
                    // If root node is in the search tree, print subtree found, otherwise it would never print anything
                    if (currentNode.Data == rootData)
                    {
                        Console.WriteLine("Subtree found");
                    }

                    return true;
                }
            }
            // If node was not found search for it in the children
            else
            {
                nextNode = (currentSearchNode.Data < currentNode.Data) ? currentNode.Left : currentNode.Right;
                found = SearchTree(nextNode, currentSearchNode, rootData);
            }

            // Back at root node -> print if found or not
            if (currentNode.Data == rootData)
            {
                Console.WriteLine("Subtree " + (found ? "" : "not ") + "found");
            }

            return found;
        }

        public void CalculateMinMaxAverage(out int minimum, out int maximum, out int sum, out int count)
        {
            minimum = int.MaxValue;
            maximum = int.MinValue;
            sum = 0;
            count = 0;
            CalculateMinMaxAverageRec(Root, ref minimum, ref maximum, ref sum, ref count);
        }

        private void CalculateMinMaxAverageRec(TreeNode node, ref int min, ref int max, ref int sum, ref int count)
        {
            if (node == null)
                return;

            // Aktualisiert Minimum
            if (node.Data < min)
                min = node.Data;

            // Aktualisiert Maximum
            if (node.Data > max)
                max = node.Data;

            // Addiert den Wert zum Gesamtsummenzähler
            sum += node.Data;

            // Zählt die Anzahl der Knoten
            count++;

            // Rekursiv zu linkem und rechtem Teilbaum gehen
            CalculateMinMaxAverageRec(node.Left, ref min, ref max, ref sum, ref count);
            CalculateMinMaxAverageRec(node.Right, ref min, ref max, ref sum, ref count);
        }
    }
}