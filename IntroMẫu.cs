using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.Unicode;
        bool flag = false;
        string introtext = @"
            ________________ 
           |_|______________|
           | |
           | |
           | |______________ 
           | |____________| |
           | |            | |
           | |            | |
           | |____________| |
           |_|____________|_| 
            
         - Nhập Enter để bỏ qua -
        ";

        Task t1 = new Task(() => Logo(introtext, ref flag));
        t1.Start();
        while(!flag)
        {
            if(Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true).Key;
                if(key == ConsoleKey.Enter)
                {
                    flag = true;
                }
            }    
        }
        Console.Clear();
        string a1 = "Dự án đến từ nhóm CrazyDog";
        int screenWidth = Console.WindowWidth;
        int screenHeight = Console.WindowHeight;
        int leftMargin = (screenWidth - a1.Length) / 2;
        int topMargin = (screenHeight - 1) / 2;
        Console.SetCursorPosition(leftMargin, topMargin);
        Console.WriteLine(a1);
        Console.ReadKey();
    }
    static void Logo(string text, ref bool flag)
    {
        lock(Console.Out)
        {
            while (!flag)
            {
                int screenWidth = Console.WindowWidth;
                int screenHeight = Console.WindowHeight;

                // Tính toán vị trí in để đưa văn bản vào giữa màn hình
                int leftMargin = (screenWidth - text.Split('\n')[1].Length - 7) / 2;
                int topMargin = (screenHeight - text.Split('\n').Length) / 2;

                // Xóa nội dung cũ bằng Console.Clear()
                Console.Clear();

                // Tách các dòng văn bản và in chúng
                string[] lines = text.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    Console.SetCursorPosition(leftMargin, topMargin + i);
                    Console.WriteLine(lines[i]);
                }

                Thread.Sleep(500);
            }
        }    
    }
}

