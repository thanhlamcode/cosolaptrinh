using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuan
{
    class Search
    {
        static string filePath = @"D:\code\vs - C#\gr_project\new proj\Movie0.txt";

        static void Menuchonlua()
        {
            string[] menuOptions = { "Tìm phim theo tên", "Tìm phim theo thể loại", "Top phim có rating cao nhất", "Thoát chương trình" };
            string prompts = "Chọn tùy chọn tìm kiếm";

            Menu menu = new Menu(menuOptions, prompts);
            int selectedIndex = menu.Run();

            ProcessSelectedOption(selectedIndex);

            Console.WriteLine("Nhấn phím bất kỳ để thoát.");
            Console.ReadKey();
        }

        static void ProcessSelectedOption(int selectedIndex)
        {
            bool check = true;

            do
            {
                switch (selectedIndex)
                {
                    case 0:
                        // Xử lý tùy chọn 1
                        Console.Clear();
                        Console.Write("Nhập tên phim: ");
                        string searchTermName = Console.ReadLine();
                        SearchMovies(filePath, "tenphim", searchTermName);
                        Console.ReadLine();
                        Menuchonlua();
                        break;
                    case 1:
                        // Xử lý tùy chọn 2
                        Console.Clear();
                        Console.Write("Nhập thể loại phim: ");
                        string searchTermGenre = Console.ReadLine();
                        SearchMovies(filePath, "theloai", searchTermGenre);
                        Console.ReadLine();
                        Menuchonlua();
                        break;
                    case 2:
                        Console.Clear();
                        SearchMovies(filePath, "rating", "DESC", 10);
                        Console.ReadLine();
                        Menuchonlua();
                        break;
                    case 3:
                        // Xử lý tùy chọn 4
                        Console.WriteLine("Bạn đã chọn Thoát.");
                        check = false;
                        Console.WriteLine("Chương trình đang kết thúc...................");
                        break;
                }
            }
            while (check);
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Menuchonlua();
        }

        static void Print_Prompts(string label, string value)
        {
            int t1 = (Console.WindowWidth - 60) / 2;
            int mid_2 = (50 - (label.Length + value.Length)) / 2;

            Console.SetCursorPosition(t1, Console.CursorTop);
            Console.Write("║");
            Console.SetCursorPosition(t1 + 6 + mid_2, Console.CursorTop);
            Console.Write($"{label} {value}");
            Console.SetCursorPosition(t1 + 59, Console.CursorTop);
            Console.WriteLine("║");
        }

        static void SearchMovies(string filePath, string columnName, string searchTerm)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                int dem = 0;

                foreach (string line in lines)
                {
                    string[] movieData = line.Split(',');
                    string tenPhim = movieData[1];
                    string theLoai = movieData[2];
                    string nsx = movieData[3];
                    int soTap = int.Parse(movieData[4]);

                    if (tenPhim.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 && columnName == "tenphim")
                    {
                        dem = 1;
                        Print_Prompts("Tên phim: ", tenPhim);
                        Print_Prompts(soTap + " tập", "");
                        Print_Prompts("Thể loại: ", theLoai);
                        Print_Prompts("Nsx: ", nsx);
                        Console.WriteLine();
                    }
                    else if (theLoai.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 && columnName == "theloai")
                    {
                        dem = 1;
                        Print_Prompts("Tên phim: ", tenPhim);
                        Print_Prompts(soTap + " tập", "");
                        Print_Prompts("Thể loại: ", theLoai);
                        Print_Prompts("Nsx: ", nsx);
                        Console.WriteLine();
                    }
                }

                if (dem != 1)
                {
                    Print_Prompts("Không tìm thấy phim phù hợp.", "");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File không tồn tại.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        static void SearchMovies(string filePath, string columnName, string sortOrder, int limit)
        {
            List<string> searchResults = new List<string>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                bool check = false;

                Console.WriteLine("Top phim có rating cao nhất:");
                foreach (string line in lines)
                {
                    string[] movieData = line.Split(',');
                    string tenPhim = movieData[1];
                    string theLoai = movieData[2];
                    string nsx = movieData[3];
                    int soTap = int.Parse(movieData[4]);
                    double rating = double.Parse(movieData[7]);

                    Print_Prompts("Tên phim: ", tenPhim);
                    Print_Prompts(soTap + " tập", "");
                    Print_Prompts("Thể loại: ", theLoai);
                    Print_Prompts("Nsx: ", nsx);
                    Print_Prompts("Rating: ", rating.ToString());
                    Console.WriteLine();
                    check = true;
                }

                if (!check)
                {
                    Console.WriteLine("Không tìm thấy phim phù hợp.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File không tồn tại.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
            }
        }
    }
}
