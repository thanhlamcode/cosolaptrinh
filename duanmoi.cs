using System;
using System.IO;
using System.Text;

class Program
{
    static string dataFilePath = @"C:\Users\MyPC\Desktop\đồ án cslt\ConsoleApp9\danhsachcmtid.txt";

    static void DeleteCommentByCmtId()
    {
        Console.Write("Nhập cmtid để xoá comment: ");
        if (int.TryParse(Console.ReadLine(), out int cmtId))
        {
            // Đọc toàn bộ nội dung từ tệp văn bản vào một danh sách
            string[] lines = File.ReadAllLines(dataFilePath);

            bool found = false;

            // Tạo một StringBuilder để lưu trữ dữ liệu mới
            StringBuilder newData = new StringBuilder();

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                int currentCmtId = int.Parse(parts[0]);

                // Nếu cmtid trùng khớp, không thêm dòng này vào newData
                if (currentCmtId == cmtId)
                {
                    found = true;
                    continue;
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
                Console.WriteLine("Không tìm thấy comment với cmtid này hoặc có lỗi xảy ra khi xoá.");
            }
        }
        else
        {
            Console.WriteLine("Vui lòng nhập một số nguyên cho cmtid.");
        }
    }

    static void DisplayAllComments()
    {
        // Đọc toàn bộ nội dung từ tệp văn bản
        string[] lines = File.ReadAllLines(dataFilePath);

        Console.WriteLine("Danh sách tất cả các comments (cmtid, filmid, cmt):");

        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length >= 3)
            {
                int cmtid = int.Parse(parts[0]);
                int filmid = int.Parse(parts[1]);
                string cmt = parts[3];

                Console.WriteLine($"CMT ID: {cmtid}, Film ID: {filmid}, Comment: {cmt}");
            }
        }
    }

    static void Main()
    {
        int choice;
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;
        do
        {
            Console.WriteLine("----- Menu -----");
            Console.WriteLine("1. Nhập đánh giá mới");
            Console.WriteLine("2. Hiển thị danh sách cmtid");
            Console.WriteLine("3. Xoá comment theo cmtid");
            Console.WriteLine("4. Thoát");
            Console.Write("Chọn một tùy chọn (1-4): ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        // Nhập đánh giá mới và ghi vào tệp văn bản
                        Console.Write("Nhập đánh giá mới (cmtid,filmid,accountId,cmt,rating): ");
                        string newComment = Console.ReadLine();
                        File.AppendAllText(dataFilePath, newComment + Environment.NewLine);
                        Console.WriteLine("Đánh giá đã được thêm thành công.");
                        break;

                    case 2:
                        // Hiển thị danh sách cmtid
                        DisplayAllComments();
                        break;

                    case 3:
                        // Xoá comment theo cmtid
                        DeleteCommentByCmtId();
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
}