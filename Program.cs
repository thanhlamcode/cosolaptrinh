using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4
{
    internal class Program
    {
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\source\repos\thanhlamcode\DangNhap\DangNhap\Project_DB.mdf;Integrated Security=True";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                while (true)
                {
                    Console.WriteLine("==== Chương trình quản lý báo cáo bình luận ====");
                    Console.WriteLine("1. Thêm báo cáo bình luận");
                    Console.WriteLine("2. Đổi mật khẩu người dùng");
                    Console.WriteLine("0. Thoát chương trình");
                    Console.WriteLine("==============================================");

                    Console.Write("Vui lòng chọn một tùy chọn: ");
                    int option = Convert.ToInt32(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            AddCommentReport(connection);
                            break;
                        case 2:
                            ChangeUserPassword(connection);
                            break;
                        case 0:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại.");
                            break;
                    }

                    Console.WriteLine();
                }
            }
        }

        static void AddCommentReport(SqlConnection connection)
        {
            Console.WriteLine("==== Thêm báo cáo bình luận ====");
            Console.Write("Nhập tên người dùng (username): ");
            string username = Console.ReadLine();

            // Kiểm tra xem người dùng có tồn tại trong cơ sở dữ liệu hay không
            string checkQuery = "SELECT COUNT(*) FROM Account WHERE Username = @Username";
            using (var command = new SqlCommand(checkQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                int accountCount = (int)command.ExecuteScalar();

                if (accountCount > 0)
                {
                    // Tăng giá trị ReportTime trong bảng Account
                    string updateQuery = "UPDATE Account SET ReportTime = ReportTime + 1 WHERE Username = @Username";
                    using (var updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Username", username);
                        updateCommand.ExecuteNonQuery();
                    }

                    Console.WriteLine("Báo cáo bình luận thành công.");
                }
                else
                {
                    Console.WriteLine("Người dùng không tồn tại.");
                }
            }
        }
        static void ChangeUserPassword(SqlConnection connection)
        {
            Console.WriteLine("==== Đổi mật khẩu người dùng ====");
            Console.Write("Nhập tên người dùng (username): ");
            string username = Console.ReadLine();

            // Kiểm tra xem người dùng có tồn tại trong cơ sở dữ liệu hay không
            string checkUserQuery = "SELECT COUNT(*) FROM Account WHERE Username = @Username";
            using (var checkUserCommand = new SqlCommand(checkUserQuery, connection))
            {
                checkUserCommand.Parameters.AddWithValue("@Username", username);
                int userCount = (int)checkUserCommand.ExecuteScalar();

                if (userCount > 0)
                {
                    Console.Write("Nhập mật khẩu cũ: ");
                    string oldPassword = Console.ReadLine();

                    // Kiểm tra mật khẩu cũ có khớp với người dùng hay không
                    string checkPasswordQuery = "SELECT COUNT(*) FROM Account WHERE Username = @Username AND Password = @OldPassword";
                    using (var checkPasswordCommand = new SqlCommand(checkPasswordQuery, connection))
                    {
                        checkPasswordCommand.Parameters.AddWithValue("@Username", username);
                        checkPasswordCommand.Parameters.AddWithValue("@OldPassword", oldPassword);
                        int passwordCount = (int)checkPasswordCommand.ExecuteScalar();

                        if (passwordCount > 0)
                        {
                            Console.Write("Nhập mật khẩu mới: ");
                            string newPassword = Console.ReadLine();

                            // Cập nhật mật khẩu mới cho người dùng
                            string updateQuery = "UPDATE Account SET Password = @NewPassword WHERE Username = @Username";
                            using (var updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@NewPassword", newPassword);
                                updateCommand.Parameters.AddWithValue("@Username", username);
                                int rowsAffected = updateCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    Console.WriteLine("Đổi mật khẩu thành công.");
                                }
                                else
                                {
                                    Console.WriteLine("Đổi mật khẩu không thành công.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Mật khẩu cũ không chính xác.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Người dùng không tồn tại.");
                }
            }
        }
    }
}