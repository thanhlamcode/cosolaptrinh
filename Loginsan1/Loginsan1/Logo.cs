using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loginsan1
{
    internal class Logo
    {
        public static string Logo_UEH()
        {
            string introtext = @"
            ______       ______ __________________ ______         ______
            |    |       |    | |                | |    |         |    |
            |    |       |    | |    ____________| |    |         |    |
            |    |       |    | |    |             |    |         |    |
            |    |       |    | |    |____________ |    |_________|    |
            |    |       |    | |                | |                   |
            |    |       |    | |    ____________| |    ___________    |
            |    |_______|    | |    |             |    |         |    |
            |                 | |    |____________ |    |         |    |
            |                 | |                | |    |         |    |
            |_________________| |________________| |____|         |____|
                                 _                    _ _         
                                (_)                  (_| |        
                     _   _ _ __  ___   _____ _ __ ___ _| |_ _   _ 
                    | | | | '_ \| \ \ / / _ | '__/ __| | __| | | |
                    | |_| | | | | |\ V |  __| |  \__ | | |_| |_| |
                     \__,_|_| |_|_| \_/ \___|_|  |___|_|\__|\__, |
                                                             __/ |
                                                            |___/ 



                             - Nhập Enter để bỏ qua -
        ";

            return introtext;
        }

        public static string Logo_App()
        {
            string title = @" 
██████╗██╗███╗   ██╗███████╗███████╗ ██████╗ ██████╗ ██████╗ ███████╗
██╔════╝██║████╗  ██║██╔════╝██╔════╝██╔════╝██╔═══██╗██╔══██╗██╔════╝
██║     ██║██╔██╗ ██║█████╗  ███████╗██║     ██║   ██║██████╔╝█████╗  
██║     ██║██║╚██╗██║██╔══╝  ╚════██║██║     ██║   ██║██╔══██╗██╔══╝  
╚██████╗██║██║ ╚████║███████╗███████║╚██████╗╚██████╔╝██║  ██║███████╗
 ╚═════╝╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚══════╝                                                       
        ";
            return title;
        }

        public static void Run_Intro()
        {
            string introtext = Logo_UEH();
            string title = Logo_App();
            bool flag = false;

            Task t1 = new Task(() => Logo_Print(introtext, ref flag));
            Task t2 = new Task(() => TenNhom(ref flag));

            t1.Start();
            Waiting_for_you(ref flag);
            t1.Wait();
            flag = false;
            Console.Clear();
            t2.Start();
            Waiting_for_you(ref flag);
            t2.Wait();
        }
        private static void Logo_Print(string text, ref bool flag)
        {
            lock (Console.Out)
            {
                while (!flag)
                {
                    int screenWidth = Console.WindowWidth;
                    int screenHeight = Console.WindowHeight;

                    // Tính toán vị trí in để đưa văn bản vào giữa màn hình
                    int leftMargin = (screenWidth - text.Split('\n')[1].Length - 12) / 2;
                    int topMargin = (screenHeight - text.Split('\n').Length) / 2;

                    // Xóa nội dung cũ bằng Console.Clear()
                    Console.Clear();

                    // Tách các dòng văn bản và in chúng
                    string[] lines = text.Split('\n');

                    // Kiểm tra nếu vị trí đặt con trỏ nằm ngoài giới hạn thì không đặt
                    if (leftMargin > 0 && topMargin > 0 && leftMargin < screenWidth && topMargin < screenHeight)
                    {
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

        private static void TenNhom(ref bool flag)
        {
            lock (Console.Out)
            {
                while (!flag)
                {
                    string a1 = "Một sản phẩm đến từ nhóm VN5";
                    int screenWidth = Console.WindowWidth;
                    int screenHeight = Console.WindowHeight;
                    int leftMargin = (screenWidth - a1.Length) / 2;
                    int topMargin = (screenHeight - 1) / 2;
                    Console.Clear();
                    if (leftMargin > 0 && topMargin > 0 && leftMargin < screenWidth && topMargin < screenHeight)
                    {
                        Console.SetCursorPosition(leftMargin, topMargin);
                        Console.WriteLine(a1);
                    }
                    Thread.Sleep(500);
                }
            }
        }

        private static void Waiting_for_you(ref bool flag)
        {
            bool localflag = flag;
            Task.Delay(2000).ContinueWith(b =>
            {
                localflag = true;
            });

            while (!localflag)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true).Key;
                    if (key == ConsoleKey.Enter)
                    {
                        localflag = true;
                    }
                }

            }

            flag = localflag;
        }
    }
}
