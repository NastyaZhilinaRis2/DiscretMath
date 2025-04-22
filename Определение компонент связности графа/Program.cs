using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace мат.логика2
{
    class Program
    {
        static int n, numberLines, trueF = 0;
        static int[,] truthTable;
        static List<List<string>> cdnf;
        static List<List<string>> cknf;
        static List<List<List<string>>> mdnf;

        static void Main(string[] args)
        {
            TruthTable();
            FirstPart();
            SecondPart();
            Console.ReadLine();
        }
        static void FirstPart()
        {

            SDNF();
            SKNF();

        }
        static void SecondPart()
        {
            MDNF();
        }
        static void TruthTable()
        {
            Random random = new Random();
            int f = 0;

            Console.Write("n = ");
            n = InputInt(1, 5);

            numberLines = (int)Math.Pow(2, n);
            truthTable = new int[numberLines, n + 1];
            int numberZero = 1;
            //столбцы начиная с последнего
            for (int j = n - 1; j >= 0; j--)
            {
                //строки матрицы
                for (int i = numberZero; i < numberLines; i += numberZero * 2)
                {
                    for (int k = 0; k < numberZero; k++)
                    {
                        truthTable[i + k, j] = 1;
                    }
                }
                numberZero *= 2;
            }
            Console.WriteLine("Ввод с клавиатуры - 0, ДСЧ - 1");
            int input = InputInt(0, 1);

            for (int i = 0; i < n; i++)
            {
                Console.Write((char)('a' + i) + " ");
            }
            Console.WriteLine("F");

            for (int i = 0; i < numberLines; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(truthTable[i, j] + " ");
                }
                switch (input)
                {
                    case 0:
                        {
                            f = InputInt(0, 1);
                            truthTable[i, n] = f;
                            break;
                        }
                    case 1:
                        {
                            f = random.Next(0, 2);
                            truthTable[i, n] = f;
                            Console.WriteLine(f + " ");
                            break;
                        }
                }
                if (f == 1) trueF++;
            }
        }
        static void SDNF()
        {
            cdnf = new List<List<string>>();
            int row = 0;

            for (int i = 0; i < numberLines; i++)
            {
                if (truthTable[i, n] == 1)
                {
                    cdnf.Add(new List<string>());
                    for (int j = 0; j < n; j++)
                    {
                        if (truthTable[i, j] == 0)
                            cdnf[row].Add("!" + (char)('a' + j));
                        else
                            cdnf[row].Add("" + (char)('a' + j));
                    }
                    row++;
                }
            }


            Console.Write("СДНФ: ");
            if(cdnf.Count == 0)
                Console.WriteLine("нет");
            else
            {
                for (int i = 0; i < trueF; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(cdnf[i][j]);
                    }
                    if (i != trueF - 1)
                        Console.Write(" v ");
                    else
                        Console.WriteLine();
                }
            }
        }
        static void SKNF()
        {
            cknf = new List<List<string>>();
            int row = 0;

            for (int i = 0; i < numberLines; i++)
            {
                if (truthTable[i, n] == 0)
                {
                    cknf.Add(new List<string>());
                    for (int j = 0; j < n; j++)
                    {
                        if (truthTable[i, j] == 0)
                            cknf[row].Add("!" + (char)('a' + j));
                        else
                            cknf[row].Add("" + (char)('a' + j));
                    }
                    row++;
                }
            }

            Console.Write("СКНФ: ");
            if (cknf.Count == 0)
                Console.WriteLine("нет");
            else
            {

                for (int i = 0; i < numberLines - trueF; i++)
                {
                    Console.Write("(");
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(cknf[i][j]);
                        if (j != n - 1)
                            Console.Write("v");
                    }
                    Console.Write(")");
                }
                Console.WriteLine();
            }
            
        }
        static void MDNF()
        {
            Console.Write("МДНФ: ");
            FindImplicants();
            if (mdnf.Count == 0)
                Console.WriteLine("нет");
            else
            {
                for (int i = 0; i < mdnf.Count; i++)
                {
                    Console.Write($"{i + 1}) ");
                    for (int j = 0; j < mdnf[i].Count; j++)
                    {
                        for (int k = 0; k < mdnf[i][j].Count; k++)
                        {
                            if (mdnf[i][j][k] != "0")
                            {
                                Console.Write(mdnf[i][j][k]);
                            }
                        }
                        if (j != mdnf[i].Count - 1)
                            Console.Write(" v ");
                    }
                    Console.WriteLine();
                }
            }
        }
        static void Takeover(ref List<List<string>> table)
        {
            int sumNull = 0, indexNotNull = -1;
            for (int i = 0; i < table.Count; i++)
            {
                sumNull = 0;
                for (int j = 0; j < table[i].Count; j++)
                {
                    if (table[i][j] == "0")
                        sumNull++;
                    else indexNotNull = j;
                }
                if (sumNull == n - 1)
                {
                    for (int k = 0; k < table.Count; k++)
                    {
                        for (int l = 0; l < table[k].Count; l++)
                        {
                            if (table[k][l] == table[i][indexNotNull] && k != i)
                            {
                                table.Add(new List<string>(table[i]));
                                if (i < k)
                                {
                                    table.RemoveAt(i);
                                    table.RemoveAt(k - 1);
                                }
                                else
                                {
                                    table.RemoveAt(k);
                                    table.RemoveAt(i - 1);
                                }
                                i = 0;
                                break;
                            }
                        }
                    }
                }

            }
        }
        static void FindImplicants()
        {
            List<List<string>> tableGluing = new List<List<string>>();
            List<List<string>> temp = new List<List<string>>();

            Gluing(ref cdnf, ref tableGluing);

            int maxNumberGlues = n - 2;
            while (maxNumberGlues > 0)
            {
                CopyTableList(ref tableGluing, ref temp);
                Gluing(ref temp, ref tableGluing);
                maxNumberGlues--;
            }
            DeleteDuplicates(ref tableGluing);
            Takeover(ref tableGluing);
            //////////////////////////ИМПЛИКАНТЫ
            /*Console.WriteLine();
            int k = 0;
            foreach (var list in tableGluing)
            {
                foreach (var item in list)
                {
                    Console.Write(item);
                }
                Console.WriteLine("  " + k);
                k++;
            }*/
            //////////////////////////


            QuineMcCluskey(ref tableGluing, ref cdnf);
        }
        static void FindIdenticalStrings(List<List<string>> table, int numberСolumns, ref int row1, ref int row2, ref int col, ref int numberMatches, ref string implicant, bool isCDNF)
        {
            if (col == numberСolumns)
            {
                if (numberMatches > 0)
                    implicant += 0;
                AdvanceToNextRows(table, ref row1, ref row2, ref col, ref numberMatches, ref implicant);
            }

            else if (row2 < table.Count)
            {
                if (numberMatches > 0 && table[row1][col] != table[row2][col])
                    implicant += 0;
                if (table[row1][col] == table[row2][col])
                {
                    if(col == 1 && implicant == "")
                        implicant += 0;
                    implicant += table[row1][col];
                    col++;
                    numberMatches++;
                    if (col == numberСolumns - 1 && numberMatches == n - 1)
                        implicant += 0;
                }

                else if (col + 1 < numberСolumns && numberMatches != 0)
                    col++;
                else if (col + 1 < numberСolumns && isCDNF)
                {
                    implicant = "";
                    col++;
                    numberMatches = 0;
                }
                else if (col + 1 < numberСolumns && !isCDNF)
                {
                    if (table[row1][col] != table[row2][col] && !isCDNF)
                    {
                        implicant += 0;
                    }
                    col++;
                    numberMatches = 0;
                }
                else if (row1 + 1 < table.Count - 1 && row2 + 1 < table.Count)
                    AdvanceToNextRows(table, ref row1, ref row2, ref col, ref numberMatches, ref implicant);
                else if (n == 1)
                    implicant = "0";
                else col = n;
            }
        }
        static void AdvanceToNextRows(List<List<string>> table, ref int row1, ref int row2, ref int col, ref int numberMatches, ref string implicant)
        {
            if (row2 + 1 < table.Count)
            {
                row2++;
                col = 0;
                numberMatches = 0;
                implicant = "";
            }
            else if (row1 + 1 < table.Count - 1)
            {
                row1++;
                row2 = row1 + 1;
                col = 0;
                numberMatches = 0;
                implicant = "";
            }
        }
        static void SeparationImplicants(ref List<List<string>> tableFrom, int rowFrom, int colFrom, ref List<List<string>> tableIn, int rowIn, int colIn)
        {
            string partImplicant = "";
            char[] variable = tableFrom[rowFrom][colFrom].ToCharArray();

            foreach (char v in variable)
            {
                if (v == '!')
                {
                    partImplicant = "!";
                }
                else if (partImplicant == "!")
                {
                    tableIn[rowIn].Add("!" + v.ToString());
                    partImplicant = "";
                }
                else
                    tableIn[rowIn].Add(v.ToString());
            }
        }
        static void DeleteDublList( ref List<List<string>> list)
        {
            HashSet<string> uniqueLists = new HashSet<string>();
            List<List<string>> result = new List<List<string>>();

            foreach (var innerList in list)
            {
                // Преобразуем список в строку для уникальности
                string listString = string.Join(",", innerList);

                if (uniqueLists.Add(listString)) // Если добавление прошло успешно, значит, это уникальный список
                {
                    result.Add(new List<string>(innerList)); // Добавляем оригинальный список
                }
            }

            list = result;
        }
        static void AddImplicant(ref string str, ref List<List<string>> tableIn)
        {
            if (str.Length == 1 || str.Length == 2 && str[0] == '!')
            {
                tableIn.Add(new List<string> { str });
            }
            else
            {
                tableIn.Add(new List<string> { str });
                SeparationImplicants(ref tableIn, tableIn.Count - 1, 0, ref tableIn, tableIn.Count - 1, 0);
                tableIn[tableIn.Count - 1].RemoveAt(tableIn[tableIn.Count - 1].Count - 1 - n);
            }
        }
        static void Gluing (ref List<List<string>> sourceTable, ref List<List<string>> outputTable)
        {
            int row1 = 0, row2 = 1, col = 0, numberMatches = 0;
            string implicant = "";
            bool[] temp = new bool[sourceTable.Count];

            if (sourceTable.Count == 1)
            {
                for (int i = 0; i < n; i++)
                {
                    implicant += sourceTable[0][i];
                }
                AddImplicant(ref implicant, ref outputTable);

            }
            else if (sourceTable.Count != 0)
            {
                do
                {
                    FindIdenticalStrings(sourceTable, n, ref row1, ref row2, ref col, ref numberMatches, ref implicant, true);
                    if (numberMatches == n - 1)
                    {
                        AddImplicant(ref implicant, ref outputTable);
                        temp[row1] = true;
                        temp[row2] = true;
                        AdvanceToNextRows(sourceTable, ref row1, ref row2, ref col, ref numberMatches, ref implicant);
                    }
                }
                while (!(row1 == sourceTable.Count - 2 && row2 == sourceTable.Count - 1 && (col == n - 1 && numberMatches == n - 1 || col == n)));

                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] == false)
                    {
                        implicant = "";
                        for (int j = 0; j < sourceTable[i].Count; j++)
                        {
                            implicant += sourceTable[i][j];
                        }
                        AddImplicant(ref implicant, ref outputTable);
                    }
                }
            }
        }
        static void CopyTableList(ref List<List<string>> sourceTable, ref List<List<string>> outputTable)
        {
            foreach (var row in sourceTable)
            {
                outputTable.Add(new List<string>(row));
            }
            sourceTable.Clear();
        }
        static void DeleteDuplicates(ref List<List<string>> list)
        {
            HashSet<string> uniqueLists = new HashSet<string>();
            List<List<string>> result = new List<List<string>>();

            foreach (var innerList in list)
            {
                // Преобразуем внутренний список в строку
                string listString = string.Join(",", innerList);

                // Проверяем, есть ли уже такое строковое представление
                if (uniqueLists.Add(listString))
                {
                    result.Add(innerList);
                }
            }
            list.Clear();
            list = result;
        }
        static int[,] BruteForceBinaryNumbers(int numberOfBits)
        {

            // Массив для хранения бинарных строк
            string[] binaryNumbers = new string[1 << numberOfBits]; // 2^5 = 32

            for (int i = 0; i < binaryNumbers.Length; i++)
            {
                // Преобразуем число в бинарную строку и дополнительно нулями
                binaryNumbers[i] = Convert.ToString(i, 2).PadLeft(numberOfBits, '0');
            }

            // Создаем двумерный массив для хранения битов
            int[,] bitMatrix = new int[binaryNumbers.Length, numberOfBits];

            // Заполняем двумерный массив
            for (int i = 0; i < binaryNumbers.Length; i++)
            {
                for (int j = 0; j < numberOfBits; j++)
                {
                    // Преобразуем символ в целое число (0 или 1)
                    bitMatrix[i, j] = binaryNumbers[i][j] - '0'; // '0' = 48 в ASCII, вычитая получаем 0 или 1
                }
            }
            return bitMatrix;
        }
        static void QuineMcCluskey(ref List<List<string>> implicants, ref List<List<string>> cdnf)
        {
            List<List<string>> mdnfTemp = new List<List<string>>();
            bool[] coincidences = new bool[cdnf.Count];//СДНФ, которые уже включены
            bool[,] coatingMatrix = new bool[implicants.Count, cdnf.Count];
            List<string> mdnfTempString = new List<string>();

            mdnf = new List<List<List<string>>>();

            if (implicants.Count != 0)
            {
                int [,] maskOffImplicahts = BruteForceBinaryNumbers(implicants.Count);
                int cycleNumber = 0;
                do//одна проходка
                {
                    InitialCoatingMatrix(ref cdnf, ref implicants, ref mdnfTempString, ref coincidences, ref coatingMatrix);

                    for (int indexImplic = 0; indexImplic < implicants.Count; indexImplic++)
                    {
                        //есди индекс проверяемого импликанта не равен "отключенному"
                        if (true)
                        {
                            //совпадения импликанта с элементами в сднф
                            bool isIncluded;
                            for (int indexCdnf = 0; indexCdnf < cdnf.Count; indexCdnf++)
                            {
                                isIncluded = true;
                                for (int j = 0; j < implicants[indexImplic].Count; j++)
                                {
                                    if (implicants[indexImplic][j] != cdnf[indexCdnf][j] && implicants[indexImplic][j] != "0")
                                    {
                                        isIncluded = false;
                                        break;
                                    }
                                }
                                if (isIncluded)
                                    coatingMatrix[indexImplic, indexCdnf] = true;
                            }
                            //были ли совпадения с данным сднф уже или нет
                            bool isNotCoincidences = false;
                            for (int i = 0; i < coincidences.Length; i++)
                            {
                                if (coincidences[i] != coatingMatrix[indexImplic, i] && coatingMatrix[indexImplic, i] == true)
                                {
                                    isNotCoincidences = true;
                                    break;
                                }
                            }
                            //если не было
                            if (isNotCoincidences)
                            {
                                string mdnfTempStr = "";
                                for (int k = 0; k < implicants[0].Count; k++)
                                {
                                    mdnfTempStr += implicants[indexImplic][k];
                                }
                                mdnfTempString.Add(mdnfTempStr);
                                for (int i = 0; i < coatingMatrix.GetLength(1); i++)
                                {
                                    if (coatingMatrix[indexImplic, i] == true)
                                        coincidences[i] = true;
                                }
                            }
                        }
                    }
                    bool allTrue = coincidences.All(element => element);
                    if (allTrue)
                    {
                        mdnfTemp.Add(new List<string>(mdnfTempString));
                    }
                    mdnfTempString.Clear();
                    for (int i = 0; i < coincidences.Length; i++)
                    {
                        coincidences[i] = false;
                    }
                    for (int i = 0; i < coatingMatrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < coatingMatrix.GetLength(1); j++)
                        {
                            coatingMatrix[i, j] = false;
                        }
                    }
                    cycleNumber++;
                } while (cycleNumber < maskOffImplicahts.GetLength(0));



                DeleteDublList(ref mdnfTemp);

                //если мднф получилась не одна, то удаляем самую длинную мднф (она ею не является)
                if (mdnfTemp.Count > 1)
                {
                    int numberOfLinesToRemove = implicants.Count;
                    mdnfTemp.RemoveAll(list => list.Count == numberOfLinesToRemove);
                }

                List<List<string>> temp = new List<List<string>>();
                for (int row = 0; row < mdnfTemp.Count; row++)
                {
                    for (int col = 0; col < mdnfTemp[row].Count; col++)
                    {
                        temp.Add(new List<string>());
                        SeparationImplicants(ref mdnfTemp, row, col, ref temp, col, 0);
                    }
                    mdnf.Add(new List<List<string>>(temp));
                    temp.Clear();
                }
            }
        }
        static void InitialCoatingMatrix(ref List<List<string>> cdnf, ref List<List<string>> implicants, ref List<string> mdnfTempString, ref bool[] coincidences, ref bool[,] coatingMatrix)
        {
            for (int indexImplic = 0; indexImplic < implicants.Count; indexImplic++)
            {
                bool isIncluded;
                //совпадения импликанта с элементами в сднф
                for (int indexCdnf = 0; indexCdnf < cdnf.Count; indexCdnf++)
                {
                    isIncluded = true;
                    for (int j = 0; j < implicants[indexImplic].Count; j++)
                    {
                        if (implicants[indexImplic][j] != cdnf[indexCdnf][j] && implicants[indexImplic][j] != "0")
                        {
                            isIncluded = false;
                            break;
                        }
                    }
                    if (isIncluded)
                        coatingMatrix[indexImplic, indexCdnf] = true;
                }
            }
            int countNull;
            int indexImplicMdnf = -1;
            for (int indexCdnf = 0; indexCdnf < cdnf.Count; indexCdnf++)
            {
                countNull = 0;
                for (int indexImplic = 0; indexImplic < implicants.Count; indexImplic++)
                {
                    if (coatingMatrix[indexImplic, indexCdnf] == true && countNull == 0)
                    {
                        indexImplicMdnf = indexImplic;
                        countNull++;
                    }
                    else if (coatingMatrix[indexImplic, indexCdnf])
                        countNull++;

                    if (countNull > 1)
                        break;
                }
                if (countNull == 1)
                {
                    string mdnfTempStr = "";
                    for (int i = 0; i < implicants[0].Count; i++)
                    {
                        mdnfTempStr += implicants[indexImplicMdnf][i];
                    }
                    mdnfTempString.Add(mdnfTempStr);
                    for (int i = 0; i < cdnf.Count; i++)
                    {
                        if (coatingMatrix[indexImplicMdnf, i] == true)
                            coincidences[i] = true;
                    }
                }
            }
            for (int i = 0; i < mdnfTempString.Count; i++)
            {
                HashSet<string> uniqueItems = new HashSet<string>(mdnfTempString);
                mdnfTempString = new List<string>(uniqueItems);
            }
        }
        static int InputInt()
        {
            int number;
            bool isCorrect = false;
            do
            {
                string input;
                input = Console.ReadLine();
                if (int.TryParse(input, out number))
                    isCorrect = true;
                else
                    Console.Write("Некорректный ввод. Введите число: ");
            }
            while (!isCorrect);
            return number;
        }
        static int InputInt(int left, int right)
        {
            bool isCorrect = false;
            int number;
            do
            {
                number = InputInt();
                if (number >= left && number <= right)
                    isCorrect = true;

                else
                    Console.Write($"Введите число из интервала от {left} до {right}");
            }
            while (!isCorrect);
            return number;
        }
    }
}
