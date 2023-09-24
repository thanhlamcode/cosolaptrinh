using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void DeleteCommentByCmtId(SqlConnection connection)
    {
        Console.Write("Nhập cmtid để xoá comment: ");
        if (int.TryParse(Console.ReadLine(), out int cmtId))
        {
            string deleteQuery = "DELETE FROM CMT WHERE cmtid = @cmtid";

            using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
            {
                cmd.Parameters.AddWithValue("@cmtid", cmtId);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Xoá comment thành công.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy comment với cmtid này hoặc có lỗi xảy ra khi xoá.");
                }
            }
        }
        else
        {
            Console.WriteLine("Vui lòng nhập một số nguyên cho cmtid.");
        }
    }

    static void DisplayAllComments(SqlConnection connection)
    {
        string query = "SELECT cmtid, filmid, accountId, cmt, rating FROM CMT";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Danh sách tất cả các comments:");

            while (reader.Read())
            {
                int cmtid = (int)reader["cmtid"];
                int filmid = (int)reader["filmid"];
                int accountId = (int)reader["accountId"];
                string cmt = reader["cmt"].ToString();
                double rating = (double)reader["rating"];

                Console.WriteLine($"CMT ID: {cmtid}, Film ID: {filmid}, Account ID: {accountId}, Comment: {cmt}, Rating: {rating}");
            }

            reader.Close();
        }
    }

    static void DisplayAllMovieNames(SqlConnection connection)
    {
        string query = "SELECT filmid, tenphim FROM MOVIE";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Danh sách tất cả các tên phim cùng với filmid:");

            while (reader.Read())
            {
                int filmid = (int)reader["filmid"];
                string tenphim = reader["tenphim"].ToString();
                Console.WriteLine($"Film ID: {filmid}, Tên phim: {tenphim}");
            }

            reader.Close();
        }
    }

    static void Main()
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MyPC\source\repos\thanhlamcode\DangNhap\DangNhap\Project_DB.mdf;Integrated Security=True";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Hiển thị danh sách tên phim ngay từ đầu
                DisplayAllMovieNames(connection);

                int choice;

                do
                {
                    Console.WriteLine("----- Menu -----");
                    Console.WriteLine("1. Nhập filmId và thêm đánh giá");
                    Console.WriteLine("2. Hiển thị danh sách cmtid");
                    Console.WriteLine("3. Xoá comment theo cmtid");
                    Console.WriteLine("4. Thoát");
                    Console.Write("Chọn một tùy chọn (1-4): ");

                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                // Nhập filmId và thêm đánh giá
                                Console.Write("Nhập filmId: ");
                                int filmId = int.Parse(Console.ReadLine());

                                Console.Write("Nhập accountId: ");
                                int accountId = int.Parse(Console.ReadLine());

                                Console.Write("Nhập nội dung đánh giá: ");
                                string comment = Console.ReadLine();

                                Console.Write("Nhập điểm đánh giá (vd: 4.5): ");
                                double rating = double.Parse(Console.ReadLine());

                                // Tạo câu lệnh SQL để thêm đánh giá vào bảng CMT
                                string insertSql = "INSERT INTO CMT (filmid, accountId, cmt, rating) VALUES (@filmid, @accountId, @cmt, @rating)";

                                using (SqlCommand command = new SqlCommand(insertSql, connection))
                                {
                                    // Thêm tham số vào câu lệnh SQL
                                    command.Parameters.AddWithValue("@filmid", filmId);
                                    command.Parameters.AddWithValue("@accountId", accountId);
                                    command.Parameters.AddWithValue("@cmt", comment);
                                    command.Parameters.AddWithValue("@rating", rating);

                                    // Thực thi câu lệnh SQL để thêm đánh giá
                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        Console.WriteLine("Đánh giá đã được thêm thành công.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Không thể thêm đánh giá.");
                                    }
                                }
                                break;

                            case 2:
                                // Hiển thị danh sách cmtid
                                DisplayAllComments(connection);
                                break;

                            case 3:
                                // Xoá comment theo cmtid
                                DeleteCommentByCmtId(connection);
                                break;

                            case 4:
                                Console.WriteLine("Chương trình kết thúc.");
                                break;

                            default:
                                Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Vui lòng nhập một số nguyên từ 1 đến 4.");
                    }
                } while (choice != 4);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }
    }
}
