using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Film4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //chuỗi kết nối đến csdl
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asus\source\repos\thanhlamcode\DangNhap\DangNhap\Project_DB.mdf;Integrated Security=True";
            //nhập thông tin từ user
            Console.WriteLine("Nhap thong tin phim ( ten hoac the loai ):");
            string searchTerm = Console.ReadLine();
            //tạo danh sách để lưu kq tìm kiếm
            List<string> searchResults = new List<string>();
            //tạo kết nối tới csdl
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //tạo đối tượng Sqlcommand
                using (var command = connection.CreateCommand())
                {
                    //thiết lập câu truy vấn sql với tham số tìm kiếm
                    command.CommandText = "Select filmid, tenphim, theloai, nhasanxuat, sotap, luotxem, doanhthu, rating FROM MOVIE WHERE tenphim LIKE @searchTerm OR theloai LIKE @searchTerm";
                    command.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");
                    //thực thi câu truy vấn 
                    using (var reader = command.ExecuteReader())
                    {
                        //ktra kq có trả về dlieu k ?
                        if (reader.HasRows)
                        {
                            Console.WriteLine("Ket qua tim kiem :");
                            while (reader.Read())
                            {
                                var tenPhim = reader.GetString(1);
                                var theLoai = reader.GetString(2);
                                var nsx = reader.GetString(3);
                                var soTap = reader.GetInt32(4);
                                searchResults.Add($"{tenPhim} ({soTap} tap)\n - the loai phim :{theLoai}\n - NSX :{nsx}");
                            }
                            foreach (var results in searchResults)
                            {
                                Console.WriteLine(results);
                            }
                        }
                        else { Console.WriteLine("Khong tim thay phim phu hop.");}
                    }
                }
                //đóng kết nối
                connection.Close();
            }
        }
    }
}
