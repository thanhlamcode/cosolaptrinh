using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outside_Interface
{
    internal class Menu
    {
        private string Prompts;
        private string[] Options;
        private int SelectedIndex;
        private bool Flag;

        // Khởi tạo một constructor -> Thuận tiện khi tạo một object
        public Menu(string[] options, string prompts, bool flag = true)
        {
            Prompts = prompts;
            Flag = flag;
            Options = options;
            SelectedIndex = 0;
        }

        // Hàm chạy các lựa chọn
        private void Display()
        {
            int num_title = 2;
            int max = 0;
            if (!Flag)
            {
                Print_title();
                num_title = 0;
            }
            else Print_Prompts();

            // In ra dòng có độ dài lớn nhất, làm chuẩn cho việc in ra khung
            for (int i = 0; i < Options.Length; i++)
            {
                if (Options[i].Length > max) max = Options[i].Length;
                else continue;
            }

            string text = null;
            int t2 = Prompts.Split('\n').Length + num_title;
            Console.SetCursorPosition(0, t2);
            Console.WriteLine('╔' + new string('═', 16 + max) + '╗');

            for (int i = 0; i < Options.Length; i++)
            {
                text = Options[i];
                char prefix;
                Console.SetCursorPosition(0, t2 + 1 + i);
                Console.Write("║");
                Console.SetCursorPosition(6, t2 + 1 + i);
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
                Console.Write($"{new string(prefix, 2)}--{text}--");
                Console.SetCursorPosition(17 + max, t2 + 1 + i);
                Console.ResetColor();
                Console.WriteLine("║");
            }

            Console.SetCursorPosition(0, t2 + Options.Length + 1);
            Console.WriteLine('╚' + new string('═', 16 + max) + '╝');
        }

        // In ra một tiêu đề bình thường
        private void Print_Prompts()
        {
            int max = 0;
            for (int i = 0; i < Prompts.Split('\n').Length; i++)
            {
                if (Prompts.Split('\n')[i].Length > max) max = Prompts.Split('\n')[i].Length;
                else continue;
            }

            Console.SetCursorPosition(0, 0);
            Console.WriteLine('╔' + new string('═', 10 + max) + '╗');

            for (int i = 0; i < Prompts.Split('\n').Length; i++)
            {
                Console.SetCursorPosition(0, 1 + i);
                Console.Write("║");
                Console.SetCursorPosition(6, 1 + i);
                Console.WriteLine($"{Prompts.Split('\n')[i]}");
                Console.SetCursorPosition(11 + max, 1 + i);
                Console.WriteLine("║");
            }
            Console.SetCursorPosition(0, Prompts.Split('\n').Length + 1);
            Console.WriteLine('╚' + new string('═', 10 + max) + '╝');
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

        // In ra các tiêu đề như: Logo UEH, Logo App, những thứ nhiều ký tự '\n'
        private void Print_title()
        {
            for (int i = 0; i < Prompts.Split('\n').Length; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine($"{Prompts.Split('\n')[i]}");
            }
        }
    }
}
