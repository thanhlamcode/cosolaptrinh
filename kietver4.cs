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
            foreach(string movie in movies)
            {
                string[] movieData = movie.Split(',');
                if(movieData.Length > 0 && movieData[0] == filmid.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        static int commentIdCounter = 1;

        static void AddReview(int currentCmtid)
        {
            int filmid;
            List<string> movies = File.ReadAllLines(filePath2).ToList();

            // Kiểm tra xem Film ID có tồn tại không
            while (true)
            {
                filmid = GetIntegerInput("Nhập phim id: ",0);
                if(checkFilmExistence(movies, filmid))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Phim với ID {filmid} không tồn tại. Vui lòng nhập lại.");
                }
            }
            int accountId = GetIntegerInput("Nhập account id: ",99);
            Console.WriteLine("Thêm review.");
            
            string cmt = GetStringInput("Bình luận của bạn: ");
            double rating = GetDoubleInput("Số điểm Rating của bạn: ",0,10);

            string review = $"{commentIdCounter},{filmid},{accountId},{cmt},{rating}";
            File.AppendAllText(filePath1, review + Environment.NewLine);

            Console.WriteLine("Bình luận của bạn đã được thêm thành công.");
            commentIdCounter++; // Tăng giá trị của commentIdCounter cho lần comment tiếp theo
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
                    Console.WriteLine($"Bình luận: {reviewData[3]}");
                    Console.WriteLine($"Rating: {reviewData[4]}");

                    Console.WriteLine("Bình luận mới.");

                    string cmt = GetStringInput("Bình luận mới của bạn: ");
                    double rating = GetDoubleInput("Số điểm Rating mới của bạn: ",0,10);

                    reviewData[3] = cmt;
                    reviewData[4] = rating.ToString();

                    reviews[i] = string.Join(",", reviewData);
                    File.WriteAllLines(filePath1, reviews);

                    Console.WriteLine("Bình luận đã được cập nhật thành công.");
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
                            Console.WriteLine($"Bình luận: {cmt}, Rating: {rating}");
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
                    string commentText = reviewData[3];
                    double rating = double.Parse(reviewData[4]);

                    Console.WriteLine($"Comment ID: {commentId}, Film ID: {filmId}, Tên Phim: {filmName}, Bình luận: {commentText}, Rating: {rating}");
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

        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            while (true)
            {
                
                Console.WriteLine("1. Thêm bình luận.");
                Console.WriteLine("2. Sửa bình luận.");
                Console.WriteLine("3. Xem các bình luận về một bộ phim.");
                Console.Write("Chọn tùy chọn 1, 2, 3: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            int currentCmtid = 1;
                            AddReview(currentCmtid);
                            break;
                        case 2:
                            Console.Clear();
                            int accountId = GetValidAccountId();
                            ViewUserComments(accountId);
                            int cmtid = GetIntegerInput("Nhập comment id bạn muốn chỉnh sửa: ",0);
                            EditReview(cmtid);
                            break;
                        case 3:
                            Console.Clear();
                            ViewAllReview();
                            break;
                        default:
                            Console.WriteLine("Không hợp lệ. Vui lòng chọn lại");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Không hợp lệ. Vui lòng chọn lại");
                }
            }
        }
    }
}