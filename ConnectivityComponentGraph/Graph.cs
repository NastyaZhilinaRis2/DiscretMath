using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectivityComponentGraph
{
    public class Graph
    {
        private Random random = new Random();
        private int[,] matrixAdjacency; // матрица смежности
        private int numVertex;
        public Graph()
        {
            switch (random.Next(3))
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
                        matrixAdjacency = new int[6,6]
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
                        matrixAdjacency = new int[7,7]
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
            }
            numVertex = matrixAdjacency.GetLength(0);
        }
        public Graph(int[,] matrixAdjacency)
        {
            this.matrixAdjacency = matrixAdjacency;
            numVertex = matrixAdjacency.GetLength(0);
        }
        public void PrintMatrixAdjacency()
        {
            int numRow = matrixAdjacency.GetLength(0);
            int numCol = matrixAdjacency.GetLength(1);
            Console.Write("  ");
            for (int i = 0; i < numCol; i++)
            {
                Console.Write(GetLetterFromIndex(i) + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < numRow; i++)
            {
                Console.Write(GetLetterFromIndex(i) + " ");
                for (int j = 0; j < numCol; j++)
                {
                    Console.Write(matrixAdjacency[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        // Преобразуем индекс в соответствующий символ
        static public char GetLetterFromIndex(int index)
        {
            if (index < 0 || index > 25) // Проверка, чтобы индекс был в пределах 0-25
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс должен быть от 0 до 25.");

            return (char)('a' + index); // Преобразуем индекс в соответствующий символ
        }
        // выделение компонент сильной связности
        public List<List<char>> StrongConnectivityComponents()
        {
            // матрица достижимости
            int[,] matrixReachability = ReachabilityMatrix();
            // матрица сильной связности
            int[,] matrixStrongConnectivity = StrongConnectivityMatrix(matrixReachability);
            // компоненты связности
            List<List<char>> componentsStrongConnectivity = new List<List<char>>();
            int numComponents = 0;
            for (int i = 0; i < numVertex; i++)
            {
                bool flag = false;
                for (int j = 0; j < numVertex; j++)
                {
                    if(matrixStrongConnectivity[i,j] == 1)
                    {
                        if (!flag) // если вершина является первой для компоненты
                        {
                            componentsStrongConnectivity.Add(new List<char>());
                            flag = true;
                        }
                        componentsStrongConnectivity[numComponents].Add(GetLetterFromIndex(j));
                        // "вычеркнем" столбик
                        for (int k = 0; k < numVertex; k++)
                        {
                            matrixStrongConnectivity[k, j] = 0;
                        }
                    }
                }
                if(flag) // если компонента была создана
                    numComponents++;
            }
            return componentsStrongConnectivity;
        }

        // копирование матриц
        static public int[,] CopyMatrix(int[,] original)
        {
            int numRow = original.GetLength(0);
            int numCol = original.GetLength(1);
            int[,] copy = new int[numRow, numCol];

            for (int i = 0; i < numRow; i++)
            {
                for (int j = 0; j < numCol; j++)
                {
                    copy[i, j] = original[i, j];
                }
            }

            return copy;
        }
        // бинарное умножение матриц
        static public int[,] MultiplicationMatrix (int[,] matrix1, int[,] matrix2)
        {
            int numRow = matrix1.GetLength(0);
            int numCol = matrix2.GetLength(1);
            int numIter = matrix1.GetLength(1);

            if (numIter != numCol)
                throw new ArgumentException("Такую матрицу нельзя возвести в степень, так как число столбцов первой матрицы не равно числу строк второй.");

            int[,] result = new int[numRow, numCol];

            for (int i = 0; i < numRow; i++)
            {
                for (int j = 0; j < numCol; j++)
                {
                    int sum = 0;
                    for (int iter = 0; iter < numIter; iter++)
                    {
                        sum += matrix1[i, iter] * matrix2[iter, j];
                    }
                    if (sum > 0)
                        result[i, j] = 1;
                }

            }
            return result;
        }
        // бинарное возведение матриц в степень
        static public int[,] ExponentiationMatrix(int[,] matrix, int degree)
        {
            if (degree < 0) 
                throw new ArgumentException("Степень не может быть отрицательной.");

            int numRow = matrix.GetLength(0);
            int numCol = matrix.GetLength(1);

            int[,] result = new int[numRow, numCol];

            if (degree == 0)
            {
                if(numRow == numCol)
                    for (int i = 0; i < numRow; i++)
                    {
                        result[i, i] = 1;
                    }
                else
                    throw new ArgumentException("Такую матрицу нельзя возвести в степень, так как количество столбцов и количество строк матрицы не равны.");
            }
            else
            {
                result = CopyMatrix(matrix);
                for (int d = 2; d <= degree; d++)
                {
                    result = MultiplicationMatrix(result, matrix);
                }
            }

            return result;
        }
        // бинарное сложение матриц
        static public int[,] AdditionMatrix(int[,] matrix1, int[,] matrix2)
        {
            int numRow1 = matrix1.GetLength(0);
            int numCol1 = matrix1.GetLength(1);
            int numRow2 = matrix2.GetLength(0);
            int numCol2 = matrix2.GetLength(1);

            if (numRow1 != numRow2 || numCol1 != numCol2)
                throw new ArgumentException("Такие матрицы невозможно сложить, так как размеры матриц отличаются.");

            int[,] result = new int[numRow1, numCol1];
            for (int i = 0; i < numRow1; i++)
            {
                for (int j = 0; j < numCol1; j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                    if (result[i, j] > 0)
                        result[i, j] = 1;
                }
            }

            return result;
        }
        // вычисление матрицы достижимости
        int[,] ReachabilityMatrix()
        {
            int[,] matrixReachability = new int[numVertex, numVertex];
            // единичная матрица Е0
            for (int i = 0; i < numVertex; i++)
            {
                matrixReachability[i, i] = 1;
            }
            // нахождение R(G) = E0 + E1 + E2 + ... + En
            for (int degree = 1; degree <= numVertex; degree++)
            {
                matrixReachability = AdditionMatrix(matrixReachability, ExponentiationMatrix(matrixAdjacency, degree));
            }

            return matrixReachability;
        }
        // транспонирование матрицы
        static int[,] TranspositionMatrix (int[,] matrix)
        {
            int numRows = matrix.GetLength(0);
            int numCols = matrix.GetLength(1);
            int[,] trans = new int[numCols, numRows];

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    trans[j, i] = matrix[i, j];
                }
            }

            return trans;
        }
        // конъюнкция матриц
        static public int[,] ConjunctionMatrix(int[,] matrix1, int[,] matrix2)
        {
            int numRow1 = matrix1.GetLength(0);
            int numCol1 = matrix1.GetLength(1);
            int numRow2 = matrix2.GetLength(0);
            int numCol2 = matrix2.GetLength(1);

            if (numRow1 != numRow2 || numCol1 != numCol2)
                throw new ArgumentException("Такие матрицы невозможно логически перемножить, так как размеры матриц отличаются.");

            int[,] result = new int[numRow1, numCol1];
            for (int i = 0; i < numRow1; i++)
            {
                for (int j = 0; j < numCol1; j++)
                {
                    result[i, j] = matrix1[i, j] * matrix2[i, j];
                }
            }

            return result;
        }
        // вычисление матрицы сильной связности
        int[,] StrongConnectivityMatrix(int[,] R)
        {
            int[,] matrixStrongConnectivity = new int[numVertex, numVertex];
            int[,] RTrans = TranspositionMatrix(R);

            matrixStrongConnectivity = ConjunctionMatrix(R, RTrans);

            return matrixStrongConnectivity;
        }
    }
}
