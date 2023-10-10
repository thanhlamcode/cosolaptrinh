using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outside_Interface;

namespace Admin
{
    internal class Xem_Phim
    {
        static string dataFilePath = @"C:\Users\84967\OneDrive\Máy tính\data.txt";

        public static void DisplayAllMovieNames(int edit_num = 0)
        {
            string[] lines = File.ReadAllLines(dataFilePath, Encoding.Unicode);
            string prompts = null;
            bool display_movie = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] movieData = line.Split(',');

                // Kiểm tra xem có đủ thông tin để hiển thị film ID và tên phim không
                if (movieData.Length >= 2)
                {
                    string filmId = movieData[0];
                    string movieName = movieData[1];

                    prompts += $"{filmId}: {movieName}";
                    if (i + 1 < lines.Length) prompts += "\n";
                }
            }

            string[] menuOptions = { "Trước", "Tiếp", "Chọn Danh sách", "Quay lại"};
            int film_max = 20;

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
                            film_max = 20;
                        }
                        break;
                    case 1:
                        film_max += 20;
                        if (film_max > prompts.Split('\n').Length)
                        {
                            film_max = prompts.Split('\n').Length;
                        }
                        break;
                    case 2:
                        Ham_moi(lines, prompts, film_max, edit_num);
                        Console.ReadLine();
                        break;
                    case 3:
                        display_movie = true;
                        break;

                }
            }       
        }

        static void Ham_moi(string[] lines, string text, int film_max,int edit_num)
        {
            List<string> movieOptions = new List<string>();
            int rac = 0;
            int check_num = film_max - 20;
            for(int i = film_max - 20; i < film_max; i++)
            {
                movieOptions.Add(text.Split('\n')[i]);
            }
            string[] menuOptions = movieOptions.ToArray();
            string prompts = "Chọn một trong số các bộ phim dưới đây";
            Menu menu = new Menu(menuOptions, prompts, true);
            int selectedIndex = menu.Run(ref rac);

            // Xử lý khi người dùng đã chọn một bộ phim
            if (selectedIndex >= 0 && selectedIndex < movieOptions.Count)
            {
                if (edit_num == 0) Admin_Interface.ShowMovieDetails(lines[selectedIndex + check_num]);
                else Admin_Interface.EditMovie(selectedIndex + check_num + 1);
                Console.ReadLine();
            }
        }
    }
}
