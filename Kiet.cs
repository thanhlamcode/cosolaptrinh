using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kiet
{
    class Cmt
    {
        static string filePath1 = @"D:\code\vs - C#\gr_project\new proj\cmt.txt";
        static string filePath2 = @"D:\code\vs - C#\gr_project\new proj\Movie0.txt";

        //Hiển thị giữa màn
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

        //Hiển thị các tùy chọn
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

        //Xử lý các tùy chọn
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
                        ViewReview();
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

        //Hàm nhập dữ liệu kiểu số nguyên
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

        //Hàm nhập dữ liệu kiểu số thực
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

        //Hàm nhập dữ liệu kiểu chuỗi
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

        //Hàm tính điểm rating trung bình
        static void AverageRating()
        {
            List<string> reviews = File.ReadAllLines(filePath1).ToList();

            // Khởi tạo một mảng lưu trữ tổng điểm và số lượng đánh giá cho mỗi phim
            double[] totalRatings = new double[1000]; // Giả sử có tối đa 1000 phim
            int[] numberOfRatings = new int[1000]; // Giả sử có tối đa 1000 phim

            foreach (string review in reviews)
            {
                string[] reviewData = review.Split(',');

                if (reviewData.Length == 5)
                {
                    int filmId = int.Parse(reviewData[1]);
                    double rating = double.Parse(reviewData[4]);

                    // Cập nhật tổng điểm và số lượng đánh giá cho phim tương ứng
                    totalRatings[filmId] += rating;
                    numberOfRatings[filmId]++;
                }
            }

            // Tính điểm rating trung bình và cập nhật vào filePath2
            List<string> movies = File.ReadAllLines(filePath2).ToList();

            for (int i = 0; i < movies.Count; i++)
            {
                string[] movieData = movies[i].Split(',');

                if (movieData.Length == 8)
                {
                    int filmId = int.Parse(movieData[0]);

                    // Tính điểm rating trung bình (nếu có đánh giá)
                    if (numberOfRatings[filmId] > 0)
                    {
                        double averageRating = totalRatings[filmId] / numberOfRatings[filmId];
                        movieData[7] = averageRating.ToString();

                        // Cập nhật dữ liệu vào filePath2
                        movies[i] = string.Join(",", movieData);
                    }
                }
            }

            // Ghi dữ liệu đã được cập nhật vào filePath2
            File.WriteAllLines(filePath2, movies);
        }

        //Hàm kiểm tra xem phim id có tồn tại trong bảng dữ liệu không
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

        //Hàm thêm review mới của người dùng nhập vào
        static void AddReview()
        {
            try
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

                //Cập nhật cmt id (nếu trong TH không có sẽ bắt đầu = 1)
                int commentIdCounter = File.ReadAllLines(filePath1).Length + 1;

                string review = $"{commentIdCounter},{filmid},{accountId},{cmt},{rating}";

                //ghi dữ liệu vừa thêm vào cuối tệp tin filePath1.
                File.AppendAllText(filePath1, review + Environment.NewLine);

                Console.WriteLine("Bình luận của bạn đã được thêm thành công.");
                Print_Prompts("Bình luận: ", cmt);
                Print_Prompts("Rating: ", rating.ToString());

                AverageRating();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Hỗ trợ cho hàm EditReview() để người dùng có thể xem các comment của mình từng comment
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

        //Sửa review
        static void EditReview(int cmtid)
        {
            try
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

                        //Tính lại điểm rating trung bình sau khi sửa
                        AverageRating();
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Không tìm thấy bình luận với ID đã cho.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Xem review về 1 bộ phim nào đó mà người dùng nhập vào
        static void ViewReview()
        {
            try
            {
                List<string> movies = File.ReadAllLines(filePath2).ToList();
                List<string> reviews = File.ReadAllLines(filePath1).ToList();

                Console.WriteLine("Nhập tên phim bạn muốn xem bình luận: ");
                string inputMovieName = Console.ReadLine().ToLower();
                byte check = 0;
                foreach (string movie in movies)
                {
                    string[] movieData = movie.Split(',');

                    if (movieData.Length == 8 && movieData[1].ToLower() == inputMovieName)
                    {
                        string movieName = movieData[1];
                        Console.WriteLine($"Dưới đây là các bình luận về phim {movieName}:");
                        check = 1;
                    }
                }

                if (check != 1)
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

        //Lấy tên phim thích hợp với phim id được gán trong bảng dữ liệu
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

        //Kiểm tra 1 accountId có tồn tại không
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

        //Kiểm tra xem commentId có tồn tại không
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
    }

}
