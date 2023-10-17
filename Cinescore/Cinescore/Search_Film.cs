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
        static string filePath = @"Movie0.txt"; 

        public static void ProcessSelectedOption(ref bool gate_end, int ac_Id, string accountnotify)
        {
            string[] menuOptions = { "Tìm phim theo tên", "Tìm phim theo thể loại", 
                "Top phim có rating cao nhất", "Chỉnh sửa bình luận","Đổi mật khẩu và báo cáo", "Thoát" };
            string prompts = accountnotify + "\nGIAO DIỆN USER";
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
                    if (mangtam.Count != 0) 
                    {
                        View_Film.User_Hienthi_Film(mangtam, ac_Id);
                        mangtam.Clear();
                    }
                    break;
                case 1:
                    // Xử lý tùy chọn 2
                    Console.Clear();
                    Console.Write("Nhập thể loại phim: ");
                    string searchTermGenre = Console.ReadLine();
                    SearchMovies(filePath, "theloai", searchTermGenre, mangtam);
                    if (mangtam.Count != 0)
                    {
                        View_Film.User_Hienthi_Film(mangtam, ac_Id);
                        mangtam.Clear();
                    }
                    break;
                case 2:
                    Console.Clear();
                    SearchMovies(filePath, "rating", "DESC", 10, mangtam);
                    if (mangtam.Count != 0)
                    {
                        View_Film.User_Hienthi_Film(mangtam, ac_Id);
                        mangtam.Clear();
                    }
                    break;
                case 3:
                    Console.Clear();
                    For_User_Comment.ViewUserComments(ac_Id); 
                    int cmtid = For_User_Comment.GetValidCommentId(ac_Id);
                    For_User_Comment.EditReview(cmtid);
                    break;
                case 4:
                    Report_A_Password.R_And_P(ac_Id);
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
                    string theLoai = movieData[3];

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
                    Console.ReadLine();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File không tồn tại.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Search_Film.SearchMovies_1: {ex.Message}");
                Console.ReadLine();
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
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Search_Film.SearchMovies_2: {ex.Message}");
                Console.ReadLine();
            }
        }
        public static void Print_Normal(string text)
        {
            try
            {
                int max = 0;
                string[] prompt = text.Split('\n');
                string fix_text = null;
                for (int i = 0; i < prompt.Length; i++)
                {
                    Khong_Vuot_60_Xuongdong(ref fix_text, prompt[i]);
                    if (i + 1 < prompt.Length) fix_text += "\n";
                }
                for (int i = 0; i < fix_text.Split('\n').Length; i++)
                {
                    if (fix_text.Split('\n')[i].Length > max) max = fix_text.Split('\n')[i].Length;
                    else continue;
                }

                Console.SetCursorPosition(0, Console.CursorTop);
                Console.WriteLine('╔' + new string('═', 10 + max) + '╗');
                for (int i = 0; i < fix_text.Split('\n').Length; i++)
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write("║");
                    Console.SetCursorPosition(6, Console.CursorTop);
                    Console.Write(fix_text.Split('\n')[i]);
                    Console.SetCursorPosition(11 + max, Console.CursorTop);
                    Console.WriteLine("║");
                }
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.WriteLine('╚' + new string('═', 10 + max) + '╝');
            }
            catch(Exception h)
            {
                Console.WriteLine($"Error Search_Film.Print_Normal: {h.Message}");
                Console.ReadLine();
            }
        }

        public static void Khong_Vuot_60_Xuongdong(ref string fix_text, string prompt)
        {
            try
            {
                int col = 0;
                if (col + prompt.Length >= 60)
                {
                    string[] check_text = prompt.Split(' ');
                    for (int j = 0; j < check_text.Length; j++)
                    {
                        if (col + check_text[j].Length <= 60)
                        {
                            fix_text += check_text[j];
                            col += check_text[j].Length;
                        }
                        else
                        {
                            fix_text += "\n" + check_text[j];
                            col = check_text[j].Length + 1;
                        }

                        if (j + 1 < check_text.Length) fix_text += " ";
                    }
                }
                else
                {
                    fix_text += prompt;
                }
            }
            catch (Exception y)
            {
                Console.WriteLine($"Error Search_Film.Khong_Vuot_60_Xuongdong: {y.Message}");
                Console.ReadLine();
            }
        }

        public static void Update_Luotxem(string F_id, List<List<string>> mangtam, int sele_num)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] movieInfo = lines[i].Split(',');
                    if (movieInfo.Length >= 2 && movieInfo[0] == F_id)
                    {
                        int views = int.Parse(movieInfo[5].Trim()) + 1; // Tăng số lượt xem
                        movieInfo[5] = views.ToString(); // Cập nhật số lượt xem trong mảng dữ liệu
                        mangtam[sele_num][5] = views.ToString();
                        lines[i] = string.Join(",", movieInfo); // Cập nhật dòng dữ liệu trong mảng lines
                        break;
                    }
                }
                // Ghi dữ liệu đã được cập nhật trở lại vào file Movie0.txt
                File.WriteAllLines(filePath, lines);
            }
            catch(Exception x)
            {
                Console.WriteLine($"Error Search_Film.Udpate_Luotxem: {x.Message}");
                Console.ReadLine();
            }
        }
    }
}
