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
        static string dataFilePath = @"C:\Users\dtlam\OneDrive\Documents\Code Làm Nhóm cuối Kì\data.txt";

        static void Hienthidongian()
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

        static void ShowMovieDetails(string movieData)
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
                Console.WriteLine(new string('-', 60)); // In đường kẻ ngang
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
                Console.WriteLine(new string('-', 60)); // In đường kẻ ngang
            }
        }

        static void Print_Prompts(string label, string value)
        {
            int t1 = (Console.WindowWidth - 60) / 2;
            int mid_2 = (50 - (label.Length + value.Length)) / 2;

            Console.SetCursorPosition(t1, Console.CursorTop);
            Console.Write("|");
            Console.SetCursorPosition(t1 + 6 + mid_2, Console.CursorTop);
            Console.Write($"{label} {value}");
            Console.SetCursorPosition(t1 + 59, Console.CursorTop);
            Console.WriteLine("|");
        }

    }
}
