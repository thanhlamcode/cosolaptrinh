using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outside_Interface;

namespace User
{
    internal class Search_Film
    {
        static string filePath = @"C:\Users\84967\OneDrive\Máy tính\Movie0.txt"; 

        public static void ProcessSelectedOption(ref bool gate_end, int ac_Id)
        {
            string[] menuOptions = { "Tìm phim theo tên", "Tìm phim theo thể loại", 
                "Top phim có rating cao nhất", "Chỉnh sửa bình luận","Đổi mật khẩu và báo cáo", "Thoát" };
            string prompts = "Chọn tùy chọn tìm kiếm";
            int rac = 0;
            List<List<string>> mangtam = new List<List<string>>();

            Menu menu = new Menu(menuOptions, prompts);
            int choice = menu.Run(ref rac);

            switch (choice)
            {
                case 0:
                    // Xử lý tùy chọn 1
                    Console.Clear();
                    Console.Write("Nhập tên phim: ");
                    string searchTermName = Console.ReadLine();
                    SearchMovies(filePath, "tenphim", searchTermName, mangtam);
                    View_Film.User_Hienthi_Film(mangtam, ac_Id);
                    Console.ReadLine();
                    mangtam.Clear();
                    break;
                case 1:
                    // Xử lý tùy chọn 2
                    Console.Clear();
                    Console.Write("Nhập thể loại phim: ");
                    string searchTermGenre = Console.ReadLine();
                    SearchMovies(filePath, "theloai", searchTermGenre, mangtam);
                    View_Film.User_Hienthi_Film(mangtam, ac_Id);
                    Console.ReadLine();
                    mangtam.Clear();
                    break;
                case 2:
                    Console.Clear();
                    SearchMovies(filePath, "rating", "DESC", 10, mangtam);
                    View_Film.User_Hienthi_Film(mangtam, ac_Id);
                    Console.ReadLine();
                    break;
                case 3:
                    Console.Clear();
                    For_User_Comment.ViewUserComments(ac_Id); 
                    int cmtid = For_User_Comment.GetValidCommentId(ac_Id);
                    For_User_Comment.EditReview(cmtid);
                    break;
                case 4:
                    Report_A_Password.R_And_P();
                    break;
                case 5:
                    gate_end = true;
                    break;
            }

        }

        static void SearchMovies(string filePath, string columnName, string searchTerm, List<List<string>> mangtam)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                int dem = 0;
                
                foreach (string line in lines)
                {
                    string[] movieData = line.Split(',');
                    List<string> m_1 = new List<string>(movieData);
                    string tenPhim = movieData[1];
                    string theLoai = movieData[2];

                    if (tenPhim.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 && columnName == "tenphim")
                    {
                        dem = 1;
                        mangtam.Add(m_1);
                    }
                    else if (theLoai.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 && columnName == "theloai")
                    {
                        dem = 1;
                        mangtam.Add(m_1);
                    }
                }

                if (dem != 1)
                {
                    Console.WriteLine("Không tìm thấy phim phù hợp.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        static void SearchMovies(string filePath, string columnName, string sortOrder, int limit, List<List<string>> mangtam)
        {
            List<string> searchResults = new List<string>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                bool check = false;

                foreach (string line in lines)
                {
                    string[] movieData = line.Split(',');
                    List<string> m_1 = new List<string>(movieData);
                    mangtam.Add(m_1);
                    check = true;
                }

                if (!check)
                {
                    Console.WriteLine("Không tìm thấy phim phù hợp.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
            }
        }
        public static void Print_Normal(string text)
        {
            int max = 0;
            for (int i = 0; i < text.Split('\n').Length; i++)
            {
                if (text.Split('\n')[i].Length > max) max = text.Split('\n')[i].Length;
                else continue;
            }

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine('╔' + new string('═', 10 + max) + '╗');
            for (int i = 0; i < text.Split('\n').Length; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("║");
                Console.SetCursorPosition(6, Console.CursorTop);
                Console.Write(text.Split('\n')[i]);
                Console.SetCursorPosition(11 + max, Console.CursorTop);
                Console.WriteLine("║");
            }
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine('╚' + new string('═', 10 + max) + '╝');
        }
    }
}
