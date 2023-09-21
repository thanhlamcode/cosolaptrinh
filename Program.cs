using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding= Encoding.UTF8;

                // Chuỗi kết nối tới cơ sở dữ liệu
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\source\repos\thanhlamcode\DangNhap\DangNhap\Project_DB.mdf;Integrated Security=True";
                using (var connection = new SqlConnection(connectionString))
                    
                try
                {
                    {
                        connection.Open();

                            // ID của phim cần xem thông tin chi tiết
                            int filmId = 1;

                            // Truy vấn thông tin chi tiết về phim từ cơ sở dữ liệu
                            string query = "SELECT Title, Rank, Rating, Summary FROM Films WHERE FilmId = @FilmId";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@FilmId", filmId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Lấy thông tin từ dữ liệu trả về
                                string title = reader.GetString(0);
                                int rank = reader.GetInt32(1);
                                double rating = reader.GetDouble(2);
                                string summary = reader.GetString(3);

                                // Hiển thị thông tin chi tiết về phim
                                Console.WriteLine("Title: " + title);
                                Console.WriteLine("Rank: " + rank);
                                Console.WriteLine("Rating: " + rating);
                                Console.WriteLine("Summary: " + summary);
                            }
                            else
                            {
                                Console.WriteLine("Phim không tồn tại.");
                            }

                        }
                            connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }
            }
        }
    }
}
