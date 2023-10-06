using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace proj_user
{
    internal class Program
    {
        static string filePath1 = @"D:\code\vs - C#\gr_project\new proj\cmt.txt";
        static string filePath2 = @"D:\code\vs - C#\gr_project\new proj\movie.txt";

        static int GetIntegerInput(string message, int min)
        {
            int value;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out value) && value > min)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Không hợp lệ. Vui lòng nhập lại.");
                }
            }
        }

        static double GetDoubleInput(string message, double min, double max)
        {
            double value;
            while (true)
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($"Không hợp lệ. Vui lòng nhập lại trong khoảng từ {min} đến {max}.");
                }
            }
        }

        static string GetStringInput(string message)
        {
            string text;
            while (true)
            {
                Console.Write(message);
                text = Console.ReadLine();
                if (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
                {
                    return text;
                }
                else
                {
                    Console.WriteLine($"Không hợp lệ. Vui lòng nhập lại.");
                }
            }
        }

        static bool checkFilmExistence(List<string> movies, int filmid)
        {
            foreach (string movie in movies)
            {
                string[] movieData = movie.Split(',');
                if (movieData.Length > 0 && movieData[0] == filmid.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        static void AddReview()
        {
            int filmid;
            List<string> movies = File.ReadAllLines(filePath2).ToList();

            // Kiểm tra xem Film ID có tồn tại không
            while (true)
            {
                filmid = GetIntegerInput("Nhập phim id: ", 0);
                if (checkFilmExistence(movies, filmid))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Phim với ID {filmid} không tồn tại. Vui lòng nhập lại.");
                }
            }
            int accountId = GetIntegerInput("Nhập account id: ", 99);
            Console.WriteLine("Thêm review.");

            string cmt = GetStringInput("Bình luận của bạn: ");
            double rating = GetDoubleInput("Số điểm Rating của bạn: ", 0, 10);

            int commentIdCounter = File.ReadAllLines(filePath1).Length + 1;

            string review = $"{commentIdCounter},{filmid},{accountId},{cmt},{rating}";
            File.AppendAllText(filePath1, review + Environment.NewLine);

            Console.WriteLine("Bình luận của bạn đã được thêm thành công.");
            Print_Prompts("Bình luận: ", cmt);
            Print_Prompts("Rating: ", rating.ToString());
        }

        static void EditReview(int cmtid)
        {
            List<string> reviews = File.ReadAllLines(filePath1).ToList();
            bool found = false;

            for (int i = 0; i < reviews.Count; i++)
            {
                string[] reviewData = reviews[i].Split(',');

                if (reviewData.Length == 5 && int.Parse(reviewData[0]) == cmtid)
                {
                    Console.WriteLine("Bình luận hiện tại:");
                    Print_Prompts("Bình luận: ", reviewData[3]);
                    Print_Prompts("Rating: ", reviewData[4]);

                    Console.WriteLine("Nhập các thông tin mới.");
                    string cmt = GetStringInput("Bình luận mới của bạn: ");
                    double rating = GetDoubleInput("Số điểm Rating mới của bạn: ", 0, 10);

                    reviewData[3] = cmt;
                    reviewData[4] = rating.ToString();

                    reviews[i] = string.Join(",", reviewData);
                    File.WriteAllLines(filePath1, reviews);

                    Console.WriteLine("Bình luận đã được cập nhật thành công.");
                    Console.WriteLine("Bình luận sau chỉnh sửa!");
                    Print_Prompts("Bình luận: ", cmt);
                    Print_Prompts("Rating: ", rating.ToString());
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Không tìm thấy bình luận với ID đã cho.");
            }
        }

        static void ViewAllReview()
        {
            try
            {
                List<string> movies = File.ReadAllLines(filePath2).ToList();
                List<string> reviews = File.ReadAllLines(filePath1).ToList();

                Console.WriteLine("Nhập tên phim bạn muốn xem bình luận: ");
                string inputMovieName = Console.ReadLine().ToLower();
                bool movieFound = false;

                foreach (string movie in movies)
                {
                    string[] movieData = movie.Split(',');

                    if (movieData.Length == 8 && movieData[1].ToLower() == inputMovieName)
                    {
                        string movieName = movieData[1];
                        Console.WriteLine($"Dưới đây là các bình luận về phim {movieName}:");
                        movieFound = true;
                    }
                }

                if (!movieFound)
                {
                    Console.WriteLine($"Không tìm thấy phim với tên '{inputMovieName}'.");
                    return;
                }

                foreach (string review in reviews)
                {
                    string[] reviewData = review.Split(',');

                    if (reviewData.Length == 5)
                    {
                        string filmId = reviewData[1];
                        string filmName = GetMovieName(filmId, movies);

                        if (filmName != null && filmName.ToLower() == inputMovieName)
                        {
                            string cmt = reviewData[3];
                            double rating = double.Parse(reviewData[4]);
                            Print_Prompts("Bình luận: ", reviewData[3]);
                            Print_Prompts("Rating: ", reviewData[4]);
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ViewUserComments(int accountId)
        {
            List<string> reviews = File.ReadAllLines(filePath1).ToList();
            List<string> movies = File.ReadAllLines(filePath2).ToList();
            bool found = false;

            foreach (string review in reviews)
            {
                string[] reviewData = review.Split(',');

                if (reviewData.Length == 5 && int.Parse(reviewData[2]) == accountId)
                {
                    int commentId = int.Parse(reviewData[0]);
                    int filmId = int.Parse(reviewData[1]);
                    string filmName = GetMovieName(filmId.ToString(), movies);
                    string cmt = reviewData[3];
                    double rating = double.Parse(reviewData[4]);

                    Print_Prompts("Comment ID: ", commentId.ToString());
                    Print_Prompts("Film ID: ", filmId.ToString());
                    Print_Prompts("Tên Phim: ", filmName);
                    Print_Prompts("Bình luận: ", cmt);
                    Print_Prompts("Rating: ", rating.ToString());
                    Console.WriteLine();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Không tìm thấy bình luận của người dùng với ID đã cho.");
            }
        }

        static string GetMovieName(string filmId, List<string> movies)
        {
            foreach (string movie in movies)
            {
                string[] movieData = movie.Split(',');

                if (movieData.Length >= 2 && movieData[0] == filmId)
                {
                    return movieData[1];
                }
            }

            return null;
        }

        static int GetValidAccountId()
        {
            int accountId = 0;
            List<string> reviews = File.ReadAllLines(filePath1).ToList();
            bool accountExists = false;

            while (!accountExists)
            {
                accountId = GetIntegerInput("Nhập accountId của bạn: ", 99);

                // Kiểm tra xem accountId có tồn tại trong dữ liệu không
                foreach (string review in reviews)
                {
                    string[] reviewData = review.Split(',');

                    if (reviewData.Length == 5 && int.Parse(reviewData[2]) == accountId)
                    {
                        accountExists = true;
                        break;
                    }
                }

                if (!accountExists)
                {
                    Console.WriteLine($"Không tìm thấy accountId {accountId}. Vui lòng nhập lại.");
                }
            }

            return accountId;
        }

        static int GetValidCommentId(int accountId)
        {
            int cmtid = 0;
            List<string> reviews = File.ReadAllLines(filePath1).ToList();
            bool commentExists = false;

            while (!commentExists)
            {
                cmtid = GetIntegerInput("Nhập comment id bạn muốn chỉnh sửa: ", 0);

                // Kiểm tra xem commentId có tồn tại trong dữ liệu không
                foreach (string review in reviews)
                {
                    string[] reviewData = review.Split(',');

                    if (reviewData.Length == 5 && int.Parse(reviewData[0]) == cmtid && int.Parse(reviewData[2]) == accountId)
                    {
                        commentExists = true;
                        break;
                    }
                }

                if (!commentExists)
                {
                    Console.WriteLine($"Không tìm thấy commentId {cmtid}. Vui lòng nhập lại.");
                }
            }

            return cmtid;
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

        static void Menuchonlua()
        {
            string[] menuOptions = { "Thêm bình luận", "Chỉnh sửa bình luận", "Xem các bình luận về một bộ phim", "Thoát" };
            string prompts = "Các tùy chọn bình luận\n" +
                             "Chọn tùy chọn:";

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
                        AddReview();
                        Console.ReadLine();
                        Menuchonlua();
                        break;
                    case 1:
                        // Xử lý tùy chọn 2
                        Console.Clear();
                        int accountId = GetValidAccountId();
                        ViewUserComments(accountId);
                        int cmtid = GetValidCommentId(accountId);
                        EditReview(cmtid);
                        Console.ReadLine();
                        Menuchonlua();
                        break;
                    case 2:
                        // Xử lý tùy chọn 3
                        Console.Clear();
                        ViewAllReview();
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
    }
    class Menu
    {
        private string Prompts;
        private string[] Options;
        private int SelectedIndex;
        private bool Flag;

        public Menu(string[] options, string prompts, bool flag = true)
        {
            Prompts = prompts;
            Flag = flag;
            Options = options;
            SelectedIndex = 0;
        }

        private void Display()
        {
            int num_title = 2;
            if (!Flag)
            {
                Print_title();
                num_title = 0;
            }
            else Print_Prompts();

            string text = null;
            int t1 = (Console.WindowWidth - 60) / 2;
            int t2 = Prompts.Split('\n').Length + num_title;
            Console.SetCursorPosition(t1, t2);
            Console.WriteLine('+' + new string('-', 58) + '+');

            for (int i = 0; i < Options.Length; i++)
            {
                text = Options[i];
                char prefix;
                int mid_2 = (50 - (Options[i].Length + 8)) / 2; // Can thay đổi số để chỉnh sao cho nó đẹp hơn
                Console.SetCursorPosition(t1, t2 + 1 + i);
                Console.Write("|");
                Console.SetCursorPosition(t1 + 6 + mid_2, t2 + 1 + i);
                if (i == SelectedIndex)
                {
                    prefix = '>';
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = ' ';
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.Write($"{new string(prefix, 2)}--{text}--");
                Console.SetCursorPosition(t1 + 59, t2 + 1 + i);
                Console.ResetColor();
                Console.WriteLine("|");
            }

            Console.SetCursorPosition(t1, t2 + Options.Length + 1);
            Console.WriteLine('+' + new string('-', 58) + '+');
        }

        private void Print_Prompts()
        {
            int t1 = (Console.WindowWidth - 60) / 2;

            Console.SetCursorPosition(t1, 0);
            Console.WriteLine('+' + new string('-', 58) + '+');

            for (int i = 0; i < Prompts.Split('\n').Length; i++)
            {
                int mid_2 = (50 - Prompts.Split('\n')[i].Length) / 2; // Dùng để hiển thị text ở giữa của khung
                Console.SetCursorPosition(t1, 1 + i);
                Console.Write("|");
                Console.SetCursorPosition(t1 + 6 + mid_2, 1 + i);
                Console.WriteLine($"{Prompts.Split('\n')[i]}");
                Console.SetCursorPosition(t1 + 59, 1 + i);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(t1, Prompts.Split('\n').Length + 1);
            Console.WriteLine('+' + new string('-', 58) + '+');
        }

        public int Run()
        {
            ConsoleKey Keypress;
            do
            {
                Console.Clear();
                Display();
                var key = Console.ReadKey(intercept: true).Key;
                Keypress = key;

                if (Keypress == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex < 0)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (Keypress == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
            } while (Keypress != ConsoleKey.Enter);

            return SelectedIndex;
        }

        private void Print_title()
        {
            int t1 = (Console.WindowWidth - 87) / 2;
            for (int i = 0; i < Prompts.Split('\n').Length; i++)
            {
                Console.SetCursorPosition(t1, i);
                Console.WriteLine($"{Prompts.Split('\n')[i]}");
            }
        }
    }
}