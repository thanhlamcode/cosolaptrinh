using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DangNhap;
using System.Linq.Expressions;

namespace Login
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            //AttachDbFilename thay bằng đường dẫn được lưu ở máy bạn để có thể hoạt động được
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\84967\source\repos\thanhlamcode\DangNhap\DangNhap\Project_DB.mdf;Integrated Security=True";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                char accessauthority = ' ';

                // Chọn đăng nhập, đăng ký và thoát
                // Tham số thứ hai sử dụng Lambda - Kiểu delegate: dùng để lưu các tham chiêu method.
                // Lambda cũng là kiểu Anonymous Type: kiểu không có tên, trong TH này là một method không tên.
                // n bắt đầu là một tham số, sau dấu => sẽ thực thi code

                string noti1 = "1.Đăng nhập.\n2.Đăng Ký.\n3.Exit.\nNhập số (1 -> 3): ";
                int choose1 = Rogin.Nhap_Vao<int>(noti1, n => n >= 1 && n <= 3);
                Rogin.First_Move(connection, ref accessauthority, choose1);

                // Vai trò Admin Lọc user ảo, user rác
                if(accessauthority == 'A')
                {
                    string noti2 = "1.Delete user dựa trên số lần đăng nhập." +
                        "\n2.Delete user dựa trên số lần bị report." + "\n3.Delete user dựa trên số lần comment." +
                        "\n4. Delete user dựa trên userId.";

                    int choose2 = Rogin.Nhap_Vao<int>(noti2, n => n >= 1 && n <= 5);
                    Admin.Loc_Rac(connection, choose2);
                    // Vai trò Admin tiếp tục trong if này
                }    

                connection.Close();
            }
        }

    }
}
