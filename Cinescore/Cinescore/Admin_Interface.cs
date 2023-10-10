using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outside_Interface;
using User;

namespace Admin
{
    internal class Admin_Interface
    {
        static string dataFilePath = @"C:\Users\84967\OneDrive\Máy tính\data.txt";

        public static void ProcessSelectedOption(string filepath, string accountnotify, ref bool gate_end)
        {
            string[] menuOptions = { "Hiển thị danh sách phim", "Chỉnh sửa thông tin phim", "Thêm phim mới",
                "Quản Lý Comment của User", "Lọc user", "Thoát" };
            string prompts = accountnotify + "\n" + "GIAO DIỆN ADMIN";
            int rac = 0;

            Menu menu = new Menu(menuOptions, prompts);
            int selectedIndex = menu.Run(ref rac);

            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    Xem_Phim.DisplayAllMovieNames();
                    break;
                case 1:
                    Console.Clear();
                    Xem_Phim.DisplayAllMovieNames(1);
                    break;
                case 2:
                    Console.Clear();
                    AddNewMovie();
                    Console.ReadLine();
                    break;
                case 3:
                    CommentManager.Comment_Control();
                    Console.ReadLine();
                    break;
                case 4:
                    // lọc rác
                    Xoa_User.Bin(filepath, accountnotify, ref gate_end);  
                    break;
                case 5:
                    gate_end = true;
                    break;
            }
        }
        public static void ShowMovieDetails(string movieData) // Sửa
        {
            // Xử lý và hiển thị thông tin chi tiết của bộ phim
            string[] movieInfo = movieData.Split(',');
            if (movieInfo.Length >= 8)
            {
                string filmId = movieInfo[0];
                string movieName = movieInfo[1];
                string genre = movieInfo[2];
                string productionCompany = movieInfo[3];
                string releaseDate = movieInfo[4];
                string budget = movieInfo[5];
                string boxOffice = movieInfo[6];
                string rating = movieInfo[7];

                Console.WriteLine("Thông tin chi tiết của bộ phim:");
                Console.WriteLine(new string('═', 60)); // In đường kẻ ngang
                Console.WriteLine();

                Print_Prompts("Film ID:", filmId);
                Print_Prompts("Tên phim:", movieName);
                Print_Prompts("Thể loại:", genre);
                Print_Prompts("Công ty sản xuất:", productionCompany);
                Print_Prompts("Ngày phát hành:", releaseDate);
                Print_Prompts("Ngân sách:", budget);
                Print_Prompts("Doanh thu:", boxOffice);
                Print_Prompts("Điểm đánh giá:", rating);

                Console.WriteLine();
                Console.WriteLine(new string('═', 60)); // In đường kẻ ngang
            }
        }

        static void Print_Prompts(string label, string value) // -
        {
            int x = 57;
            int y = 58;
            int leftMargin = 2; // Độ lệch bên trái
            Console.Write("╔");
            Console.Write(new string('═', x)); // 58 là chiều rộng còn lại
            Console.WriteLine("╗");

            Console.Write("║");
            Console.SetCursorPosition(leftMargin, Console.CursorTop);
            Console.Write($"{label} {value}");
            int totalWidth = y; // Tổng chiều rộng
            int rightMargin = totalWidth - leftMargin - label.Length - value.Length;
            Console.SetCursorPosition(leftMargin + label.Length + value.Length + rightMargin, Console.CursorTop);
            Console.WriteLine("║");

            Console.Write("╚");
            Console.Write(new string('═', x)); // 58 là chiều rộng còn lại
            Console.WriteLine("╝");
        }
        
        public static void EditMovie(int F_id, string thongbao, List<string> mangtam)
        {
            string[] lines = File.ReadAllLines(dataFilePath, Encoding.Unicode);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length >= 8)
                {
                    int existingFilmid;
                    if (int.TryParse(parts[0], out existingFilmid) && existingFilmid == F_id)
                    {
                        Search_Film.Print_Normal("Thông tin hiện tại của phim:\n" + thongbao);
                        Console.WriteLine("Nhập các thông tin mới:");

                        string tenphim, theloai, nhasanxuat;
                        int sotap, luotxem, doanhthu;
                        double rating;

                        // Nhập thông tin mới với các hàm Print_Prompts
                        tenphim = Check_Nhapvao.Themdulieu_string("Tên phim: ");
                        theloai = Check_Nhapvao.Themdulieu_string("Thể loại: ");
                        nhasanxuat = Check_Nhapvao.Themdulieu_string("Nhà sản xuất: ");
                        sotap = Check_Nhapvao.Themdulieu_int("Số tập: ");
                        luotxem = Check_Nhapvao.Themdulieu_int("Lượt xem: ");
                        doanhthu = Check_Nhapvao.Themdulieu_int("Doanh thu: ");
                        rating = Check_Nhapvao.DoubleInputWithPrompt("Rating: ");

                        // Tạo thông tin phim mới
                        string newMovieInfo = $"{F_id},{tenphim},{theloai},{nhasanxuat},{sotap},{luotxem},{doanhthu},{rating}";

                        // Cập nhật thông tin phim trong danh sách
                        lines[i] = newMovieInfo;

                        File.WriteAllLines(dataFilePath, lines, Encoding.Unicode);

                        Console.WriteLine("Dữ liệu đã được cập nhật thành công.");
                        ShowMovieDetails(newMovieInfo); // Hiển thị thông tin chi tiết mới
                        Console.WriteLine("Thông tin hiện tại của bộ phim sau chỉnh sửa!");
                    }
                }
            }
        }

        static void AddNewMovie()
        {
            Console.WriteLine("Nhập thông tin phim mới:");
            string tenphim, theloai, nhasanxuat;

            // Sử dụng Print_Prompts để hiển thị và nhập dữ liệu
            tenphim = Check_Nhapvao.Themdulieu_string("Tên phim: "); 
            theloai = Check_Nhapvao.Themdulieu_string("Thể loại: "); 
            nhasanxuat = Check_Nhapvao.Themdulieu_string("Nhà sản xuất: "); 

            int sotap, luotxem, doanhthu;
            double rating;

            sotap = Check_Nhapvao.Themdulieu_int("Số tập: "); 
            luotxem = Check_Nhapvao.Themdulieu_int("Lượt xem: "); 
            doanhthu = Check_Nhapvao.Themdulieu_int("Doanh thu: "); 
            rating = Check_Nhapvao.DoubleInputWithPrompt("Rating: ");

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
        static int GetNextFilmId()
        {
            if (File.Exists(dataFilePath))
            {
                string[] lines = File.ReadAllLines(dataFilePath, Encoding.Unicode);
                if (lines.Length > 0)
                {
                    string lastLine = lines[lines.Length - 1];
                    string[] parts = lastLine.Split(',');
                    if (parts.Length >= 1)
                    {
                        int lastFilmId;
                        if (int.TryParse(parts[0], out lastFilmId))
                        {
                            return lastFilmId + 1;
                        }
                    }
                }
            }

            return 1; // Trả về 1 nếu không tìm thấy dữ liệu hoặc lỗi.
        }
    }
}
