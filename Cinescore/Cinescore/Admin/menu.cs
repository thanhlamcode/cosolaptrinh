﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    class Menu
    {
        private string Prompts;
        private string[] Options;
        private int SelectedIndex;
        private bool Flag;

        public Menu(string[] options, string prompts, bool flag = true)
        {
            Prompts = prompts;
            Flag = flag;
            Options = options;
            SelectedIndex = 0;
        }

        private void Display()
        {
            int num_title = 2;
            if (!Flag)
            {
                Print_title();
                num_title = 0;
            }
            else Print_Prompts();

            string text = null;
            int t1 = (Console.WindowWidth - 60) / 2;
            int t2 = Prompts.Split('\n').Length + num_title;
            Console.SetCursorPosition(t1, t2);
            Console.WriteLine('+' + new string('-', 58) + '+');

            for (int i = 0; i < Options.Length; i++)
            {
                text = Options[i];
                char prefix;
                int mid_2 = (50 - (Options[i].Length + 8)) / 2;
                Console.SetCursorPosition(t1, t2 + 1 + i);
                Console.Write("|");
                Console.SetCursorPosition(t1 + 6 + mid_2, t2 + 1 + i);
                if (i == SelectedIndex)
                {
                    prefix = '>';
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = ' ';
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.Write($"{new string(prefix, 2)}[{text}]");
                Console.SetCursorPosition(t1 + 59, t2 + 1 + i);
                Console.ResetColor();
                Console.WriteLine("|");
            }

            Console.SetCursorPosition(t1, t2 + Options.Length + 1);
            Console.WriteLine('+' + new string('-', 58) + '+');
        }

        void Print_Prompts()
        {
            int t1 = (Console.WindowWidth - 60) / 2;

            Console.SetCursorPosition(t1, 0);
            Console.WriteLine('+' + new string('-', 58) + '+');

            for (int i = 0; i < Prompts.Split('\n').Length; i++)
            {
                int mid_2 = (50 - Prompts.Split('\n')[i].Length) / 2;
                Console.SetCursorPosition(t1, 1 + i);
                Console.Write("|");
                Console.SetCursorPosition(t1 + 6 + mid_2, 1 + i);
                Console.WriteLine($"{Prompts.Split('\n')[i]}");
                Console.SetCursorPosition(t1 + 59, 1 + i);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(t1, Prompts.Split('\n').Length + 1);
            Console.WriteLine('+' + new string('-', 58) + '+');
        }

        public int Run()
        {
            ConsoleKey Keypress;
            do
            {
                Console.Clear();
                Display();
                var key = Console.ReadKey(intercept: true).Key;
                Keypress = key;

                if (Keypress == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex < 0)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (Keypress == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
            } while (Keypress != ConsoleKey.Enter);

            return SelectedIndex;
        }

        private void Print_title()
        {
            int t1 = (Console.WindowWidth - 87) / 2;
            for (int i = 0; i < Prompts.Split('\n').Length; i++)
            {
                Console.SetCursorPosition(t1, i);
                Console.WriteLine($"{Prompts.Split('\n')[i]}");
            }
        }
    }
}