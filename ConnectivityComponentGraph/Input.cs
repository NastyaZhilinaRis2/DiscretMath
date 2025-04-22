using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectivityComponentGraph
{
    class Input
    {
        public static int InputInt()
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
                    Console.WriteLine("Некорректный ввод. Введите число: ");
            }
            while (!isCorrect);
            return number;
        }
        public static int InputInt(int left, int right)
        {
            bool isCorrect = false;
            int number;
            do
            {
                number = InputInt();
                if (number >= left && number <= right)
                    isCorrect = true;

                else
                    Console.WriteLine($"Введите число из интервала от {left} до {right}");
            }
            while (!isCorrect);
            return number;
        }
    }
}
