using System;
using System.IO;
using System.Text;

namespace Admin
{
    public static class CommentManager
    {
        public static void DeleteCommentByAccountId()
        {
            Console.Write("Nhập Account ID để hiển thị tất cả các comment và số lần comment: ");
            if (int.TryParse(Console.ReadLine(), out int accountId))
            {
                // Đọc toàn bộ nội dung từ tệp văn bản vào một danh sách
                string[] lines = File.ReadAllLines(Program.dataFilePath);

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
                    // Đếm số lần comment của Account ID
                    int commentCount = CountCommentsByAccountId(accountId);
                    Console.WriteLine($"Số lần comment của Account ID {accountId}: {commentCount}");

                    Console.Write("Nhập Comment ID để xoá (hoặc Enter để bỏ qua): ");
                    if (int.TryParse(Console.ReadLine(), out int commentIdToDelete))
                    {
                        // Xoá comment dựa trên Comment ID và giảm số lần comment của Account ID
                        DeleteCommentByCommentId(commentIdToDelete, accountId);
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

        public static void DeleteCommentByCommentId(int commentId, int accountId)
        {
            // Đọc toàn bộ nội dung từ tệp văn bản vào một danh sách
            string[] lines = File.ReadAllLines(Program.dataFilePath);

            bool found = false;

            // Tạo một StringBuilder để lưu trữ dữ liệu mới
            StringBuilder newData = new StringBuilder();

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 1)
                {
                    int currentCmtId = int.Parse(parts[0]);
                    int currentAccountId = int.Parse(parts[2]);

                    // Nếu Comment ID trùng khớp và Account ID trùng khớp, không thêm dòng này vào newData
                    if (currentCmtId == commentId && currentAccountId == accountId)
                    {
                        found = true;
                        continue;
                    }
                }
                newData.AppendLine(line);
            }

            // Ghi dữ liệu mới vào tệp văn bản
            File.WriteAllText(Program.dataFilePath, newData.ToString());

            if (found)
            {
                // Giảm số lần comment của Account ID
                DecreaseCommentCount(accountId);
                Console.WriteLine("Xoá comment thành công và giảm số lần comment của Account ID.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy comment với Comment ID này hoặc có lỗi xảy ra khi xoá.");
            }
        }

        public static int CountCommentsByAccountId(int accountId)
        {
            // Đọc toàn bộ nội dung từ tệp văn bản vào một danh sách
            string[] lines = File.ReadAllLines(Program.dataFilePath);

            int commentCount = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    int currentAccountId = int.Parse(parts[2]);

                    // Nếu Account ID trùng khớp, tăng biến đếm số lần comment
                    if (currentAccountId == accountId)
                    {
                        commentCount++;
                    }
                }
            }

            return commentCount;
        }

        public static void DecreaseCommentCount(int accountId)
        {
            // Đọc toàn bộ nội dung từ tệp văn bản vào một danh sách
            string[] lines = File.ReadAllLines(Program.dataFilePath);

            // Tạo một StringBuilder để lưu trữ dữ liệu mới
            StringBuilder newData = new StringBuilder();

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    int currentAccountId = int.Parse(parts[2]);

                    // Nếu Account ID trùng khớp, giảm số lần comment
                    if (currentAccountId == accountId)
                    {
                        int commentCount = CountCommentsByAccountId(accountId);
                        commentCount--;

                        // Thêm dòng mới với số lần comment cập nhật
                        newData.AppendLine($"{parts[0]},{parts[1]},{currentAccountId},{commentCount},{parts[4]}");
                    }
                    else
                    {
                        newData.AppendLine(line);
                    }
                }
            }

            // Ghi dữ liệu mới vào tệp văn bản
            File.WriteAllText(Program.dataFilePath, newData.ToString());
        }

        public static void DisplayAllComments()
        {
            // Đọc toàn bộ nội dung từ tệp văn bản
            string[] lines = File.ReadAllLines(Program.dataFilePath);

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
}