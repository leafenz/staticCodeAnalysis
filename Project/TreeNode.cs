using System;

namespace ConsoleApplication1
{
    public class TreeNode
    {
        public readonly int Data;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(int data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
        
        //Funktion zur Berechnung des Balancefaktors 
        public int GetBalance(TreeNode node)
        {
            if (node == null)
                return 0;

            int leftHeight = Height(node.Left);
            int rightHeight = Height(node.Right);

            return rightHeight- leftHeight;
        }

        private int Height(TreeNode node)
        {
            if (node == null)
                return 0;

            int leftHeight = Height(node.Left);
            int rightHeight = Height(node.Right);

            // die größere Höhe wird zurückgegeben +1 wegen dem aktuellen knoten
            return Math.Max(leftHeight, rightHeight) + 1;
        }

    }
}