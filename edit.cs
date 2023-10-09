using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    public class support
    {
        static void EditMovie(int filmid)
        {
            string[] lines = File.ReadAllLines(dataFilePath, Encoding.Unicode);
            bool found = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length >= 8)
                {
                    int existingFilmid;
                    if (int.TryParse(parts[0], out existingFilmid) && existingFilmid == filmid)
                    {
                        found = true;
                        Console.WriteLine("Thông tin hiện tại của bộ phim:");
                        Console.WriteLine(new string('═', 60)); // In đường kẻ ngang
                        Console.WriteLine();

                        Print_Prompts("Film ID:", parts[0]);
                        Print_Prompts("Tên phim:", parts[1]);
                        Print_Prompts("Thể loại:", parts[2]);
                        Print_Prompts("Nhà sản xuất:", parts[3]);
                        Print_Prompts("Số tập:", parts[4]);
                        Print_Prompts("Lượt xem:", parts[5]);
                        Print_Prompts("Doanh thu:", parts[6]);
                        Print_Prompts("Rating:", parts[7]);

                        Console.WriteLine();
                        Console.WriteLine(new string('═', 60)); // In đường kẻ ngang
                        Console.WriteLine("Nhập các thông tin mới:");

                        string tenphim, theloai, nhasanxuat;
                        int sotap, luotxem, doanhthu;
                        double rating;

                        // Nhập thông tin mới với các hàm Print_Prompts
                        tenphim = InputWithPrompt("Tên phim: ");
                        theloai = InputWithPrompt("Thể loại: ");
                        nhasanxuat = InputWithPrompt("Nhà sản xuất: ");
                        sotap = IntInputWithPrompt("Số tập: ");
                        luotxem = IntInputWithPrompt("Lượt xem: ");
                        doanhthu = IntInputWithPrompt("Doanh thu: ");
                        rating = DoubleInputWithPrompt("Rating: ");

                        // Tạo thông tin phim mới
                        string newMovieInfo = $"{filmid},{tenphim},{theloai},{nhasanxuat},{sotap},{luotxem},{doanhthu},{rating}";

                        // Cập nhật thông tin phim trong danh sách
                        lines[i] = newMovieInfo;

                        File.WriteAllLines(dataFilePath, lines, Encoding.Unicode);

                        Console.WriteLine("Dữ liệu đã được cập nhật thành công.");
                        ShowMovieDetails(newMovieInfo); // Hiển thị thông tin chi tiết mới
                        Console.WriteLine("Thông tin hiện tại của bộ phim sau chỉnh sửa!");
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("Filmid không tồn tại trong danh sách.");
            }
        }
        static void AddNewMovie()
        {
            Console.WriteLine("Nhập thông tin phim mới:");
            string tenphim, theloai, nhasanxuat;

            // Sử dụng Print_Prompts để hiển thị và nhập dữ liệu
            tenphim = InputWithPrompt("Tên phim: ");
            theloai = InputWithPrompt("Thể loại: ");
            nhasanxuat = InputWithPrompt("Nhà sản xuất: ");

            int sotap, luotxem, doanhthu;
            double rating;

            sotap = IntInputWithPrompt("Số tập: ");
            luotxem = IntInputWithPrompt("Lượt xem: ");
            doanhthu = IntInputWithPrompt("Doanh thu: ");
            rating = DoubleInputWithPrompt("Rating: ");

            // Tạo thông tin phim mới
            string newMovieInfo = $"{GetNextFilmId()},{tenphim},{theloai},{nhasanxuat},{sotap},{luotxem},{doanhthu},{rating}";

            // Ghi thông tin phim mới vào danh sách
            using (StreamWriter writer = new StreamWriter(dataFilePath, true, Encoding.Unicode))
            {
                writer.WriteLine(newMovieInfo);
            }

            Console.WriteLine("Phim mới đã được thêm thành công.");
            ShowMovieDetails(newMovieInfo);
            Console.WriteLine("Đây là thông tin của bộ phim bạn vừa thêm!");
        }
    }
}
