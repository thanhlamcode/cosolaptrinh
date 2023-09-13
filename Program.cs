using System;
using System.Data.SqlClient;
using System.Text;

namespace user
{
    internal class Program
    {
        static void addReview(SqlConnection connection)
        {
            Console.WriteLine("Thêm reiview:");

            Console.Write("Viết bình luận của bạn: ");
            string comment = Console.ReadLine();

            double rating;
            while (true)
            {
                Console.Write("Số điểm Rating của bạn: ");
                if (double.TryParse(Console.ReadLine(), out rating) && rating >= 0 && rating <= 10)
                    break;
                else
                    Console.WriteLine("Không hợp lệ. Vui lòng cho điểm lại trên thang từ 0 đến 10");
            }

            string updateQuery = "UPDATE COMMENT " +
                             "SET comment = @comment" +
                             "rating = @rating" +
                             "WHERE review = @review";

            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {
                updateCommand.Parameters.AddWithValue("@comment", comment);
                updateCommand.Parameters.AddWithValue("@rating", rating);

                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Dữ liệu đã được cập nhật thành công.");
                }
                else
                {
                    Console.WriteLine("Không có bản ghi nào được cập nhật.");
                }
            }
        }

        static void viewAllReview(SqlConnection connection)
        {
            string query = "SELECT commnet, rating FROM REVIEW";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Danh sách tất cả các review:");

                while (reader.Read())
                {
                    string comment = reader["comment"].ToString();
                    double rating = (double)reader["rating"];
                    Console.WriteLine($"Comment: {comment}, rating: {rating}");
                }

                reader.Close();
            }
        }
        
        private static string connectionString;
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                while (true)
                {
                    Console.WriteLine("1. Thêm hoặc sửa review");
                    Console.WriteLine("2. Xem tất cả review");
                    Console.Write("Chọn tùy chọn 1, 2: ");

                    int choice;
                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                addReview(connection);
                                break;
                            case 2:
                                viewAllReview(connection);
                                break;
                            default:
                                Console.WriteLine("Không hợp lệ. Vui lòng chọn lại");
                                break;
                        }
                    }
                    else Console.WriteLine("Không hợp lệ. Vui lòng chọn lại");
                }
            }   
        }
    }
}
