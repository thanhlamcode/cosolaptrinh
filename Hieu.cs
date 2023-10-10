using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hieu
{
    class ReportAndPassword
    {
        static string dataFilePath = @"D:\code\vs - C#\gr_project\new proj\n.txt"; // Đường dẫn đến tệp văn bản chứa dữ liệu

        static void Print_Prompts(string label, string value)
        {
            int t1 = (Console.WindowWidth - 60) / 2;
            int mid_2 = (50 - (label.Length + value.Length)) / 2;

            Console.SetCursorPosition(t1, Console.CursorTop);
            Console.Write("║");
            Console.SetCursorPosition(t1 + 6 + mid_2, Console.CursorTop);
            Console.Write($"{label} {value}");
            Console.SetCursorPosition(t1 + 59, Console.CursorTop);
            Console.WriteLine("║");
        }

        static void Menuchonlua()
        {
            string[] menuOptions = { "Thêm báo cáo bình luận", "Đổi mật khẩu người dùng", "Thoát chương trình" };
            string prompts = "Chương trình truy vấn dữ liệu\n" +
                             "Chọn tùy chọn:";

            Menu menu = new Menu(menuOptions, prompts);
            int selectedIndex = menu.Run();

            ProcessSelectedOption(selectedIndex);

            Console.WriteLine("Nhấn phím bất kỳ để thoát.");
            Console.ReadKey();
        }

        static void ProcessSelectedOption(int selectedIndex)
        {
            bool check = true;
            List<string> dataLines = File.ReadAllLines(dataFilePath).ToList();
            do
            {
                switch (selectedIndex)
                {
                    case 0:
                        // Xử lý tùy chọn 1
                        Console.Clear();
                        AddCommentReport(dataLines);
                        Console.ReadLine();
                        Menuchonlua();
                        break;
                    case 1:
                        // Xử lý tùy chọn 2
                        Console.Clear();
                        ChangeUserPassword(dataLines);
                        Console.ReadLine();
                        Menuchonlua();
                        break;
                    case 2:
                        // Xử lý tùy chọn 3
                        Console.WriteLine("Bạn đã chọn Thoát.");
                        check = false;
                        Console.WriteLine("Chương trình đang kết thúc...................");
                        break;
                }
            }
            while (check);
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Menuchonlua();
        }

        static void AddCommentReport(List<string> dataLines)
        {
            Console.Clear();
            Print_Prompts("Thêm báo cáo bình luận ", "");
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
                    return;
                }

                Console.WriteLine("Người dùng không tồn tại. Vui lòng thử lại.");
            } while (true);
        }

        static void ChangeUserPassword(List<string> dataLines)
        {
            Console.Clear();
            Print_Prompts("Đổi mật khẩu người dùng", "");
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
            Print_Prompts("Tên người dùng: ", username);
            Print_Prompts("Mật khẩu mới: ", newPassword);
        }
    }

}
