using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Film3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //chuỗi kết nối đến csdl
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asus\source\repos\thanhlamcode\DangNhap\DangNhap\Project_DB.mdf;Integrated Security=True";
            //tạo kết nối tới csdl
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //tạo đối tượng Sqlcommand
                using (var command = connection.CreateCommand())
                {
                    //thiết lập câu truy vấn sql (ở đây dùng ORDER BY để xếp rating giảm dần)
                    command.CommandText = "Select filmid, tenphim, theloai, nhasanxuat, sotap, luotxem, doanhthu, rating FROM MOVIE WHERE filmid >= @filmid ORDER BY rating DESC";
                    //thiết lập tham số 
                    var FilmId = command.Parameters.AddWithValue("@filmid", 5);
                    FilmId.Value = 0;
                    //thực thi câu truy vấn
                    var sqlreader = command.ExecuteReader();
                    //kiểm tra có trả về dữ liệu k ?
                    if (sqlreader.HasRows)
                    {

                        Console.WriteLine("Xep hang cac bo phim :");
                        //đọc kq và in ra 
                        while (sqlreader.Read())
                        {
                            var id = sqlreader.GetInt32(0);
                            var ten = sqlreader["tenphim"];
                            var ranked = sqlreader["rating"];
                            var lxem = sqlreader.GetInt32(6);
                            Console.WriteLine($"rating: {ranked} - {ten} ({lxem} luot xem)");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Khong co du lieu phim nay");
                    }
                    Console.WriteLine("Ban muon xem bo phim nao ?");
                }
                //đóng kết nối
                connection.Close();
            }
        }
    }
}
