using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Film4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\Asus\\Movie0.txt";

            bool continueSearching = true;
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            while (continueSearching)
            {
                Console.WriteLine("╔════════════════════════════════════════════════╗");
                Console.WriteLine("║             Chọn tùy chọn tìm kiếm             ║");
                Console.WriteLine("╠════════════════════════════════════════════════╣");
                Console.WriteLine("║     1. Tìm phim theo tên                       ║");
                Console.WriteLine("║     2. Tìm phim theo thể loại                  ║");
                Console.WriteLine("║     3. Top phim có rating cao nhất             ║");
                Console.WriteLine("║     0. Kết thúc chương trình                   ║");
                Console.WriteLine("╚════════════════════════════════════════════════╝");
                Console.Write("Nhập lựa chọn của bạn: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Nhập tên phim: ");
                        string searchTermName = Console.ReadLine();
                        SearchMovies(filePath, "tenphim", searchTermName);
                        break;
                    case "2":
                        Console.Write("Nhập thể loại phim: ");
                        string searchTermGenre = Console.ReadLine();
                        SearchMovies(filePath, "theloai", searchTermGenre);
                        break;
                    case "3":
                        SearchMovies(filePath, "rating", "DESC", 10);
                        break;
                    case "4":
                        continueSearching = false;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void SearchMovies(string filePath, string columnName, string searchTerm)
        {
            List<string> searchResults = new List<string>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] movieData = line.Split(';');
                    string tenPhim = movieData[0];
                    string theLoai = movieData[1];
                    string nsx = movieData[2];
                    int soTap = int.Parse(movieData[3]);

                    if (tenPhim.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 && columnName == "tenphim")
                    {
                        searchResults.Add($"{tenPhim} ({soTap} tập)\n- Thể loại phim: {theLoai}\n- NSX: {nsx}");
                    }
                    else if (theLoai.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 && columnName == "theloai")
                    {
                        searchResults.Add($"{tenPhim} ({soTap} tập)\n- Thể loại phim: {theLoai}\n- NSX: {nsx}");
                    }
                }

                if (searchResults.Any())
                {
                    Console.WriteLine("Kết quả tìm kiếm:");
                    foreach (var result in searchResults)
                    {
                        Console.WriteLine(result);
                    }
                }
                else
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

        static void SearchMovies(string filePath, string columnName, string sortOrder, int limit)
        {
            List<string> searchResults = new List<string>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] movieData = line.Split(';');
                    string tenPhim = movieData[0];
                    string theLoai = movieData[1];
                    string nsx = movieData[2];
                    int soTap = int.Parse(movieData[3]);
                    double rating = double.Parse(movieData[6]);

                    searchResults.Add($"{tenPhim} ({soTap} tập)\n- Thể loại phim: {theLoai}\n- NSX: {nsx}\n- Rating: {rating}");
                }

                if (searchResults.Any())
                {
                    Console.WriteLine("Top phim có rating cao nhất:");
                    foreach (var result in searchResults.OrderByDescending(r => double.Parse(r.Substring(r.LastIndexOf("Rating: ") + 8))))
                    {
                        Console.WriteLine(result);
                        limit--;
                        if (limit == 0)
                        {
                            break;
                        }
                    }
                }
                else
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