using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding= Encoding.UTF8;
            // Dữ liệu bình luận và số lần báo cáo tương ứng
            Dictionary<string, int> commentReports = new Dictionary<string, int>();

            while (true)
            {
                Console.WriteLine("==== Chương trình quản lý báo cáo bình luận ====");
                Console.WriteLine("1. Thêm báo cáo bình luận");
                Console.WriteLine("2. Kiểm tra số lần báo cáo của bình luận");
                Console.WriteLine("3. Đổi mật khẩu quản trị viên");
                Console.WriteLine("0. Thoát chương trình");
                Console.WriteLine("==============================================");

                Console.Write("Vui lòng chọn một tùy chọn: ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AddCommentReport(commentReports);
                        break;
                    case 2:
                        CheckCommentReports(commentReports);
                        break;
                    case 3:
                        ChangeAdminPassword();
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

        static void AddCommentReport(Dictionary<string, int> commentReports)
        {
            Console.WriteLine("==== Thêm báo cáo bình luận ====");
            Console.Write("Nhập ID bình luận: ");
            string commentId = Console.ReadLine();

            if (commentReports.ContainsKey(commentId))
            {
                commentReports[commentId]++;
            }
            else
            {
                commentReports.Add(commentId, 1);
            }

            if (commentReports[commentId] >= 5)
            {
                Console.WriteLine("Bình luận đã bị báo cáo 5 lần. Đã gửi thông báo đến quản trị viên.");
                // Gửi thông báo đến quản trị viên để xử lý việc khóa tài khoản hoặc thực hiện các hành động khác
            }
            else
            {
                Console.WriteLine("Báo cáo bình luận thành công.");
            }
        }

        static void CheckCommentReports(Dictionary<string, int> commentReports)
        {
            Console.WriteLine("==== Kiểm tra số lần báo cáo của bình luận ====");
            Console.Write("Nhập ID bình luận: ");
            string commentId = Console.ReadLine();

            if (commentReports.ContainsKey(commentId))
            {
                int reportCount = commentReports[commentId];
                Console.WriteLine($"Bình luận có ID '{commentId}' đã được báo cáo {reportCount} lần.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy bình luận có ID '{commentId}'.");
            }
        }

        static void ChangeAdminPassword()
        {
            Console.WriteLine("==== Đổi mật khẩu quản trị viên ====");
            Console.Write("Nhập mật khẩu hiện tại: ");
            string currentPassword = Console.ReadLine();

            // Kiểm tra mật khẩu hiện tại có đúng không
            if (currentPassword == "admin123")
            {
                Console.Write("Nhập mật khẩu mới: ");
                string newPassword = Console.ReadLine();

                // Lưu mật khẩu mới vào cơ sở dữ liệu hoặc nơi lưu trữ tương ứng
                Console.WriteLine("Mật khẩu đã được thay đổi thành công.");
            }
            else
            {
                Console.WriteLine("Mật khẩu hiện tại không đúng. Không thể thay đổi mật khẩu.");
            }
        }
    }
}
