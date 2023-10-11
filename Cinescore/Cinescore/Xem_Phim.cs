using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outside_Interface;
using User;
using static System.Net.Mime.MediaTypeNames;

namespace Admin
{
    internal class Xem_Phim
    {
        static string dataFilePath = @"C:\Users\84967\OneDrive\Máy tính\Movie0.txt";

        public static void DisplayAllMovieNames(int edit_num = 0)
        {
            string[] lines = File.ReadAllLines(dataFilePath);
            List<List<string>> mangtam = new List<List<string>>();
            string prompts = null;
            bool display_movie = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] movieData = lines[i].Split(',');
                List<string> m_1 = new List<string>(movieData);

                // Kiểm tra xem có đủ thông tin để hiển thị film ID và tên phim không
                if (movieData.Length > 0)
                {
                    string filmId = movieData[0];
                    string movieName = Khong_Vuot_60(Convert.ToString(movieData[1]));
                    mangtam.Add(m_1);
                    prompts += $"{filmId}- Tên Phim: {movieName}";
                    if (i + 1 < lines.Length) prompts += "\n";
                }
            }

            string[] menuOptions = { "Trước", "Tiếp", "Chọn Danh sách", "Quay lại"};
            int film_max = 0;
            if (prompts.Split('\n').Length > 20) film_max = 20;

            Menu menu = new Menu(menuOptions, prompts);

            while (!display_movie)
            {
                Console.Clear();
                int selectedIndex = menu.Run(ref film_max);
                switch (selectedIndex)
                {
                    case 0:
                        film_max -= 20;
                        if (film_max < 20)
                        {
                            if (prompts.Split('\n').Length > 20) film_max = 20;
                            else film_max = 0;
                        }
                        break;
                    case 1:
                        film_max += 20;
                        if (film_max > prompts.Split('\n').Length)
                        {
                            if (prompts.Split('\n').Length > 20) film_max = prompts.Split('\n').Length;
                            else film_max = 0;
                        }
                        break;
                    case 2:
                        Ham_moi(mangtam, prompts, film_max, edit_num);
                        break;
                    case 3:
                        display_movie = true;
                        break;

                }
            }       
        }

        static void Ham_moi(List<List<string>> mangtam, string text, int film_max,int edit_num)
        {
            List<string> movieOptions = new List<string>();
            int rac = 0;
            int check_num = 0;
            if (text.Split('\n').Length >= 20) check_num = film_max - 20;
            else film_max = text.Split('\n').Length;

            for (int i = check_num; i < film_max; i++)
            {
                movieOptions.Add(text.Split('\n')[i]);
            }
            string[] menuOptions = movieOptions.ToArray();
            string prompts = "Chọn một trong số các bộ phim dưới đây";
            Menu menu = new Menu(menuOptions, prompts);
            int selectedIndex = menu.Run(ref rac);

            // Xử lý khi người dùng đã chọn một bộ phim
            if (selectedIndex >= 0 && selectedIndex < movieOptions.Count) 
            {
                int sele_num = selectedIndex + check_num;
                int F_id = Convert.ToInt32(mangtam[sele_num][0]);
                string F_name = Convert.ToString(mangtam[sele_num][1]);

                string thongbao = $"Phim Id: {F_id}\nTên Phim: {F_name}\n" +
                    $"Nội dung: {mangtam[sele_num][2]}\nThể loại: {mangtam[sele_num][3]}\n" +
                    $"Nhà sản xuất: {Convert.ToString(mangtam[sele_num][4])}\nLượt Xem: {Convert.ToInt32(mangtam[sele_num][5])}\n" +
                    $"Rating: {Convert.ToDouble(mangtam[sele_num][6])}";

                if (edit_num == 0) Search_Film.Print_Normal(thongbao);
                else Admin_Interface.EditMovie(F_id, thongbao, mangtam[selectedIndex]);
                Console.ReadLine();
            }
        }

        public static string Khong_Vuot_60(string moviedata)
        {
            int col = 0;
            string prompt = null;
            if(moviedata.Length >= 60)
            {
                string[] text = moviedata.Split(' ');
                for (int i = 0; i < text.Length; i++)
                {
                    if (col + text[i].Length <= 60)
                    {
                        string alpha1 = text[i] + " ";
                        prompt += alpha1;
                        col += alpha1.Length;
                    }
                    else break;
                }    
            }    
            else
            {
                prompt = moviedata;
            }

            return prompt;
        }
    }
}
