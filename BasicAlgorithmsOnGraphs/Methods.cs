using System;
using System.Collections.Generic;
using System.Text;
using Input;

namespace BasicAlgorithmsOnGraphs
{
    public class Methods
    {
        public static void TieredParallel()
        {
            try
            {
                Console.WriteLine("Ввести матрицу смежности с помощью ДСЧ - 1, авто - 0");
                int input = UserInput.InputInt(0, 1);
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
                            int size = UserInput.InputInt(0, 10);
                            int[,] matrixAdjacency = new int[size, size];
                            for (int i = 0; i < size; i++)
                            {
                                Console.WriteLine($"Введите данные для {i + 1} строки:");
                                for (int j = 0; j < size; j++)
                                {
                                    matrixAdjacency[i, j] = UserInput.InputInt(0, 1);
                                }
                            }
                            graph = new Graph(matrixAdjacency);
                            break;
                        }
                }
                graph.PrintMatrixAdjacency();
                graph.Print();
                Console.WriteLine();

                Console.WriteLine("Ярусно-параллельная форма:");
                graph.TieredParallel();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public static void Deystras()
        {
            try
            {
                Console.WriteLine("Ввести матрицу смежности с помощью ДСЧ - 1, авто - 0");
                int input = UserInput.InputInt(0, 1);
                OrientedGraph orientedGraph = null;
                switch (input)
                {
                    case 0:
                        {
                            orientedGraph = new OrientedGraph();
                            break;
                        }
                    case 1:
                        {
                            Console.WriteLine("Введите количество вершин (n <= 10)");
                            int size = UserInput.InputInt(0, 10);
                            int[,] matrixAdjacency = new int[size, size];
                            for (int i = 0; i < size; i++)
                            {
                                Console.WriteLine($"Введите данные для {i + 1} строки:");
                                for (int j = 0; j < size; j++)
                                {
                                    matrixAdjacency[i, j] = UserInput.InputInt(0, int.MaxValue);
                                }
                            }
                            orientedGraph = new OrientedGraph(matrixAdjacency);
                            break;
                        }
                }
                orientedGraph.PrintMatrixAdjacency();
                orientedGraph.Deystras();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void Kruskal()
        {
            try
            {
                Console.WriteLine("Ввести матрицу смежности с помощью ДСЧ - 1, авто - 0");
                int input = UserInput.InputInt(0, 1);
                WeightedGraph weightedGraph = null;
                switch (input)
                {
                    case 0:
                        {
                            weightedGraph = new WeightedGraph();
                            break;
                        }
                    case 1:
                        {
                            Console.WriteLine("Введите количество вершин (n <= 10)");
                            int size = UserInput.InputInt(0, 10);
                            int[,] matrixAdjacency = new int[size, size];
                            for (int i = 0; i < size; i++)
                            {
                                Console.WriteLine($"Введите данные для {i + 1} строки:");
                                for (int j = i + 1; j < size; j++)
                                {
                                    Console.WriteLine($"Введите данные для {j + 1} столбца:");
                                    int weight = UserInput.InputInt(0, int.MaxValue);
                                    matrixAdjacency[i, j] = weight;
                                    matrixAdjacency[j, i] = weight;
                                }
                            }
                            weightedGraph = new WeightedGraph(matrixAdjacency);
                            break;
                        }
                }
                weightedGraph.PrintMatrixAdjacency();
                weightedGraph.Kruskal();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
