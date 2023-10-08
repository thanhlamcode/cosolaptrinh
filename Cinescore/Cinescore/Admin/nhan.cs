using System;
using System.Text;

namespace Admin
{
    public static class Program
    {
        public static string dataFilePath = @"C:\Users\MyPC\Desktop\đồ án cslt\duanmoi\danhsachcmtid.txt";

        static void nhan()
        {
            int choice;
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            do
            {
                Console.Clear(); // Xóa màn hình để vẽ lại giao diện
                UIHelper.giaodien();
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            UIHelper.giaodien();
                            CommentManager.DisplayAllComments();
                            break;

                        case 2:
                            Console.Clear();
                            UIHelper.giaodien();
                            CommentManager.DeleteCommentByAccountId();
                            break;

                        case 3:
                            Console.WriteLine("Chương trình kết thúc.");
                            return;

                        default:
                            Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Vui lòng nhập một số nguyên từ 1 đến 3.");
                }

                Console.WriteLine("Nhấn Enter để tiếp tục...");
                Console.ReadLine();
            } while (choice != 3);
        }
    }
}