using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outside_Interface;

namespace User
{
    internal class For_User_Comment
    {
        static string filePath1 = @"C:\Users\84967\OneDrive\Máy tính\cmt.txt";
        static string filePath2 = @"C:\Users\84967\OneDrive\Máy tính\Movie0.txt";

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

        //Hàm thêm review mới của người dùng nhập vào
        public static void AddReview(string filmid, int accountId)
        {
            try
            {
                List<string> movies = File.ReadAllLines(filePath2).ToList();

                string cmt = Check_Nhapvao.Themdulieu_string("Bình luận của bạn: ");
                double rating = Check_Nhapvao.DoubleInputWithPrompt("Số điểm Rating của bạn: ", 0, 10, true);

                //Cập nhật cmt id (nếu trong TH không có sẽ bắt đầu = 1)
                int commentIdCounter = File.ReadAllLines(filePath1).Length + 1;

                string review = $"{commentIdCounter},{filmid},{accountId},{cmt},{rating}";

                //ghi dữ liệu vừa thêm vào cuối tệp tin filePath1.
                File.AppendAllText(filePath1, review + Environment.NewLine);

                Console.WriteLine("Bình luận của bạn đã được thêm thành công.");
                Search_Film.Print_Normal($"Bình luận: {cmt}\nRating: {rating.ToString()}");

                AverageRating();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Hỗ trợ cho hàm EditReview() để người dùng có thể xem các comment của mình từng comment
        public static void ViewUserComments(int accountId)
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
                    string text = $"Comment ID: {commentId.ToString()}\nFilm ID: {filmId.ToString()}\n" +
                        $"Tên Phim: {filmName}\nBình luận: {cmt}\nRating: {rating.ToString()}";

                    Search_Film.Print_Normal(text);
                    Console.WriteLine();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Bạn chưa có bình luận nào. Hãy tìm kiếm và thực hiện bình luận.");
            }
        }

        //Sửa review
        public static void EditReview(int cmtid)
        {
            try
            {
                List<string> reviews = File.ReadAllLines(filePath1).ToList();

                for (int i = 0; i < reviews.Count; i++)
                {
                    string[] reviewData = reviews[i].Split(',');

                    if (reviewData.Length == 5 && int.Parse(reviewData[0]) == cmtid)
                    {
                        Console.WriteLine("Bình luận hiện tại:");
                        Search_Film.Print_Normal($"Bình luận: {reviewData[3]}\nRating: {reviewData[4]}");

                        Console.WriteLine("Nhập các thông tin mới.");
                        string cmt = Check_Nhapvao.Themdulieu_string("Bình luận mới của bạn: ");
                        double rating = Check_Nhapvao.DoubleInputWithPrompt("Số điểm Rating mới của bạn: ", 0, 10, true);

                        reviewData[3] = cmt;
                        reviewData[4] = rating.ToString();

                        reviews[i] = string.Join(",", reviewData);
                        File.WriteAllLines(filePath1, reviews);

                        Console.WriteLine("Bình luận đã được cập nhật thành công.");
                        Console.WriteLine("Bình luận sau chỉnh sửa!");
                        Search_Film.Print_Normal($"Bình luận: {cmt}\nRating: {rating.ToString()}");

                        //Tính lại điểm rating trung bình sau khi sửa
                        AverageRating();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Xem review về 1 bộ phim nào đó mà người dùng nhập vào
        public static void ViewReview(string filmid)
        {
            try
            {
                List<string> movies = File.ReadAllLines(filePath2).ToList();
                List<string> reviews = File.ReadAllLines(filePath1).ToList();

                foreach (string movie in movies)
                {
                    string[] movieData = movie.Split(',');

                    if (movieData.Length == 8 && movieData[0] == filmid)
                    {
                        string movieName = movieData[1];
                        Console.WriteLine($"Dưới đây là các bình luận về phim {movieName}:");
                    }
                }

                foreach (string review in reviews)
                {
                    string[] reviewData = review.Split(',');

                    if (reviewData.Length == 5)
                    {
                        string film_Id = reviewData[0];

                        if (film_Id != null && film_Id == filmid)
                        {
                            string cmt = reviewData[3];
                            double rating = double.Parse(reviewData[4]);
                            Search_Film.Print_Normal($"Bình luận: {reviewData[3]}\nRating: {reviewData[4]}");
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

        //Kiểm tra xem commentId có tồn tại không
        public static int GetValidCommentId(int accountId)
        {
            int cmtid = 0;
            List<string> reviews = File.ReadAllLines(filePath1).ToList();
            bool commentExists = false;

            while (!commentExists)
            {
                cmtid = Check_Nhapvao.Themdulieu_int("Nhập comment id bạn muốn chỉnh sửa: ");

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
