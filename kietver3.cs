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

        static void addReview()
        {
            Console.Write("Nhập phim id: ");
            int filmid;
            while (!int.TryParse(Console.ReadLine(), out filmid) || filmid <= 0)
            {
                Console.Write("Không hợp lệ. Vui lòng nhập lại: ");
            }

            Console.Write("Nhập account id: ");
            int accountId;
            while (!int.TryParse(Console.ReadLine(), out accountId) || accountId <= 0)
            {
                Console.Write("Không hợp lệ. Vui lòng nhập lại: ");
            }
            
            Console.WriteLine("Thêm review.");

            Console.Write("Bình luận của bạn: ");
            string cmt = Console.ReadLine();

            Console.Write("Số điểm Rating của bạn: ");
            double rating;
            while (!double.TryParse(Console.ReadLine(), out rating) || rating <= 0 || rating >= 10)
            {
                Console.Write("Không hợp lệ. Vui lòng cho điểm lại trên thang từ 0 đến 10: ");
            }

            string review = $"{filmid},{accountId},{cmt},{rating}";

            File.AppendAllText(filePath1, review + Environment.NewLine);

            Console.WriteLine("Bình luận của bạn đã được thêm thành công.");
        }

        static void editReview(int cmtid)
        {
            List<string> reviews = File.ReadAllLines(filePath1).ToList();

            bool found = false;

            for (int i = 0; i < reviews.Count; i++)
            {
                string[] reviewData = reviews[i].Split(',');

                if (reviewData.Length == 4 && int.Parse(reviewData[0]) == cmtid)
                {
                    Console.WriteLine("Bình luận hiện tại:");
                    Console.WriteLine($"Comment: {reviewData[2]}");
                    Console.WriteLine($"Rating: {reviewData[3]}");

                    Console.WriteLine("Bình luận mới.");

                    Console.Write("Bình luận: ");
                    string cmt = Console.ReadLine();

                    Console.Write("Số điểm Rating mới của bạn: ");
                    double rating;
                    while (!double.TryParse(Console.ReadLine(), out rating) || rating <= 0 || rating >= 10)
                    {
                        Console.Write("Không hợp lệ. Vui lòng cho điểm lại trên thang từ 0 đến 10: ");
                    }

                    reviewData[2] = cmt;
                    reviewData[3] = rating.ToString();

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

        static string GetMovieName(string filmId, List<string> movies)
        {
            foreach (string movie in movies)
            {
                string[] movieData = movie.Split(',');

                if (movieData.Length >= 2 && movieData[0] == filmId)
                {
                    return movieData[1]; // Trả về tên phim nếu tìm thấy filmid tương ứng.
                }
            }

            return null; // Trả về null nếu không tìm thấy filmid.
        }

        static void viewAllReview()
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

                    if (reviewData.Length == 4)
                    {
                        string filmId = reviewData[0];
                        string filmName = GetMovieName(filmId, movies);

                        if (filmName != null && filmName.ToLower() == inputMovieName)
                        {
                            string cmt = reviewData[2];
                            double rating = double.Parse(reviewData[3]);
                            Console.WriteLine($"Bình luận: {cmt}, Rating: {rating}");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        static double avgRating()
        {
            string[] lines = File.ReadAllLines(filePath1);
            int totalRatings = 0;
            double sumRatings = 0;

            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                if (values.Length >= 8)
                {
                    double rating;
                    if (double.TryParse(values[7], out rating))
                    {
                        totalRatings++;
                        sumRatings += rating;
                    }
                }
            }

            if (totalRatings > 0)
            {
                return sumRatings / totalRatings;
            }
            else
            {
                return 0;
            }
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
                            addReview();
                            break;
                        case 2:
                            Console.Clear();
                            int cmtid;
                            while (true)
                            {
                                Console.Write("Nhập comment id bạn muốn chỉnh sửa: ");
                                if (int.TryParse(Console.ReadLine(), out cmtid) && cmtid > 0)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Không hợp lệ. Vui lòng nhập lại.");
                                }
                            }
                            editReview(cmtid);
                            break;
                        case 3:
                            Console.Clear();
                            viewAllReview();
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
