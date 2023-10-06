using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QuerySystemIO
{
    class Program
    {
        static string dataFilePath = @"C:\Users\HP\OneDrive\Tài liệu\N\n.txt"; // Đường dẫn đến tệp văn bản chứa dữ liệu

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;
            List<string> dataLines = File.ReadAllLines(dataFilePath).ToList();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════╗");
                Console.WriteLine("║          Chương trình truy vấn dữ liệu         ║");
                Console.WriteLine("╠════════════════════════════════════════════════╣");
                Console.WriteLine("║    1. Thêm báo cáo bình luận                   ║");
                Console.WriteLine("║    2. Đổi mật khẩu người dùng                  ║");
                Console.WriteLine("║    0. Thoát chương trình                       ║");
                Console.WriteLine("╚════════════════════════════════════════════════╝");
                Console.Write("Vui lòng chọn một tùy chọn: ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AddCommentReport(dataLines);
                        break;
                    case 2:
                        ChangeUserPassword(dataLines);
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

        static void AddCommentReport(List<string> dataLines)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           Thêm báo cáo bình luận               ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            string username = "";

            do
            {
                Console.Write("Nhập tên người dùng (username): ");
                username = Console.ReadLine();

                // Kiểm tra xem người dùng có tồn tại trong dữ liệu hay không
                string userData = dataLines.FirstOrDefault(line => line.Contains(username));
                if (userData != null)
                {
                    // Lấy số lần báo cáo hiện tại của người dùng
                    int reportTime = int.Parse(userData.Split(',')[4].Trim());
                    reportTime++;

                    // Cập nhật dữ liệu người dùng
                    for (int i = 0; i < dataLines.Count; i++)
                    {
                        if (dataLines[i].Contains(username))
                        {
                            // Cập nhật số lần báo cáo mới
                            string[] userDataParts = dataLines[i].Split(',');
                            userDataParts[4] = reportTime.ToString();
                            dataLines[i] = string.Join(",", userDataParts);
                            break;
                        }
                    }

                    // Ghi lại dữ liệu vào tệp
                    File.WriteAllLines(dataFilePath, dataLines);

                    Console.WriteLine("Báo cáo bình luận thành công.");
                    Console.ReadLine(); // Wait for user input
                    return;
                }

                Console.WriteLine("Người dùng không tồn tại. Vui lòng thử lại.");
            } while (true);
        }

        static void ChangeUserPassword(List<string> dataLines)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║         Đổi mật khẩu người dùng                ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            string username = "";

            while (true)
            {
                Console.Write("Nhập tên người dùng (username): ");
                username = Console.ReadLine();

                // Kiểm tra xem người dùng có tồn tại trong dữliệu hay không
                string userData = dataLines.FirstOrDefault(line => line.Contains(username));
                if (userData != null)
                {
                    break;
                }

                Console.WriteLine("Tên người dùng không tồn tại. Vui lòng thử lại.");
            }

            Console.Write("Nhập mật khẩu cũ: ");
            string oldPassword = Console.ReadLine();
            string passwordData = dataLines.FirstOrDefault(line => line.Contains(username) && line.Contains(oldPassword));
            while (passwordData == null)
            {
                Console.WriteLine("Mật khẩu cũ không chính xác. Vui lòng thử lại.");
                Console.Write("Nhập mật khẩu cũ: ");
                oldPassword = Console.ReadLine();
                passwordData = dataLines.FirstOrDefault(line => line.Contains(username) && line.Contains(oldPassword));
            }

            Console.Write("Nhập mật khẩu mới: ");
            string newPassword = Console.ReadLine();

            // Cập nhật mật khẩu mới trong dữ liệu
            for (int i = 0; i < dataLines.Count; i++)
            {
                if (dataLines[i].Contains(username) && dataLines[i].Contains(oldPassword))
                {
                    string[] userDataParts = dataLines[i].Split(',');
                    userDataParts[2] = newPassword;
                    dataLines[i] = string.Join(",", userDataParts);
                    break;
                }
            }

            // Ghi lại dữ liệu vào tệp
            File.WriteAllLines(dataFilePath, dataLines);
            Console.WriteLine("Đổi mật khẩu thành công.");
            Console.ReadLine(); // Wait for user input
        }
    }
}