using System;
using System.Linq;

namespace Machine
{
    internal class Program
    {
        static string InputAllowdStr(char[] allowedChars)
        {
            string strInput;
            do
            {
                Console.WriteLine($"Введите строку (разрешенные символы: {string.Join(", ", allowedChars)}): ");
                strInput = Console.ReadLine();

            }
            while (!strInput.All(c => allowedChars.Contains(c)));
            return strInput;
        }

        static void Main(string[] args)
        {
            char[] x = { 'a', 'b', 'c', 'd'};
            string str = InputAllowdStr(x);

            int[,] transitionTable = new int[4, 4]
                { 
                    { 1, 3, 1, 3 }, 
                    { 2, 2, 3, 3 },
                    { 2, 2, 3, 3 },
                    { 2, 2, 3, 3 }
                };// столбцы - состояния, строки - входные данные
            int state = 0;// состояния: 0 - начальное положение, 1 - гласная, 2 - согласная, 3 - ошибка (не перемежаются)
            for (int i = 0; i < str.Length; i++)
            {
                int indexX = -1;
                for (int j = 0; j < x.Length; j++)
                {
                    if (str[i] == ' ')
                    {
                        indexX = 4;
                        break;
                    }

                    else if (str[i] == x[j])
                    {
                        indexX = j;
                        break;
                    }
                }
                state = transitionTable[indexX, state];
            }
            Console.WriteLine(state == 3 ? "Не перемежаются" : "Перемежаются");
        }
    }
}