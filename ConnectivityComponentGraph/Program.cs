using System;
using System.Collections.Generic;

namespace ConnectivityComponentGraph
{
    class Program
    {
         static void Main(string[] args)
        {

            Console.WriteLine("Ввести матрицу смежности с помощью ДСЧ - 1, авто - 0");
            int input = Input.InputInt(0, 1);
            Graph graph = null;
            switch (input)
            {
                case 0:
                    {
                        graph = new Graph();
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("Введите количество вершин (n <= 10)");
                        int size = Input.InputInt(0, 10);
                        int[,] matrixAdjacency = new int[size, size];
                        for (int i = 0; i < size; i++)
                        {
                            Console.WriteLine($"Введите данные для {i+1} строки:");
                            for (int j = 0; j < size; j++)
                            {
                                matrixAdjacency[i, j] = Input.InputInt(0, 1);
                            }
                        }
                        graph = new Graph(matrixAdjacency);
                        break;
                    }
            }

            Console.WriteLine("Матрица смежности:");
            graph.PrintMatrixAdjacency();

            List<List<char>> componentsStrongConnectivity;
            componentsStrongConnectivity = graph.StrongConnectivityComponents();

            Console.WriteLine("Компоненты сильной связности:");

            for (int i = 0; i < componentsStrongConnectivity.Count; i++)
            {
                Console.Write("{ ");
                for (int j = 0; j < componentsStrongConnectivity[i].Count; j++)
                {
                    Console.Write(componentsStrongConnectivity[i][j]);
                    if (j != componentsStrongConnectivity[i].Count - 1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine(" }");
            }
        }
    }
}
