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
        static string filePath = @"D:\code\vs - C#\gr_project\new proj\proj.txt";

        static void addReview()
        {
            try
            {
                Console.Write("Nhập phim id: ");
                int filmid = int.Parse(Console.ReadLine());

                Console.Write("Nhập account id: ");
                int accountId = int.Parse(Console.ReadLine());

                Console.WriteLine("Thêm review.");

                Console.Write("Bình luận của bạn: ");
                string cmt = Console.ReadLine();

                double rating;
                while (true)
                {
                    Console.Write("Số điểm Rating của bạn: ");
                    if (double.TryParse(Console.ReadLine(), out rating) && rating >= 0 && rating <= 10)
                        break;
                    else
                        Console.WriteLine("Không hợp lệ. Vui lòng cho điểm lại trên thang từ 0 đến 10");
                }

                string review = $"{filmid},{accountId},{cmt},{rating}";

                File.AppendAllText(filePath, review + Environment.NewLine);

                Console.WriteLine("Bình luận của bạn đã được thêm thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void editReview(int cmtid)
        {
            try
            {
                List<string> reviews = File.ReadAllLines(filePath).ToList();

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

                        reviewData[2] = cmt;

                        reviews[i] = string.Join(",", reviewData);

                        File.WriteAllLines(filePath, reviews);

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
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void viewAllReview()
        {
            try
            {
                List<string> reviews = File.ReadAllLines(filePath).ToList();

                Console.WriteLine("Danh sách tất cả các review:");

                foreach (string review in reviews)
                {
                    string[] reviewData = review.Split(',');

                    if (reviewData.Length == 4)
                    {
                        string cmt = reviewData[2];
                        double rating = double.Parse(reviewData[3]);
                        Console.WriteLine($"cmt: {cmt}, rating: {rating}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static double avgRating()
        {
            string[] lines = File.ReadAllLines(filePath);
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
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("1. Thêm bình luận");
                Console.WriteLine("2. Sửa bình luận");
                Console.WriteLine("3. Xem tất cả bình luận");
                Console.Write("Chọn tùy chọn 1, 2, 3: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            addReview();
                            break;
                        case 2:
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
