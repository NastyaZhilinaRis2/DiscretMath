using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAlgorithmsOnGraphs
{
    static public class Menu
    {
        static public int NavigatingMenu(string nameMenu, string[] menuItems)
        {
            int currentIndex = 0; // Индекс текущего выделенного элемента
            ConsoleKeyInfo key;
            bool isOut = false;
            do
            {
                Console.Clear(); // Очищаем консоль перед перерисовкой меню
                Console.WriteLine(nameMenu);
                // Выводим элементы меню
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i == currentIndex) // Если это текущий элемент
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Устанавливаем цвет выделения
                        Console.WriteLine(menuItems[i]);
                        Console.ResetColor(); // Сбрасываем цвет
                    }
                    else
                    {
                        Console.WriteLine(menuItems[i]);
                    }
                }

                key = Console.ReadKey(true); // Читаем нажатую клавишу

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentIndex > 0) // Если не на первом элементе
                        {
                            currentIndex--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentIndex < menuItems.Length - 1) // Если не на последнем элементе
                        {
                            currentIndex++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        {
                            isOut = true;
                        }
                        break;
                    case ConsoleKey.Escape:
                        {
                            isOut = true;
                            currentIndex = -1;
                        }
                        break;
                }
            }
            while (!isOut); // Завершаем цикл по нажатию Enter
            return currentIndex;
        }

        static public void MainMenu()
        {
            string[] menuItems = {
            "Алгоритм 1: Ярусно-параллельная форма графа (топологическая сортировка)",
            "Алгоритм 2: Алгоритм Дейкстры",
            "Алгоритм 3: Поиск минимального остова методом Краскала"
            };
            int currentIndex = NavigatingMenu("\nГЛАВНОЕ МЕНЮ\n", menuItems);
            switch (currentIndex)
            {
                case 0:
                    MenuMethod1();
                    break;
                case 1:
                    MenuMethod2();
                    break;
                case 2:
                    MenuMethod3();
                    break;
                case -1:
                    Environment.Exit(0);
                    break;
            }
        }
        static void ExitInMainMenu()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true); // Читаем нажатую клавишу
            } while (key.Key != ConsoleKey.Escape);
            MainMenu();
        }
        static public void MenuMethod1()
        {
            Console.Clear();
            Methods.TieredParallel();
            Console.WriteLine("\nДля выхода нажмите esc...");
            ExitInMainMenu();

        }
        static public void MenuMethod2()
        {
            Console.Clear();
            Methods.Deystras();
            Console.WriteLine("\nДля выхода нажмите esc...");
            ExitInMainMenu();
        }
        static public void MenuMethod3()
        {
            Console.Clear();
            Methods.Kruskal();
            Console.WriteLine("\nДля выхода нажмите esc...");
            ExitInMainMenu();
        }
    }
}
