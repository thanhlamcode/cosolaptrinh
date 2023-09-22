using System;
using System.Data.SqlClient;
using System.Text;

class Program
{
    static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dtlam\Source\Repos\thanhlamcode\DangNhap\DangNhap\Project_DB.mdf;Integrated Security=True";

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

    static void EditMovie(SqlConnection connection, int filmid)
    {
        // Kiểm tra xem filmid có tồn tại trong bảng MOVIE hay không.
        string checkExistQuery = "SELECT COUNT(*) FROM MOVIE WHERE filmid = @filmid";
        using (SqlCommand checkExistCommand = new SqlCommand(checkExistQuery, connection))
        {
            checkExistCommand.Parameters.AddWithValue("@filmid", filmid);
            int count = (int)checkExistCommand.ExecuteScalar();

            if (count == 0)
            {
                Console.Clear();
                Console.WriteLine("Filmid không tồn tại trong bảng MOVIE.\nVui lòng chọn một tùy chọn mới:\n");
                return; // Kết thúc hàm nếu filmid không tồn tại.
            }
        }

        string selectQuery = "SELECT * FROM MOVIE WHERE filmid = @filmid";
        using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
        {
            selectCommand.Parameters.AddWithValue("@filmid", filmid);
            SqlDataReader reader = selectCommand.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine("Thông tin hiện tại của bản ghi:");
                Console.WriteLine($"Film ID: {reader["filmid"]}");
                Console.WriteLine($"Tên phim: {reader["tenphim"]}");
                Console.WriteLine($"Thể loại: {reader["theloai"]}");
                Console.WriteLine($"Nhà sản xuất: {reader["nhasanxuat"]}");
                Console.WriteLine($"Số tập: {reader["sotap"]}");
                Console.WriteLine($"Lượt xem: {reader["luotxem"]}");
                Console.WriteLine($"Doanh thu: {reader["doanhthu"]}");
                Console.WriteLine($"Rating: {reader["rating"]}");
                reader.Close();
            }
        }

        Console.WriteLine("Nhập các thông tin mới:");

        string tenphim = "";
        while (string.IsNullOrEmpty(tenphim))
        {
            Console.Write("Tên phim: ");
            tenphim = Console.ReadLine();
            if (string.IsNullOrEmpty(tenphim))
            {
                Console.WriteLine("Yêu cầu nhập dữ liệu cho mục này!!");
            }
        }

        // Sau khi vòng lặp kết thúc, bạn có tên phim hợp lệ trong biến "tenphim".

        string theloai = "";
        while (string.IsNullOrEmpty(theloai))
        {
            Console.Write("Thể loại: ");
            theloai = Console.ReadLine();
            if (string.IsNullOrEmpty(theloai))
            {
                Console.WriteLine("Yêu cầu nhập dữ liệu cho mục này!!");
            }
        }

        string nhasanxuat = "";
        while (string.IsNullOrEmpty(nhasanxuat))
        {
            Console.Write("Nhà sản xuất: ");
            nhasanxuat = Console.ReadLine();
            if (string.IsNullOrEmpty(nhasanxuat))
            {
                Console.WriteLine("Yêu cầu nhập dữ liệu cho mục này!!");
            }
        }

        int sotap;

        while (true)
        {
            Console.Write("Số tập: ");
            if (int.TryParse(Console.ReadLine(), out sotap)) break;
            else Console.WriteLine("Dữ liệu không hợp lệ, vui lòng nhập lại!!");
        }

        int luotxem;
        while (true)
        {
            Console.Write("Lượt xem: ");
            if (int.TryParse(Console.ReadLine(), out luotxem)) break;
            else Console.WriteLine("Dữ liệu không hợp lệ, vui lòng nhập lại!!");
        }
        int doanhthu;
        while (true)
        {
            Console.Write("Doanh thu: ");
            if (int.TryParse(Console.ReadLine(), out doanhthu)) break;
            else Console.WriteLine("Dữ liệu không hợp lệ, vui lòng nhập lại!!");
        }

        double rating;

        while (true)
        {
            Console.Write("Rating: ");
            if (double.TryParse(Console.ReadLine(), out rating)) break;
            else Console.WriteLine("Dữ liệu không hợp lệ, vui lòng nhập lại!!");
        }

        string updateQuery = "UPDATE MOVIE " +
                             "SET tenphim = @tenphim, theloai = @theloai, nhasanxuat = @nhasanxuat, " +
                             "sotap = @sotap, luotxem = @luotxem, doanhthu = @doanhthu, rating = @rating " +
                             "WHERE filmid = @filmid";

        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
        {
            updateCommand.Parameters.AddWithValue("@tenphim", tenphim);
            updateCommand.Parameters.AddWithValue("@theloai", theloai);
            updateCommand.Parameters.AddWithValue("@nhasanxuat", nhasanxuat);
            updateCommand.Parameters.AddWithValue("@sotap", sotap);
            updateCommand.Parameters.AddWithValue("@luotxem", luotxem);
            updateCommand.Parameters.AddWithValue("@doanhthu", doanhthu);
            updateCommand.Parameters.AddWithValue("@rating", rating);
            updateCommand.Parameters.AddWithValue("@filmid", filmid);

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

            // Tiếp tục với việc cập nhật thông tin của bộ phim.
            // ...
        }

