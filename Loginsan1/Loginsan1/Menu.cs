using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loginsan1
{
    internal class Menu
    {
        private int SelectedIndex;
        private string Prompts;
        private string Accoutprompt;
        private string[] Options;

        public Menu(string[] option, string prompt, string accountprompt = " ")
        {
            Options = option;
            Prompts = prompt;
            Accoutprompt = accountprompt;
            SelectedIndex = 0;
        }

        private void Display()
        {
            Printtitle();

            for (int i = 0; i < Options.Length; i++)
            {
                string currentoption = Options[i];
                string prefix;

                int screenWidth = Console.WindowWidth;
                int screenHeight = Console.WindowHeight;
                int leftMargin = (screenWidth - Prompts.Split('\n')[0].Length - 20) / 2;
                int topMargin = Prompts.Split('\n').Length;
            if (leftMargin > 0 && leftMargin < screenWidth && topMargin > 0 && topMargin < screenHeight)
                {
                    Console.SetCursorPosition(leftMargin, topMargin + i);

                    if (i == SelectedIndex)
                    {
                        prefix = ">";
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        prefix = " ";
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine($"{prefix} --{currentoption}--");
                }
            }
            Console.ResetColor();
        }

        public int Run()
        {
            ConsoleKey keyPress;
            do
            {
                Console.Clear();
                Display();
                var key = Console.ReadKey(intercept: true);
                keyPress = key.Key;

                if (keyPress == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if(SelectedIndex < 0)
                    {
                        SelectedIndex = Options.Length - 1;
                    }    
                }
                else if (keyPress == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if(SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }    
                }
            } while (keyPress != ConsoleKey.Enter);

            return SelectedIndex;
        }

        private void Printtitle()
        {
            int screenWidth = Console.WindowWidth;

            // Tính toán vị trí in để đưa văn bản vào giữa màn hình
            int leftMargin = (screenWidth - Prompts.Split('\n')[1].Length - 10) / 2;

            // Tách các dòng văn bản và in chúng
            string[] lines = Prompts.Split('\n');
            if (leftMargin > 0 && leftMargin < screenWidth)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    Console.SetCursorPosition(leftMargin, 0 + i);
                    Console.WriteLine(lines[i]);
                }
            }
            Console.SetCursorPosition(leftMargin, Prompts.Split('\n').Length);
            Console.WriteLine(Accoutprompt);
        }
    }
}
