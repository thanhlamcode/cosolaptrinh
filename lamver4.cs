using System;
using System.IO;
using System.Text;

class Program
{
    static string dataFilePath = @"C:\Users\dtlam\OneDrive\Documents\Bản dự phòng\Code CSLT\data.txt";

    static void DisplayAllMovieNames()
    {
            string[] lines = File.ReadAllLines(dataFilePath, Encoding.Unicode);
            Console.WriteLine("Danh sách tất cả các tên phim cùng với filmid:");

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
                        Console.WriteLine($"Film ID: {filmid}, Tên phim: {tenphim}");
                    }
                }
            }
    }

    static void check()
    {
        // Hỏi người dùng có muốn xem thông tin chi tiết của một bộ phim không
        while (true)
        {
            Console.Write("Bạn muốn xem thông tin chi tiết của một bộ phim (Y/N)?\n");
            Console.Write("Lựa chọn của bạn là (Y/N): ");
            string choice = Console.ReadLine().ToLower();
            Console.Clear();
            DisplayAllMovieNames();
            if (choice == "y")
            {
                Console.Write("Nhập filmid của bộ phim bạn muốn xem: ");
                if (int.TryParse(Console.ReadLine(), out int selectedFilmId))
                {
                    DisplayMovieDetails(selectedFilmId);
                    break;
                }
                else
                {
                    Console.WriteLine("Film ID không hợp lệ.");
                }
            }
            else if (choice == "n")
            {
                break;
            }
            else
            {
                Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn 'y' hoặc 'n'.");
            }
        }
    }
    static void DisplayMovieDetails(int filmId)
    {
        // Đọc dữ liệu từ tệp và hiển thị thông tin chi tiết của bộ phim có filmId tương ứng
        string[] lines = File.ReadAllLines(dataFilePath, Encoding.Unicode);
        bool found = false;

        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length >= 8)
            {
                if (int.TryParse(parts[0], out int currentFilmId) && currentFilmId == filmId)
                {
                    Console.WriteLine($"Thông tin chi tiết của bộ phim (Film ID: {filmId}):");
                    Console.WriteLine($"Tên phim: {parts[1]}");
                    Console.WriteLine($"Thể loại: {parts[2]}");
                    Console.WriteLine($"Nhà sản xuất: {parts[3]}");
                    Console.WriteLine($"Số tập: {parts[4]}");
                    Console.WriteLine($"Lượt xem: {parts[5]}");
                    Console.WriteLine($"Doanh thu: {parts[6]}");
                    Console.WriteLine($"Rating: {parts[7]}");
                    found = true;
                    break;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine($"Không tìm thấy thông tin cho bộ phim có Film ID: {filmId}");
        }
    }


    static void EditMovie(int filmid)
    {
        string[] lines = File.ReadAllLines(dataFilePath, Encoding.Unicode);
        bool found = false;

        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            if (parts.Length >= 2)
            {
                int existingFilmid;
                if (int.TryParse(parts[0], out existingFilmid) && existingFilmid == filmid)
                {
                    found = true;
                    Console.WriteLine("Thông tin hiện tại của bản ghi:");
                    Console.WriteLine($"Film ID: {existingFilmid}");
                    Console.WriteLine($"Tên phim: {parts[1]}");
                    Console.WriteLine($"Thể loại: {parts[2]}");
                    Console.WriteLine($"Nhà sản xuất: {parts[3]}");
                    Console.WriteLine($"Số tập: {parts[4]}");
                    Console.WriteLine($"Lượt xem: {parts[5]}");
                    Console.WriteLine($"Doanh thu: {parts[6]}");
                    Console.WriteLine($"Rating: {parts[7]}");

                    Console.WriteLine("Nhập các thông tin mới:");

                    string tenphim, theloai, nhasanxuat;

                    do
                    {
                        Console.Write("Tên phim: ");
                        tenphim = Console.ReadLine();
                    } while (string.IsNullOrWhiteSpace(tenphim)); // Lặp lại cho đến khi có dữ liệu

                    do
                    {
                        Console.Write("Thể loại: ");
                        theloai = Console.ReadLine();
                    } while (string.IsNullOrWhiteSpace(theloai)); // Lặp lại cho đến khi có dữ liệu

                    do
                    {
                        Console.Write("Nhà sản xuất: ");
                        nhasanxuat = Console.ReadLine();
                    } while (string.IsNullOrWhiteSpace(nhasanxuat)); // Lặp lại cho đến khi có dữ liệu

                    int sotap;
                    do
                    {
                        Console.Write("Số tập: ");
                    } while (!int.TryParse(Console.ReadLine(), out sotap));

                    int luotxem;
                    do
                    {
                        Console.Write("Lượt xem: ");
                    } while (!int.TryParse(Console.ReadLine(), out luotxem));

                    int doanhthu;
                    do
                    {
                        Console.Write("Doanh thu: ");
                    } while (!int.TryParse(Console.ReadLine(), out doanhthu));

                    double rating;
                    do
                    {
                        Console.Write("Rating: ");
                    } while (!double.TryParse(Console.ReadLine(), out rating));

                    // Tạo thông tin phim mới
                    string newMovieInfo = $"{filmid},{tenphim},{theloai},{nhasanxuat},{sotap},{luotxem},{doanhthu},{rating}";

                    // Cập nhật thông tin phim trong danh sách
                    lines[i] = newMovieInfo;

                    File.WriteAllLines(dataFilePath, lines, Encoding.Unicode);

                    Console.WriteLine("Dữ liệu đã được cập nhật thành công.");
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

        do
        {
            Console.Write("Tên phim: ");
            tenphim = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(tenphim)); // Lặp lại cho đến khi có dữ liệu

        do
        {
            Console.Write("Thể loại: ");
            theloai = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(theloai)); // Lặp lại cho đến khi có dữ liệu

        do
        {
            Console.Write("Nhà sản xuất: ");
            nhasanxuat = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(nhasanxuat)); // Lặp lại cho đến khi có dữ liệu


        int sotap;
        do
        {
            Console.Write("Số tập: ");
        } while (!int.TryParse(Console.ReadLine(), out sotap));

        int luotxem;
        do
        {
            Console.Write("Lượt xem: ");
        } while (!int.TryParse(Console.ReadLine(), out luotxem));

        int doanhthu;
        do
        {
            Console.Write("Doanh thu: ");
        } while (!int.TryParse(Console.ReadLine(), out doanhthu));

        double rating;
        do
        {
            Console.Write("Rating: ");
        } while (!double.TryParse(Console.ReadLine(), out rating));

        // Tạo thông tin phim mới
        string newMovieInfo = $"{GetNextFilmId()},{tenphim},{theloai},{nhasanxuat},{sotap},{luotxem},{doanhthu},{rating}";

        // Ghi thông tin phim mới vào danh sách
        using (StreamWriter writer = new StreamWriter(dataFilePath, true, Encoding.Unicode))
        {
            writer.WriteLine(newMovieInfo);
        }

        Console.WriteLine("Phim mới đã được thêm thành công.");
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

    static void Main()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;

        while (true)
        {
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║    Quản lý danh sách phim    ║");
            Console.WriteLine("╠══════════════════════════════╣");
            Console.WriteLine("║ 1. Hiển thị danh sách phim   ║");
            Console.WriteLine("║ 2. Chỉnh sửa thông tin phim  ║");
            Console.WriteLine("║ 3. Thêm phim mới             ║");
            Console.WriteLine("║ 4. Thoát                     ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.Write("Chọn tùy chọn (1/2/3/4): ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        DisplayAllMovieNames();
                        check();
                        break;
                    case 2:
                        Console.Clear();
                        DisplayAllMovieNames();
                        Console.Write("Nhập filmid của bản ghi bạn muốn chỉnh sửa: ");
                        int filmid = int.Parse(Console.ReadLine());
                        Console.Clear();
                        EditMovie(filmid);
                        break;
                    case 3:
                        Console.Clear();
                        AddNewMovie();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Ứng dụng đã kết thúc.");
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
            }
        }
    }
}