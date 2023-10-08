using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outside_Interface;
using Admin;

namespace Cinescore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            // gate_next dùng để chuyển sang chức năng khác
            // gate-end dùng để kết thúc chương trình
            bool gate_next = false;
            bool gate_end = false;

            // Nếu true là phân quyền Admin, false là phân quyền user -> Kết quả được gán dựa vào hàm Dang_Nhap
            bool accessauthority = false;
            // Giúp người dùng nhận biết Account Id, Username và Phân quyền của mình dễ dàng hơn
            string accountnotify = " ";
            string filepath = @"C:\Users\84967\OneDrive\Máy tính\Truyvan.txt";

            string title = Logo.Logo_App();
            Logo.Run_Intro();

            while (!gate_next && !gate_end)
            {
                Giao_dien_ngoai.First_I(filepath, title, ref accountnotify, ref accessauthority, ref gate_next, ref gate_end);
            }
            gate_next = false;

            if (accessauthority)
            {
                // Phần làm admin
                while (!gate_next && !gate_end)
                {
                    Loc_USER.Bin(filepath, accountnotify, ref gate_next, ref gate_end);
                    Console.ReadKey(); // Dùng để nhận giá trị nhập vào từ bàn phím user
                                       // -> Mục đích để hiện thị thông báo sau khi lọc rác thành công
                }
            }
            else
            {
                // Phần làm User
            }
        }
    }
}
