using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outside_Interface;

namespace User
{
    internal class Report_A_Password
    {
        static string File_F_Report_Password = @"C:\Users\84967\OneDrive\Máy tính\Truyvan.txt";

        public static void R_And_P()
        {
            List<string> dataLines = File.ReadAllLines(File_F_Report_Password).ToList();
            string[] menuOptions = { "Thêm báo cáo bình luận", "Đổi mật khẩu người dùng", "Quay lại" };
            string prompts = "Chương trình truy vấn dữ liệu\n";
            int rac = 0;

            Menu menu = new Menu(menuOptions, prompts);
            int choice_Phu = menu.Run(ref rac);

            switch (choice_Phu)
            {
                case 0:
                    Console.Clear();
                    AddCommentReport(dataLines);
                    Console.ReadLine();
                    break;
                case 1:
                    Console.Clear();
                    ChangeUserPassword(dataLines);
                    Console.ReadLine();
                    break;
                case 2:
                    break;
            }

        }
        static void AddCommentReport(List<string> dataLines)
        {
            Console.Clear();
            Search_Film.Print_Normal("Thêm báo cáo bình luận ");
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
                    File.WriteAllLines(File_F_Report_Password, dataLines);

                    Console.WriteLine("Báo cáo bình luận thành công.");
                    return;
                }

                Console.WriteLine("Người dùng không tồn tại. Vui lòng thử lại.");
            } while (true);
        }

        static void ChangeUserPassword(List<string> dataLines)
        {
            Console.Clear();
            Search_Film.Print_Normal("Đổi mật khẩu người dùng");
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
            File.WriteAllLines(File_F_Report_Password, dataLines);
            Console.WriteLine("Đổi mật khẩu thành công.");
            Search_Film.Print_Normal($"Tên người dùng: {username}\nMật khẩu mới: {newPassword}");
        }
    }
}
