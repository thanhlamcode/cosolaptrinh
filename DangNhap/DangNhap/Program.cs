using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DangNhap;

namespace Login
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\84967\source\repos\thanhlamcode\DangNhap\DangNhap\Project_DB.mdf;Integrated Security=True";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                char accessauthority = ' ';
                // Chọn đăng nhập, đăng ký và thoát
                int choose1 = Rogin.Nhap_Vao(1, 3);
                Rogin.First_Move(connection, ref accessauthority, choose1);

                // Vai trò Admin Lọc user ảo, user rác
                if(accessauthority == 'A')
                {
                    int choose2 = Rogin.Nhap_Vao(1, 5);
                    Admin.Loc_Rac(connection, choose2);
                }    

                connection.Close();
            }
        }

    }
}
