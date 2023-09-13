using System;
using System.Collections.Generic;
using System.Text;
// xoá review dựa trên userID và reviewID (ID se dc random tu 1-1000)
namespace phanmemdanhgiaphimanhnhomVN5
{
    class Film
    {
        public string Title { get; set; }
        public int Rank { get; set; }
        public double Rating { get; set; }
        public string Summary { get; set; }
        public List<Review> Reviews { get; set; }

        public Film(string title, int rank, double rating, string summary)
        {
            Title = title;
            Rank = rank;
            Rating = rating;
            Summary = summary;
            Reviews = new List<Review>(); // danh sach
        }
    }

    class Review   // ch chắc chắn về việc nên xoá review dựa theo user ID + review ID hay film ID
    {
        public int UserID { get; set; }
        public int ReviewID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }

        public Review(int userID, int reviewID, int rating, string comment, string userName)
        {
            UserID = userID;
            ReviewID = reviewID;
            Rating = rating;
            Comment = comment;
            UserName = userName;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Film> films = new List<Film>();
            Random random = new Random();    // chỗ này tạo đối tượng random đdểdùng tạo soó ngẫu nhiên cho cả ct vì bên dưới sẽ random từ 1-1000 tạo ID

            bool exit = false;  //việc thoát ct ở đây là sai
            while (!exit)
            {
                Console.OutputEncoding = Encoding.Unicode;
                Console.InputEncoding = Encoding.Unicode;
                Console.WriteLine("Chương trình quản lý phim");
                Console.WriteLine("1) Thêm phim mới");
                Console.WriteLine("2) Thêm đánh giá (review) cho phim");
                Console.WriteLine("3) Xem thông tin bộ phim");
                Console.WriteLine("4) Quản lý các review từ người dùng");
                Console.WriteLine("5) Thoát");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Nhập thông tin phim:");
                        Console.Write("Tiêu đề: ");
                        string title = Console.ReadLine();
                        Console.Write("Rank: ");
                        int rank = int.Parse(Console.ReadLine());
                        Console.Write("Rating: ");
                        double rating = double.Parse(Console.ReadLine());
                        Console.Write("Tóm tắt: ");
                        string summary = Console.ReadLine();

                        Film newFilm = new Film(title, rank, rating, summary);
                        films.Add(newFilm);
                        Console.WriteLine("Phim đã được thêm vào danh sách.");
                        break;

                    case 2:
                        Console.WriteLine("Nhập tiêu đề phim muốn đánh giá:");
                        string filmTitle = Console.ReadLine();
                        Film filmToReview = films.Find(f => f.Title == filmTitle);

                        if (filmToReview != null)
                        {
                            Console.Write("Tên người dùng: ");
                            string userName = Console.ReadLine();
                            Console.Write("Đánh giá (từ 1 đến 10): ");
                            int userRating = int.Parse(Console.ReadLine());
                            Console.Write("Bình luận: ");
                            string comment = Console.ReadLine();

                            int randomUserID = random.Next(1, 10001);
                            int randomReviewID = random.Next(1, 1001);
                            Review newReview = new Review(randomUserID, randomReviewID, userRating, comment, userName);
                            filmToReview.Reviews.Add(newReview);

                            //chỗ này sẽ cập nhật dtb của phim
                            double totalRating = 0;
                            foreach (Review review in filmToReview.Reviews)
                            {
                                totalRating += review.Rating;
                            }
                            filmToReview.Rating = totalRating / filmToReview.Reviews.Count;

                            Console.WriteLine("Đánh giá đã được thêm vào phim.");
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy phim.");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Nhập tiêu đề phim muốn xem thông tin chi tiết:");
                        string filmTitleToView = Console.ReadLine();
                        Film filmToView = films.Find(f => f.Title == filmTitleToView);

                        if (filmToView != null)
                        {
                            Console.WriteLine($"Tiêu đề: {filmToView.Title}");
                            Console.WriteLine($"Rank: {filmToView.Rank}");
                            Console.WriteLine($"Rating: {filmToView.Rating}");
                            Console.WriteLine($"Tóm tắt: {filmToView.Summary}");

                            Console.WriteLine("Đánh giá từ người dùng:");
                            foreach (Review review in filmToView.Reviews)
                            {
                                Console.WriteLine($"ReviewID: {review.ReviewID}");
                                Console.WriteLine($"UserID: {review.UserID}");
                                Console.WriteLine($"Tên người dùng: {review.UserName}");
                                Console.WriteLine($"Rating: {review.Rating}");
                                Console.WriteLine($"Bình luận: {review.Comment}");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy phim.");
                        }
                        break;

                    case 4:
                        ViewAllReviews(films);
                        break;

                    case 5:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                        break;
                }
            }
        }

        static void ViewAllReviews(List<Film> films)
        {
            Console.WriteLine("Danh sách tất cả đánh giá:");

            foreach (Film film in films)
            {
                Console.WriteLine($"Tiêu đề: {film.Title}");
                foreach (Review review in film.Reviews)
                {
                    Console.WriteLine($"ReviewID: {review.ReviewID}");
                    Console.WriteLine($"UserID: {review.UserID}");
                    Console.WriteLine($"Tên người dùng: {review.UserName}");
                    Console.WriteLine($"Rating: {review.Rating}");
                    Console.WriteLine($"Bình luận: {review.Comment}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Nhập ReviewID để xóa đánh giá (hoặc nhập 'cancel' để thoát):");
            if (int.TryParse(Console.ReadLine(), out int reviewIDToDelete))
            {
                Console.WriteLine("Nhập UserID để xóa đánh giá (hoặc nhập 'cancel' để thoát):");
                if (int.TryParse(Console.ReadLine(), out int userIDToDelete))
                {
                    bool reviewFound = false;
                    foreach (Film film in films)
                    {
                        Review reviewToRemove = film.Reviews.Find(r => r.ReviewID == reviewIDToDelete && r.UserID == userIDToDelete);
                        if (reviewToRemove != null)
                        {
                            film.Reviews.Remove(reviewToRemove);
                            Console.WriteLine($"Đánh giá có ReviewID {reviewIDToDelete} và UserID {userIDToDelete} đã bị xóa.");
                            reviewFound = true;
                            break;
                        }
                    }

                    if (!reviewFound)
                    {
                        Console.WriteLine($"Không tìm thấy đánh giá có ReviewID {reviewIDToDelete} và UserID {userIDToDelete}.");
                    }
                }
                else if (Console.ReadLine().ToLower() != "cancel")
                {
                    Console.WriteLine("UserID không hợp lệ. Đánh giá không được xóa.");
                }
            }
            else if (Console.ReadLine().ToLower() != "cancel")
            {
                Console.WriteLine("ReviewID không hợp lệ. Đánh giá không được xóa.");
            }
        }
    }
}
