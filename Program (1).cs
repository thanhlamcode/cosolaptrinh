using System;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;

namespace user
{
    internal class Program
    {
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dtlam\Source\Repos\thanhlamcode\DangNhap\DangNhap\Project_DB.mdf;Integrated Security=True";

        static void addReview(SqlConnection connection)
        {
            try
            {
                Console.Write("Nhập phim id: ");
                int filmid = int.Parse(Console.ReadLine());

                Console.Write("Nhập account id: ");
                int accountId = int.Parse(Console.ReadLine());

                Console.WriteLine("Thêm reiview.");

                Console.Write("Bình luận: ");
                string cmt = Console.ReadLine();

                double rating;
                while (true)
                {
                    Console.Write("Số điểm Rating của bạn: ");
                    if (double.TryParse(Console.ReadLine(), out rating) && rating >= 0 && rating <= 10)
                        break;
                    else
                        Console.WriteLine("Không hợp lệ. Vui lòng cho điểm lại trên thang từ 0 đến 10");
                }

                string insertQuery = "INSERT INTO CMT (filmid, accountId, cmt, rating) " +
                                 "VALUES (@filmid, @accountId, @cmt, @rating)";

                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@filmid", filmid);
                    insertCommand.Parameters.AddWithValue("@accountId", accountId);
                    insertCommand.Parameters.AddWithValue("@cmt", cmt);
                    insertCommand.Parameters.AddWithValue("@rating", rating);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Bình luận của bạn đã được thêm thành công.");
                    }
                    else
                    {
                        Console.WriteLine("Không thể thêm bình luận.");
                    }
                }
            }
            catch (Exception c)
            {
                Console.WriteLine($"Error: {c.Message}");
            }
        }

        static void editReview(SqlConnection connection, int cmtid)
        {
            try
            {
                string selectQuery = "SELECT * FROM CMT WHERE cmtid = @cmtid";
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@cmtid", cmtid);
                    SqlDataReader reader = selectCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        Console.WriteLine("Bình luận hiện tại:");
                        Console.WriteLine($"Comment: {reader["cmt"]}");
                        Console.WriteLine($"Rating: {reader["rating"]}");
                        reader.Close();
                    }
                }

                Console.WriteLine("Bình luận mới.");

                Console.Write("Bình luận: ");
                string cmt = Console.ReadLine();

                string updateQuery = "UPDATE CMT " +
                                     "SET cmt = @cmt " +
                                     "WHERE cmtid = @cmtid";

                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@cmt", cmt);
                    updateCommand.Parameters.AddWithValue("@cmtid", cmtid);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Bình luận đã được cập nhật thành công.");
                    }
                    else
                    {
                        Console.WriteLine("Không có bình luận nào được cập nhật.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        static void viewAllReview(SqlConnection connection)
        {
            string query = "SELECT cmt, rating FROM CMT";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Danh sách tất cả các review:");

                while (reader.Read())
                {
                    string cmt = reader["cmt"].ToString();
                    double rating = (double)reader["rating"];
                    Console.WriteLine($"cmt: {cmt}, rating: {rating}");
                }

                reader.Close();
            }
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                while (true)
                {
                    Console.WriteLine("1. Thêm bình luận");
                    Console.WriteLine("2. Sửa bình luận");
                    Console.WriteLine("3. Xem tất cả bình luận");
                    Console.Write("Chọn tùy chọn 1, 2, 3: ");

                    int choice;
                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                addReview(connection);
                                break;
                            case 2:
                                int cmtid;
                                while (true)
                                {
                                    Console.Write("Nhập comment id bạn muốn chỉnh sửa: ");
                                    if (int.TryParse(Console.ReadLine(), out cmtid) && cmtid > 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Không hợp lệ. Vui lòng nhập lại.");
                                    }
                                }
                                editReview(connection, cmtid);
                                break;
                            case 3:
                                viewAllReview(connection);
                                break;
                            default:
                                Console.WriteLine("Không hợp lệ. Vui lòng chọn lại");
                                break;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Không hợp lệ. Vui lòng chọn lại");
                    }
                }
            }
        }
    }
}
