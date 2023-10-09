using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outside_Interface;


namespace Admin
{
    internal class Admin_Interface
    {
        static string dataFilePath = @"C:\Users\84967\OneDrive\Máy tính\data.txt";

        static void Hienthidongian() // Sửa
        {
            string[] lines = File.ReadAllLines(dataFilePath, Encoding.Unicode);
            Console.Clear(); // Xóa màn hình để hiển thị danh sách dễ đọc hơn
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                          Danh sách tất cả các tên phim cùng với filmid:                ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════════════╣");

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(',');
                if (parts.Length >= 2)
                {
                    int filmid;
                    if (int.TryParse(parts[0], out filmid))
                    {
                        string tenphim = parts[1];
                        Console.WriteLine($"║  Film ID: {filmid,-10}║ Tên phim: {tenphim,-55}║");
                    }
                }
            }

            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════╝");
        }

        public static void ProcessSelectedOption(string filepath, string accountnotify, ref bool gate_end)
        {
            string[] menuOptions = { "Hiển thị danh sách phim", "Chỉnh sửa thông tin phim", "Thêm phim mới",
                "Quản Lý Comment của User", "Lọc user", "Thoát" };
            string prompts = accountnotify + "\n" + "GIAO DIỆN ADMIN";

            Menu menu = new Menu(menuOptions, prompts);
            int selectedIndex = menu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    DisplayAllMovieNames();
                    Console.ReadLine();
                    break;
                case 1:
                    Hienthidongian();

                    int filmid;
                    bool isNumeric = false;

                    do
                    {
                        Console.Write("Nhập filmid của bản ghi bạn muốn chỉnh sửa: ");
                        string input = Console.ReadLine();

                        isNumeric = int.TryParse(input, out filmid);

                        if (!isNumeric)
                        {
                            Console.WriteLine("Vui lòng nhập một số nguyên hợp lệ.");
                        }
                    } while (!isNumeric);

                    EditMovie(filmid);
                    Console.ReadLine();
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

        static void DisplayAllMovieNames()
        {
            string[] lines = File.ReadAllLines(dataFilePath, Encoding.Unicode);
            List<string> movieOptions = new List<string>(); 


            Console.WriteLine("Danh sách tất cả các tên phim cùng với film ID:\n");

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] movieData = line.Split(',');

                // Kiểm tra xem có đủ thông tin để hiển thị film ID và tên phim không
                if (movieData.Length >= 2)
                {
                    string filmId = movieData[0];
                    string movieName = movieData[1];

                    movieOptions.Add($"{filmId}: {movieName}");
                }
            }

            string[] menuOptions = movieOptions.ToArray();
            string prompts = "Danh sách tất cả các phim\n" + "Enter để xem thông tin chi tiết\n";

            Menu menu = new Menu(menuOptions, prompts);

            int selectedIndex = menu.Run();

            // Xử lý khi người dùng đã chọn một bộ phim
            if (selectedIndex >= 0 && selectedIndex < movieOptions.Count)
            {
                ShowMovieDetails(lines[selectedIndex]);
            }
        }

        static void ShowMovieDetails(string movieData) // Sửa
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

        static void Print_Prompts(string label, string value)
        {
            int t1 = (Console.WindowWidth - 60) / 2;

            Console.SetCursorPosition(t1, Console.CursorTop);
            Console.Write("║");
            Console.SetCursorPosition(t1 + 6, Console.CursorTop);
            Console.Write($"{label} {value}");
            Console.SetCursorPosition(t1 + 59, Console.CursorTop);
            Console.WriteLine("║");
        }
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
                        tenphim = Check_Nhapvao.Themdulieu_string("Tên phim: "); 
                        theloai = Check_Nhapvao.Themdulieu_string("Thể loại: "); 
                        nhasanxuat = Check_Nhapvao.Themdulieu_string("Nhà sản xuất: "); 
                        sotap = Check_Nhapvao.Themdulieu_int("Số tập: "); 
                        luotxem = Check_Nhapvao.Themdulieu_int("Lượt xem: "); 
                        doanhthu = Check_Nhapvao.Themdulieu_int("Doanh thu: "); 
                        rating = Check_Nhapvao.DoubleInputWithPrompt("Rating: ");

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
