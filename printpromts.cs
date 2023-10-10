using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Print_Prompts(string label, string value, int maxWidth = 70)
        {
            int leftMargin = 0; // Độ lệch bên trái
            int topMargin = 2; // Độ lệch từ đỉnh cửa sổ console

            // Tạo khung
            Console.SetCursorPosition(leftMargin, topMargin - 1);
            Console.WriteLine("╔" + new string('═', maxWidth) + "╗");

            // Tách dữ liệu thành các dòng con nếu quá dài
            string[] words = value.Split(new[] { ' ' }, StringSplitOptions.None);
            StringBuilder currentLine = new StringBuilder(label);

            foreach (string word in words)
            {
                if (currentLine.Length + word.Length + 1 > maxWidth)
                {
                    // Nếu dòng hiện tại quá dài, in ra và bắt đầu dòng mới
                    Console.SetCursorPosition(leftMargin, topMargin);
                    Console.WriteLine("║" + currentLine.ToString().PadRight(maxWidth) + "║");
                    topMargin++;

                    currentLine.Clear();
                    currentLine.Append("    "); // Để bỏ trống độ lệch và khoảng trắng cho label
                }

                currentLine.Append(" " + word);
            }

            // In dòng cuối cùng của dữ liệu
            Console.SetCursorPosition(leftMargin, topMargin);
            Console.WriteLine("║" + currentLine.ToString().PadRight(maxWidth) + "║");
            topMargin++;

            Console.SetCursorPosition(leftMargin, topMargin);
            Console.WriteLine("╚" + new string('═', maxWidth) + "╝");

            Console.WriteLine("Ấn một phím bất kỳ ngoài Enter để tiếp tục nhập.");
        }


        static void Main(string[] args)
        {
            string label = "Label: hom nay la ngay muoi thang muoi nam";
            string longText = "This is a very long text that needs to be wrapped into multiple lines if it exceeds the maximum width of the frame. This is a test text.";
            Print_Prompts(label, longText);
            Console.ReadKey();
        }
    }
}