    static void AddNewMovie(SqlConnection connection)
    {
        Console.WriteLine("Nhập thông tin phim mới:");

        string tenphim = "";
        while (string.IsNullOrEmpty(tenphim))
        {
            Console.Write("Tên phim: ");
            tenphim = Console.ReadLine();
            if (string.IsNullOrEmpty(tenphim))
            {
                Console.WriteLine("Yêu cầu nhập dữ liệu cho mục này!!");
            }
        }

        // Sau khi vòng lặp kết thúc, bạn có tên phim hợp lệ trong biến "tenphim".

        string theloai = "";
        while (string.IsNullOrEmpty(theloai))
        {
            Console.Write("Thể loại: ");
            theloai = Console.ReadLine();
            if (string.IsNullOrEmpty(theloai))
            {
                Console.WriteLine("Yêu cầu nhập dữ liệu cho mục này!!");
            }
        }

        string nhasanxuat = "";
        while (string.IsNullOrEmpty(nhasanxuat))
        {
            Console.Write("Nhà sản xuất: ");
            nhasanxuat = Console.ReadLine();
            if (string.IsNullOrEmpty(nhasanxuat))
            {
                Console.WriteLine("Yêu cầu nhập dữ liệu cho mục này!!");
            }
        }

        int sotap;

        while (true)
        {
            Console.Write("Số tập: ");
            if (int.TryParse(Console.ReadLine(), out sotap)) break;
            else Console.WriteLine("Dữ liệu không hợp lệ, vui lòng nhập lại!!");
        }

        int luotxem;
        while (true)
        {
            Console.Write("Lượt xem: ");
            if (int.TryParse(Console.ReadLine(), out luotxem)) break;
            else Console.WriteLine("Dữ liệu không hợp lệ, vui lòng nhập lại!!");
        }
        int doanhthu;
        while (true)
        {
            Console.Write("Doanh thu: ");
            if (int.TryParse(Console.ReadLine(),out doanhthu)) break;
            else Console.WriteLine("Dữ liệu không hợp lệ, vui lòng nhập lại!!");
        }

        double rating;

        while (true)
        {
            Console.Write("Rating: ");
            if (double.TryParse(Console.ReadLine(), out rating)) break;
            else Console.WriteLine("Dữ liệu không hợp lệ, vui lòng nhập lại!!");
        }

        string insertQuery = "INSERT INTO MOVIE (tenphim, theloai, nhasanxuat, sotap, luotxem, doanhthu, rating) " +
                             "VALUES (@tenphim, @theloai, @nhasanxuat, @sotap, @luotxem, @doanhthu, @rating)";

        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
        {
            insertCommand.Parameters.AddWithValue("@tenphim", tenphim);
            insertCommand.Parameters.AddWithValue("@theloai", theloai);
            insertCommand.Parameters.AddWithValue("@nhasanxuat", nhasanxuat);
            insertCommand.Parameters.AddWithValue("@sotap", sotap);
            insertCommand.Parameters.AddWithValue("@luotxem", luotxem);
            insertCommand.Parameters.AddWithValue("@doanhthu", doanhthu);
            insertCommand.Parameters.AddWithValue("@rating", rating);

            int rowsAffected = insertCommand.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("\nPhim mới đã được thêm thành công.");
            }
            else
            {
                Console.WriteLine("\nKhông thể thêm phim mới.");
            }
        }
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            while (true)
            {
                Console.WriteLine("1. Hiển thị danh sách phim");
                Console.WriteLine("2. Chỉnh sửa thông tin phim");
                Console.WriteLine("3. Thêm phim mới");
                Console.WriteLine("4. Thoát");
                Console.Write("Chọn tùy chọn (1/2/3/4): ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            DisplayAllMovieNames(connection);
                            break;
                        case 2:
                            Console.Clear();
                            DisplayAllMovieNames(connection);
                            Console.Write("Nhập filmid của bản ghi bạn muốn chỉnh sửa: ");
                            int filmid = int.Parse(Console.ReadLine());
                            EditMovie(connection, filmid);
                            break;
                        case 3:
                            Console.Clear();
                            AddNewMovie(connection);
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("Ứng dụng đã kết thúc.");
                            return;
                        default:
                            Console.Clear();
                            Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                }
            }
        }
    }
}
