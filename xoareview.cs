using System;
using System.IO;
using System.Text;

class Program
{
    static string dataFilePath = @"C:\Users\MyPC\Desktop\đồ án cslt\duanmoi\danhsachcmtid.txt";

    static void Main()
    {
        int choice;
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;
        do
        {
            Console.Clear(); // Xóa màn hình để vẽ lại giao diện
            DrawMenu();
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        DrawMenu();
                        Console.Write("Nhập đánh giá mới (cmtid,filmid,accountId,cmt,rating): ");
                        string newComment = Console.ReadLine();
                        File.AppendAllText(dataFilePath, newComment + Environment.NewLine);
                        Console.WriteLine("Đánh giá đã được thêm thành công.");
                        break;

                    case 2:
                        Console.Clear();
                        DrawMenu();
                        DisplayAllComments();
                        break;

                    case 3:
                        Console.Clear();
                        DrawMenu();
                        DeleteCommentByAccountId();
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

            Console.WriteLine("Nhấn Enter để tiếp tục...");
            Console.ReadLine();
        } while (choice != 4);
    }

    static void DrawMenu()
    {
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║         Ứng dụng Quản lý Đánh giá  ║");
        Console.WriteLine("╠════════════════════════════════════╣");
        Console.WriteLine("║ 1. Nhập đánh giá mới               ║");
        Console.WriteLine("║ 2. Hiển thị danh sách cmtid        ║");
        Console.WriteLine("║ 3. Xoá comment theo Account ID     ║");
        Console.WriteLine("║ 4. Thoát                           ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.Write("Chọn một tùy chọn (1-4): ");
    }

    static void DeleteCommentByAccountId()
    {
        Console.Write("Nhập Account ID để hiển thị tất cả các comment: ");
        if (int.TryParse(Console.ReadLine(), out int accountId))
        {
            // Đọc toàn bộ nội dung từ tệp văn bản vào một danh sách
            string[] lines = File.ReadAllLines(dataFilePath);

            bool found = false;

            Console.WriteLine($"Danh sách tất cả các comments của Account ID {accountId}:");

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    int currentAccountId = int.Parse(parts[2]);

                    // Nếu Account ID trùng khớp, hiển thị thông tin comment, Comment ID và Film ID
                    if (currentAccountId == accountId)
                    {
                        int cmtId = int.Parse(parts[0]);
                        int filmId = int.Parse(parts[1]);
                        string comment = parts[3];

                        Console.WriteLine($"CMT ID: {cmtId}, Film ID: {filmId}, Comment: {comment}");
                        found = true;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("Không tìm thấy comment với Account ID này.");
            }
            else
            {
                Console.Write("Nhập Comment ID để xoá (hoặc Enter để bỏ qua): ");
                if (int.TryParse(Console.ReadLine(), out int commentIdToDelete))
                {
                    // Xoá comment dựa trên Comment ID
                    DeleteCommentByCommentId(commentIdToDelete);
                }
                else
                {
                    Console.WriteLine("Không nhập hoặc nhập một giá trị không hợp lệ cho Comment ID.");
                }
            }
        }
        else
        {
            Console.WriteLine("Vui lòng nhập một số nguyên cho Account ID.");
        }
    }

    static void DeleteCommentByCommentId(int commentId)
    {
        // Đọc toàn bộ nội dung từ tệp văn bản vào một danh sách
        string[] lines = File.ReadAllLines(dataFilePath);

        bool found = false;

        // Tạo một StringBuilder để lưu trữ dữ liệu mới
        StringBuilder newData = new StringBuilder();

        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length >= 1)
            {
                int currentCmtId = int.Parse(parts[0]);

                // Nếu Comment ID trùng khớp, không thêm dòng này vào newData
                if (currentCmtId == commentId)
                {
                    found = true;
                    continue;
                }
            }
            newData.AppendLine(line);
        }

        // Ghi dữ liệu mới vào tệp văn bản
        File.WriteAllText(dataFilePath, newData.ToString());

        if (found)
        {
            Console.WriteLine("Xoá comment thành công.");
        }
        else
        {
            Console.WriteLine("Không tìm thấy comment với Comment ID này hoặc có lỗi xảy ra khi xoá.");
        }
    }

    static void DisplayAllComments()
    {
        // Đọc toàn bộ nội dung từ tệp văn bản
        string[] lines = File.ReadAllLines(dataFilePath);

        Console.WriteLine("Danh sách tất cả các comments (cmtid, filmid, accountId, cmt):");

        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length >= 4)
            {
                int cmtid = int.Parse(parts[0]);
                int filmid = int.Parse(parts[1]);
                int accountId = int.Parse(parts[2]);
                string cmt = parts[3];

                Console.WriteLine($"CMT ID: {cmtid}, Film ID: {filmid}, Account ID: {accountId}, Comment: {cmt}");
            }
        }
    }
}
