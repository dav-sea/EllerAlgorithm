using System;

namespace EulersAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = (10) * 2 + 1, height = (10) * 2 + 1;

            var maze = new EullerAlgorithm();
            maze.Generate();

            // Console.Write(" ");
            // for (int i = 0; i < maze.Data.GetWidth(); i++)
            //     Console.Write($"{i} ");
            // Console.WriteLine();
            for (int i = 0; i < maze.Data.GetWidth(); i++)
                Console.Write("----");
            Console.WriteLine();


            DrawData(maze.Data);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        static void DrawData(IEullerAlgorithmDataProvider data)
        {
            for (int i = 0; i < data.GetHeight(); i++)
                DrawRow(data, i);
        }
        static void DrawRow(IEullerAlgorithmDataProvider data, int row)
        {

            for (int i = 0; i < data.GetWidth(); i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write((char)((int)data.GetGroup(i, row) + 64));
                Console.Write(" ");
                if (data.ContainsRightBorder(i, row))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("  ");
                }
                else
                {
                    Console.Write("  ");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine();

            for (int i = 0; i < data.GetWidth(); i++)
            {
                if (data.ContainsBottomBorder(i, row))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("    ");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write("  ");
                    if (data.ContainsRightBorder(i, row))
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    Console.Write("  ");
                }

            }
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine();
        }


    }
}
