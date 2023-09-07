using System;
using System.Collections.Generic;

namespace MovieReviewApp
{
    class Film
    {
        public int Rank { get; set; }
        public string Title { get; set; }
        public double Score { get; set; }
        public string Summary { get; set; }
        public List<Review> Reviews { get; set; }

        public Film(int rank, string title, double score, string summary)
        {
            Rank = rank;
            Title = title;
            Score = score;
            Summary = summary;
            Reviews = new List<Review>();
        }
    }

    class Review
    {
        public int Rating { get; set; }
        public string Comment { get; set; }

        public Review(int rating, string comment)
        {
            Rating = rating;
            Comment = comment;
        }
    }

    class Program
    {
        static List<Film> films = new List<Film>();

        static void Main(string[] args)
        {
            InitializeFilms();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("IDEA CONCEPT FOR PROJECT");
                Console.WriteLine("1. Chọn vai trò");
                Console.WriteLine("2. Exit");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("1. Admin");
                        Console.WriteLine("2. User");
                        int roleChoice = int.Parse(Console.ReadLine());

                        if (roleChoice == 1)
                        {
                            // Admin functions
                            AdminFunctions();
                        }
                        else if (roleChoice == 2)
                        {
                            // User functions
                            UserFunctions();
                        }
                        break;

                    case 2:
                        exit = true;
                        break;
                }
            }
        }

        static void InitializeFilms()
        {
            films.Add(new Film(1, "Inception", 8.8, "A thief who steals corporate secrets through the use of dream-sharing technology."));
            films.Add(new Film(2, "The Shawshank Redemption", 9.3, "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency."));
            films.Add(new Film(3, "The Dark Knight", 9.0, "When the menace known as The Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham."));
            // Add more films...
        }

        static void AdminFunctions()
        {
            Console.WriteLine("1. View Top film");
            Console.WriteLine("2. Edit film");
            Console.WriteLine("3. Thêm phim mới");
            int adminChoice = int.Parse(Console.ReadLine());

            if (adminChoice == 1)
            {
                // View top films
              //  ViewTopFilms();
            }
            else if (adminChoice == 2)
            {
                // Edit film
                EditFilm();
            }
            else if (adminChoice == 3)
            {
                // Add new film
                AddNewFilm();
            }
        }

        static void UserFunctions()
        {
            Console.WriteLine("1. View top film");
            Console.WriteLine("2. Exit");
            int userChoice = int.Parse(Console.ReadLine());

            if (userChoice == 1)
            {
                // View all films
                ViewAllFilms();
            }
            // Add more user functions if needed
        }

        static void ViewAllFilms()
        {
            Console.WriteLine("Danh sách các phim:");
            foreach (Film film in films)
            {
                Console.WriteLine($"{film.Rank}. {film.Title}");
            }

            Console.WriteLine("Chọn một bộ phim để xem chi tiết: ");
            int filmChoice = int.Parse(Console.ReadLine());

            Film selectedFilm = films.Find(film => film.Rank == filmChoice);

            if (selectedFilm != null)
            {
                Console.WriteLine($"Tiêu đề: {selectedFilm.Title}");
                Console.WriteLine($"Rank: {selectedFilm.Rank}");
                Console.WriteLine($"Rating: {selectedFilm.Score}");
                Console.WriteLine($"Tóm tắt: {selectedFilm.Summary}");

                Console.WriteLine("1. Add Review");
                Console.WriteLine("2. View All Reviews");
                int detailChoice = int.Parse(Console.ReadLine());

                if (detailChoice == 1)
                {
                    AddReview(selectedFilm);
                }
                else if (detailChoice == 2)
                {
                    ViewAllReviews(selectedFilm);
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy phim!");
            }
        }

        static void AddReview(Film film)
        {
            Console.WriteLine("Nhập đánh giá (từ 1 đến 10): ");
            int rating = int.Parse(Console.ReadLine());

            Console.WriteLine("Nhập bình luận: ");
            string comment = Console.ReadLine();

            Review review = new Review(rating, comment);
            film.Reviews.Add(review);

            film.Score = (film.Score * (film.Reviews.Count - 1) + rating) / film.Reviews.Count; // Cập nhật điểm trung bình

            Console.WriteLine("Đánh giá và bình luận đã được thêm!");
        }

        static void ViewAllReviews(Film film)
        {
            Console.WriteLine($"Tất cả đánh giá và bình luận cho phim {film.Title}:");
            foreach (Review review in film.Reviews)
            {
                Console.WriteLine($"Rating: {review.Rating}");
                Console.WriteLine($"Bình luận: {review.Comment}");
                Console.WriteLine();
            }
            Console.WriteLine($"Điểm trung bình: {film.Score}");
        }

        static void EditFilm()
        {
            Console.WriteLine("Chọn bộ phim cần chỉnh sửa (theo Rank): ");
            int filmChoice = int.Parse(Console.ReadLine());

            Film selectedFilm = films.Find(film => film.Rank == filmChoice);

            if (selectedFilm != null)
            {
                Console.WriteLine($"1. Sửa tiêu đề");
                Console.WriteLine($"2. Sửa điểm");
                Console.WriteLine($"3. Sửa tóm tắt");
                int editChoice = int.Parse(Console.ReadLine());

                switch (editChoice)
                {
                    case 1:
                        Console.WriteLine("Nhập tiêu đề mới: ");
                        selectedFilm.Title = Console.ReadLine();
                        Console.WriteLine("Tiêu đề đã được cập nhật!");
                        break;

                    case 2:
                        Console.WriteLine("Nhập điểm mới: ");
                        double newScore = double.Parse(Console.ReadLine());
                        selectedFilm.Score = newScore;
                        Console.WriteLine("Điểm đã được cập nhật!");
                        break;

                    case 3:
                        Console.WriteLine("Nhập tóm tắt mới: ");
                        selectedFilm.Summary = Console.ReadLine();
                        Console.WriteLine("Tóm tắt đã được cập nhật!");
                        break;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy phim!");
            }
        }

        static void AddNewFilm()
        {
            Console.WriteLine("Nhập tiêu đề phim mới: ");
            string title = Console.ReadLine();

            Console.WriteLine("Nhập điểm phim mới: ");
            double score = double.Parse(Console.ReadLine());

            Console.WriteLine("Nhập tóm tắt phim mới: ");
            string summary = Console.ReadLine();

            int newRank = films.Count + 1; // Tính rank cho phim mới

            Film newFilm = new Film(newRank, title, score, summary);
            films.Add(newFilm);

            Console.WriteLine("Phim mới đã được thêm!");
        }
    }
}
