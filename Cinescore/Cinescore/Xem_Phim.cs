﻿using System;
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
                    string movieName = movieData[1];
                    mangtam.Add(m_1);
                    prompts += $"{filmId}: {movieName}";
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
                        Console.ReadLine();
                        mangtam.Clear();
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
            Menu menu = new Menu(menuOptions, prompts, true);
            int selectedIndex = menu.Run(ref rac);

            // Xử lý khi người dùng đã chọn một bộ phim
            if (selectedIndex >= 0 && selectedIndex < movieOptions.Count)
            {
                string thongbao = null;
                int F_id = Convert.ToInt32(mangtam[selectedIndex][0]);
                thongbao = $"Phim Id: {mangtam[selectedIndex][0]}\nTên Phim: {mangtam[selectedIndex][1]}\n" +
                    $"Thể loại: {mangtam[selectedIndex][2]}\nNhà sản xuất: {mangtam[selectedIndex][3]}\n" +
                    $"Số tập: {Convert.ToInt32(mangtam[selectedIndex][4])}\nLượt Xem: {Convert.ToInt32(mangtam[selectedIndex][5])}\n" +
                    $"Doanh Thu: {Convert.ToInt32(mangtam[selectedIndex][6])}\nRating: {Convert.ToDouble(mangtam[selectedIndex][7])}";

                if (edit_num == 0) Admin_Interface.ShowMovieDetails(thongbao);
                else Admin_Interface.EditMovie(F_id, thongbao, mangtam[selectedIndex]);
                Console.ReadLine();
            }
        }
    }
}
