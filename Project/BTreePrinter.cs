using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class BTreePrinter
    {
        class NodeInfo
        {
            public TreeNode Node;
            public string Text;
            public int StartPos;

            public int Size
            {
                get { return Text.Length; }
            }

            public int EndPos
            {
                get { return StartPos + Size; }
                set { StartPos = value - Size; }
            }

            public NodeInfo Parent, Left, Right;
        }

        public static void Print(TreeNode root, int topMargin = 2, int LeftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var Data = new NodeInfo { Node = next, Text = next.Data.ToString(" 0 ") };
                if (level < last.Count)
                {
                    Data.StartPos = last[level].EndPos + 1;
                    last[level] = Data;
                }
                else
                {
                    Data.StartPos = LeftMargin;
                    last.Add(Data);
                }

                if (level > 0)
                {
                    Data.Parent = last[level - 1];
                    if (next == Data.Parent.Node.Left)
                    {
                        Data.Parent.Left = Data;
                        Data.EndPos = Math.Max(Data.EndPos, Data.Parent.StartPos);
                    }
                    else
                    {
                        Data.Parent.Right = Data;
                        Data.StartPos = Math.Max(Data.StartPos, Data.Parent.EndPos);
                    }
                }

                next = next.Left ?? next.Right;
                for (; next == null; Data = Data.Parent)
                {
                    Print(Data, rootTop + 2 * level);
                    if (--level < 0) break;
                    if (Data == Data.Parent.Left)
                    {
                        Data.Parent.StartPos = Data.EndPos;
                        next = Data.Parent.Node.Right;
                    }
                    else
                    {
                        if (Data.Parent.Left == null)
                            Data.Parent.EndPos = Data.StartPos;
                        else
                            Data.Parent.StartPos += (Data.StartPos - Data.Parent.EndPos) / 2;
                    }
                }
            }

            
            int yPos = rootTop + 2 * last.Count - 1;
            EnsureBufferSize(0, yPos);
            Console.SetCursorPosition(0, yPos);
        }

        private static void Print(NodeInfo Data, int top)
        {
            SwapColors();
            Print(Data.Text, top, Data.StartPos);
            SwapColors();
            if (Data.Left != null)
            {
                EnsureBufferSize(0 , top + 1);
                PrintLink(top + 1, "┌", "┘", Data.Left.StartPos + Data.Left.Size / 2, Data.StartPos);
            }

            if (Data.Right != null)
            {
                EnsureBufferSize(0 , top + 1);
                PrintLink(top + 1, "└", "┐", Data.EndPos - 1, Data.Right.StartPos + Data.Right.Size / 2);
            }
                
        }

        private static void PrintLink(int top, string start, string end, int startPos, int endPos)
        {
            Print(start, top, startPos);
            Print("─", top, startPos + 1, endPos);
            Print(end, top, endPos);
        }

        private static void Print(string s, int top, int Left, int Right = -1)
        {
            EnsureBufferSize(Left, top);
            Console.SetCursorPosition(Left, top);
            if (Right < 0) Right = Left + s.Length;
            while (Console.CursorLeft < Right) Console.Write(s);
        }

        private static void SwapColors()
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
        }
        
        private static void EnsureBufferSize(int x, int y)
        {
            if (Console.BufferHeight <= y)
            {
                Console.SetBufferSize(Console.BufferWidth, y + 1);
            }
            if (Console.BufferWidth <= x)
            {
                Console.SetBufferSize(x + 1, Console.BufferHeight);
            }
        }

    }
}