using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Test4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            // Thay vì dùng StringBuilder thì có thể dùng string
            StringBuilder sb = new StringBuilder();
            int col = 0;
            int row = Console.CursorTop;
            Xuong_dong(sb, ref col, ref row);
        }

        static string Nhap_Vao()
        {
            string text;
            while(true)
            {
                text = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(text) && !string.IsNullOrEmpty(text))
                {
                    Console.Clear();
                    break;
                }    
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Values. Please try again.");
                }    
            }

            return text;
        }

        static void Xuong_dong(StringBuilder sn, ref int col, ref int row)
        {
            ConsoleKey Keypress;
            do
            {
                Console.Clear(); // Can thay thế bằng phương pháp hay hơn
                Nhap_lientuc(sn, ref col, ref row);
                var key = Console.ReadKey(intercept: true).Key;
                Keypress = key;
                if (Keypress == ConsoleKey.Spacebar) continue;
                 
            } while (Keypress != ConsoleKey.Enter); 
        }

        static void Nhap_lientuc(StringBuilder sn, ref int col, ref int row)
        {
            string text = null;
            int a = 0;
            Console.Write(sn.ToString());

            // Mục đích là không để con trỏ quay trở về dòng 0 cột 0 làm ghi đè lên dữ liệu ban đầu.
            // Còn khi nhập tiếp thì thả ga
            if (row == 0) Console.SetCursorPosition(sn.ToString().Length, row);
            else Console.SetCursorPosition(col, row);

            text = Console.ReadLine();
            Console.Clear();
            a = text.Length;

            // Phục vụ cho việc xuống dòng và kiểm tra liệu dòng đã đầy?
            if (col + a >= 50) // Can thay số 20 ở đây thành một biến
            {
                string[] check_text = text.Split(' ');
                for(int i = 0; i < check_text.Length; i++)
                {
                    if(col + check_text[i].Length <= 50)
                    {
                        string alpha_1 = check_text[i] + " ";
                        sn.Append(alpha_1);
                        col += alpha_1.Length;
                    }    
                    else
                    {
                        sn.Append("\n" + check_text[i] + " ");
                        col = check_text[i].Length;
                        row++; // -> Phục vụ cho việc nhập xuống dòng
                    }    
                }    
            }  
            else
            {
                sn.Append(text);
                col += text.Length;
            }
            int track_of_row = 0; // giá trị theo dõi số dòng được in ra -> Aim: nhằm phục vụ cho việc thực hiện in ra khung cuối cùng

            for (int i = 0; i < sn.ToString().Split('\n').Length; i++)
            {
                track_of_row++; // đếm số dòng bao gồm khung - > phục vụ cho UI lựa chọn
            }    

            // Set khung mặc định -> Làm được do sự giới hạn cỡ chữ -> Tránh việc phụ thuộc việc in ra khung, vị trí của nó dựa vào số chữ.
            int t1 = (Console.WindowWidth - 60) / 2; // Cột ra giữa
            int t2 = (Console.WindowHeight - (track_of_row + 2)) / 2; // Điều chỉnh để nó in ra giữa screen hay chỉ giữa dòng -> Nếu chỉ giữa dòng set = 0;
            
            Console.SetCursorPosition(t1, t2); 
            Console.WriteLine('+' + new string('-',58) + '+'); 
            // Setting sao cho mỗi dòng in ra một chữ một khung đi kèm với khoảng cách.
            for(int i = 0; i < sn.ToString().Split('\n').Length; i++)
            {
                int mid_2 = (50 - sn.ToString().Split('\n')[i].Length) / 2; // Dùng để hiển thị text ở giữa của khung
                Console.SetCursorPosition(t1, t2 + 1 + i); 
                Console.Write("|"); 
                Console.SetCursorPosition(t1 + 6 + mid_2, t2 + 1 + i);
                Console.WriteLine($"{sn.ToString().Split('\n')[i]}");
                Console.SetCursorPosition(t1 + 59, t2 + 1 + i); 
                Console.WriteLine("|"); 
            }

            Console.SetCursorPosition(t1, t2 + track_of_row + 1); 
            Console.WriteLine('+' + new string('-',58) + '+'); 
            Console.WriteLine("Ấn một phím bất kỳ ngoài Enter để tiếp tục nhập.");
        }
    }
}
