using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAlgorithmsOnGraphs
{
    public class Graph
    {
        public Random random = new Random();
        protected int[,] matrixAdjacency; // матрица смежности
        public int NumVertex { get => matrixAdjacency.GetLength(0); }
        public virtual int NumEdges
        {
            get
            {
                int numEdges = 0;
                for (int i = 0; i < NumRow; i++)
                {
                    for (int j = 0; j < NumCol; j++)
                    {
                        if (matrixAdjacency[i,j] != 0)
                        {
                            numEdges++;
                        }
                    }
                }
                return numEdges;
            }
        }

        public virtual int[,] Edges
        {
            get
            {
                int[,] edges = new int[NumEdges, 3];
                int num = 0;
                for (int i = 0; i < NumRow; i++)
                {
                    for (int j = 0; j < NumCol; j++)
                    {
                        if (matrixAdjacency[i, j] != 0)
                        {
                            edges[num, 0] = i;
                            edges[num, 1] = j;
                            edges[num, 2] = matrixAdjacency[i, j];
                            num++;
                        }
                    }
                }
                return edges;
            }
        }
        public int NumRow { get => matrixAdjacency.GetLength(0);}
        public int NumCol { get => matrixAdjacency.GetLength(1);}
    public Graph()
        {
            switch (random.Next(4))
            {
                case 0:
                    {
                        matrixAdjacency = new int[5, 5]
                        {
                            { 0, 1, 0, 0, 1 },
                            { 1, 0, 0, 0, 1 },
                            { 0, 0, 0, 1, 0 },
                            { 0, 0, 1, 0, 1 },
                            { 1, 1, 0, 1, 0 }
                        }; // матрица смежности для 5 вершин
                        break;
                    }
                case 1:
                    {
                        matrixAdjacency = new int[6, 6]
                        {
                            { 0, 1, 0, 0, 0, 1},
                            { 1, 0, 1, 0, 0, 0},
                            { 0, 1, 0, 1, 0, 1},
                            { 0, 0, 1, 0, 0, 0},
                            { 0, 0, 0, 0, 0, 1},
                            { 1, 0, 1, 0, 1, 0}
                        }; // матрица смежности для 6 вершин
                        break;
                    }
                case 2:
                    {
                        matrixAdjacency = new int[7, 7]
                        {
                            { 0, 0, 0, 1, 0, 1, 0},
                            { 0, 0, 0, 0, 1, 0, 1},
                            { 0, 1, 0, 0, 0, 1, 0},
                            { 1, 0, 1, 0, 1, 0, 0},
                            { 0, 0, 0, 0, 0, 0, 1},
                            { 0, 0, 1, 0, 0, 1, 1},
                            { 0, 0, 0, 0, 1, 0, 0}
                        }; // матрица смежности для 7 вершин
                        break;
                    }
                case 3:
                    {
                        matrixAdjacency = new int[8, 8]
                        {
                            { 0, 1, 0, 1, 0, 0, 0, 1},
                            { 0, 0, 0, 1, 0, 0, 0, 0},
                            { 0, 0, 0, 0, 0, 0, 0, 0},
                            { 0, 0, 1, 0, 1, 0, 0, 0},
                            { 0, 0, 0, 0, 0, 0, 0, 0},
                            { 0, 1, 0, 1, 0, 0, 0, 0},
                            { 0, 0, 1, 1, 1, 0, 0, 0},
                            { 0, 0, 1, 0, 0, 0, 0, 0}
                        }; // матрица смежности для 8 вершин
                        break;
                    }
            }
        }
        public int[,] SortedEdges()
        {
            int[,] edges = Edges;
            for (int i = 0; i < NumEdges - 1; i++)
            {
                for (int j = i + 1; j < NumEdges; j++)
                {
                    if (edges[i, 2] > edges[j, 2])
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            int temp = edges[i, k];
                            edges[i, k] = edges[j, k];
                            edges[j, k] = temp;
                        }
                    }

                }
            }
            return edges;
        }
        public Graph(int[,] matrixAdjacency)
        {
            this.matrixAdjacency = matrixAdjacency;
        }
        public void PrintMatrixAdjacency()
        {
            Console.Write("  ");
            for (int i = 0; i < NumCol; i++)
            {
                Console.Write($"{GetLetterFromIndex(i),3} ");
            }
            Console.WriteLine();
            for (int i = 0; i < NumRow; i++)
            {
                Console.Write($"{GetLetterFromIndex(i)} ");
                for (int j = 0; j < NumCol; j++)
                {

                    Console.Write($"{matrixAdjacency[i, j],3} ");
                }
                Console.WriteLine();
            }
        }
        public void PrintVertex()
        {
            for (int i = 0; i < NumVertex; i++)
            {
                Console.Write(GetLetterFromIndex(i));
                if (i != NumVertex - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine();
        }
        public void PrintEdges()
        {
            int[,] edges = Edges;
            for (int i = 0; i < NumEdges; i++)
            {
                Console.Write($"({GetLetterFromIndex(edges[i,0])}, {GetLetterFromIndex(edges[i, 1])})");
                if (i != NumEdges - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine();
        }
        public void Print()
        {
            Console.WriteLine("Узлы графа:");
            PrintVertex();

            Console.WriteLine("Ребра графа:");
            PrintEdges();
        }
        public void PrintListOfLists(ref List<List<int>> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    Console.Write(GetLetterFromIndex(list[i][j]) + " ");
                }
                Console.WriteLine();
            }
        }
        public void PrintList(ref List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(GetLetterFromIndex(list[i]) + " ");
            }
        }
        static public char GetLetterFromIndex(int index)
        {
            if (index < 0 || index > 25)
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс должен быть от 0 до 25.");

            return (char)('a' + index);
        }
        // ярусно-параллельная форма
        public void TieredParallel()
        {
            if (NumVertex == 0)
                throw new ArgumentOutOfRangeException("Количество вершин = 0");
            int[,] matrix = new int[NumRow, NumCol];
            Array.Copy(matrixAdjacency, matrix, matrixAdjacency.Length);

            List<List<int>> tieredParallel = new List<List<int>>();
            // пройденные столбцы (изначально false)
            bool[] markedСol = new bool[NumCol];

            // все ли столбцы пройдены
            bool isAllEmptyColumns = false;

            while (!isAllEmptyColumns)
            {
                List<int> emptyColumns = FindEmptyColumns(ref matrix, ref markedСol);
                tieredParallel.Add(new List<int>(emptyColumns));

                СlearRows(ref matrix, emptyColumns);

                if (AllMarked(markedСol))
                {
                    isAllEmptyColumns = true;
                }

            }

            Console.WriteLine("Уровни узлов:");
            PrintListOfLists(ref tieredParallel);
        }
        // нахождение пустых столбцов + отметка о прохождении (ЯПФ)
        List<int> FindEmptyColumns(ref int[,] matrix, ref bool[] markedСol)
        {
            List<int> emptyColumns = new List<int>();
            for (int i = 0; i < NumCol; i++)
            {
                if (!markedСol[i])
                {
                    bool isNull = true;
                    for (int j = 0; j < NumRow; j++)
                    {
                        if (matrix[j, i] == 1)
                        {
                            isNull = false;
                            break;
                        }
                    }
                    if (isNull)
                    {
                        emptyColumns.Add(i);
                        markedСol[i] = true;
                    }
                }
            }
            return emptyColumns;
        }
        // очищение строк (ЯПФ)
        void СlearRows(ref int[,] matrix, List<int> emptyColumns)
        {

            while (emptyColumns.Count != 0)
            {
                for (int i = 0; i < NumCol; i++)
                {
                    matrix[emptyColumns[0], i] = 0;
                }
                emptyColumns.RemoveAt(0);
            }
        }
        // проверка, все ли столбцы были задействованы
        protected bool AllMarked(bool[] markedСol)
        {
            bool isMarked = true;
            for (int i = 0; i < NumCol; i++)
            {
                if (!markedСol[i])
                {
                    isMarked = false;
                    break;
                }
            }
            return isMarked;
        }
    }
    public class OrientedGraph : Graph
    {
        public OrientedGraph()
        {
            switch (random.Next(3))
            {
                case 0:
                    {
                        matrixAdjacency = new int[6, 6]
                        {
                            { 0, 11, 0, 14, 15, 0},
                            { 0, 0, 13, 0, 0, 0},
                            { 0, 0, 0, 0, 0, 13},
                            { 0, 7, 11, 0, 9, 0},
                            { 0, 11, 10, 0, 0, 14},
                            { 0, 0, 0, 0, 0, 0}
                        };
                        break;
                    }
                case 1:
                    {
                        matrixAdjacency = new int[6, 6]
                        {
                            { 0, 5, 8, 7, 18, 0},
                            { 0, 0, 11, 0, 0, 0},
                            { 0, 0, 0, 0, 0, 17},
                            { 0, 10, 12, 0, 6, 0},
                            { 0, 7, 8, 0, 0, 11},
                            { 0, 0, 0, 0, 0, 0}
                        };
                        break;
                    }
                case 2:
                    {
                        matrixAdjacency = new int[6, 6]
                        {
                            { 0, 5, 10, 13, 0, 0},
                            { 0, 0, 8, 9, 13, 0},
                            { 0, 0, 0, 5, 3, 6},
                            { 0, 0, 0, 0, 8, 10},
                            { 0, 0, 0, 0, 0, 9},
                            { 0, 0, 0, 0, 0, 0}
                        };
                        break;
                    }
            }
        }
        public OrientedGraph(int[,] matrixAdjacency) : base(matrixAdjacency) { }

        public void Deystras()
        {
            if (NumVertex == 0)
                throw new ArgumentOutOfRangeException("Количество вершин = 0");
            // массив пройденных вершин (*)
            bool[] peaksPassed = new bool[NumVertex];
            // массив минимальных длин до каждой из вершин
            int[] minLengths = new int[NumVertex];
            for (int i = 0; i < minLengths.Length; i++)
            {
                minLengths[i] = int.MaxValue;
            }

            // все ли столбцы пройдены
            bool isAllEmptyColumns = false;

            int vertex = 0;
            minLengths[vertex] = 0;
            // часть 1 - получение минимальных путей до каждого пункта
            while (!isAllEmptyColumns)
            {
                peaksPassed[vertex] = true;
                for (int i = 0; i < NumCol; i++)
                {
                    if (matrixAdjacency[vertex, i] != 0)
                    {
                        if(minLengths[i] > matrixAdjacency[vertex, i] + minLengths[vertex] && peaksPassed[i] != true)
                        {
                            minLengths[i] = matrixAdjacency[vertex, i] + minLengths[vertex];
                        }
                    }
                }
                int min = int.MaxValue;
                int minIndex = int.MaxValue;
                for (int i = 1; i < minLengths.Length; i++)
                {
                    if(min > minLengths[i] && peaksPassed[i] != true)
                    {
                        min = minLengths[i];
                        minIndex = i;
                    }
                }
                vertex = minIndex;
                if (AllMarked(peaksPassed))
                {
                    isAllEmptyColumns = true;
                }
            }
            // часть 2 - получение минимального пути из пункта А в конечный
            Stack<int> stackVertex = new Stack<int>();

            vertex = NumVertex - 1;
            stackVertex.Push(vertex);

            while (vertex != 0)
            {
                for (int i = 0; i < NumRow; i++)
                {
                    if (matrixAdjacency[i, vertex] != 0)
                    {
                        if (minLengths[i] + matrixAdjacency[i, vertex] == minLengths[vertex])
                        {
                            vertex = i;
                            stackVertex.Push(vertex);
                            break;
                        }
                    }
                }

            }
            List<int> listVertex = new List<int> ();
            while (stackVertex.Count > 0)
            {
                listVertex.Add(stackVertex.Pop());
            }
            Console.WriteLine("Путь:");
            PrintList(ref listVertex);
        }
    }
    public class WeightedGraph : Graph
    {
        public WeightedGraph()
        {
            switch (random.Next(2))
            {
                case 0:
                    {
                        matrixAdjacency = new int[7, 7]
                        {
                            { 0, 10, 0, 5, 0, 0, 14},
                            { 10, 0, 6, 2, 4, 8, 0},
                            { 0, 6, 0, 3, 1, 1, 0},
                            { 5, 2, 3, 0, 0, 0, 3},
                            { 0, 4, 1, 0, 0, 5, 0},
                            { 0, 8, 1, 0, 5, 0, 2},
                            { 14, 0, 0, 3, 0, 2, 0}
                        };
                        break;
                    }
                case 1:
                    {
                        matrixAdjacency = new int[7, 7]
                        {
                            { 0, 7, 0, 5, 0, 0, 0},
                            { 7, 0, 8, 9, 7, 0, 0},
                            { 0, 8, 0, 0, 5, 0, 0},
                            { 5, 9, 0, 0, 0, 6, 0},
                            { 0, 7, 5, 0, 0, 8, 9},
                            { 0, 0, 0, 6, 8, 0, 11},
                            { 0, 0, 0, 0, 9, 11, 0}
                        };
                        break;
                    }
            }
        }
        public WeightedGraph(int[,] matrixAdjacency) : base(matrixAdjacency) { }
        public void Kruskal()
        {
            if (NumVertex == 0)
                throw new ArgumentOutOfRangeException("Количество вершин = 0");
            // cортировка ребер
            int[,] sortedEdges = SortedEdges();

            int[,] way = new int[NumVertex, NumVertex];

            int numEdgesNew = 0;

            // если нет цикла и есть доступ из каждой вершины до каждой
            for (int i = 0; i < NumEdges; i++)
            {
                int vertex1New = sortedEdges[i, 0];
                int vertex2New = sortedEdges[i, 1];
                int weightNew = sortedEdges[i, 2];

                if (numEdgesNew/2 == NumVertex - 1)
                    break;

                if (!CycleExists(way, vertex1New, vertex2New))
                {
                    way[vertex1New, vertex2New] = weightNew;
                    numEdgesNew ++;
                }
            }

            int sum = 0;
            // считаем сумму
            for (int i = 0; i < way.GetLength(0); i++)
            {
                for (int j = i + 1; j < way.GetLength(1); j++)
                {
                    sum += way[i,j];
                }
            }
            Console.WriteLine($"\nДлина пути минимального остова = {sum}");
        }
        private bool DFS(int[,] matrixAdjacency, int vertex, bool[] visited, int parent)
        {
            visited[vertex] = true;

            for (int j = 0; j < matrixAdjacency.GetLength(0); j++)
            {
                if (matrixAdjacency[vertex, j] != 0)
                {
                    if (!visited[j])
                    {
                        if (DFS(matrixAdjacency, j, visited, vertex))
                        {
                            return true;
                        }
                    }
                    else if (j != parent)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CycleExists(int[,] way, int vertex1, int vertex2)
        {
            int[,] wayNew = new int[way.GetLength(0), way.GetLength(1)];
            for (int i = 0; i < way.GetLength(0); i++)
            {
                for (int j = 0; j < way.GetLength(1); j++)
                {
                    wayNew[i, j] = way[i, j];
                }
            }
            wayNew[vertex1, vertex2] = 1; 

            bool[] visited = new bool[wayNew.GetLength(0)];

            return DFS(wayNew, vertex1, visited, -1);
        }
    }
}
