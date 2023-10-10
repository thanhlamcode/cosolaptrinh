using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outside_Interface;

namespace User
{
    internal class View_Film
    {
        public static void User_Hienthi_Film(List<List<string>> mangtam, int ac_Id)
        {
            bool display_movie = false;
            string prompts = null;
            string[] menuOptions = { "Trước", "Tiếp", "Chọn Danh sách", "Quay lại" };
            int film_max = 0;
            for(int i = 0; i < mangtam.Count; i++)
            {
                prompts += $"Phim Id: {mangtam[i][0]} Tên Phim: {mangtam[i][1]}";
                if (i + 1 < mangtam.Count) prompts += "\n";
            }    
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
                        Ham_moi(mangtam, prompts, film_max, ac_Id);
                        Console.ReadLine();
                        break;
                    case 3:
                        display_movie = true;
                        break;

                }
            }
        }

        static void Ham_moi(List<List<string>> mangtam, string text, int film_max, int ac_Id)
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
                string F_id = mangtam[selectedIndex][0];
                string thongbao = null;
                thongbao = $"Phim Id: {mangtam[selectedIndex][0]}\nTên Phim: {mangtam[selectedIndex][1]}\n" +
                    $"Thể loại: {mangtam[selectedIndex][2]}\nNhà sản xuất: {mangtam[selectedIndex][3]}\n" +
                    $"Số tập: {Convert.ToInt32(mangtam[selectedIndex][4])}\nLượt Xem: {Convert.ToInt32(mangtam[selectedIndex][5])}\n" +
                    $"Doanh Thu: {Convert.ToInt32(mangtam[selectedIndex][6])}\nRating: {Convert.ToDouble(mangtam[selectedIndex][7])}";
                Menu_Film_User_D(thongbao, ac_Id, F_id);
            }
        }

        static void Menu_Film_User_D(string prompts, int ac_Id, string F_id)
        {
            string[] options_1 = {"Thêm Review", "Xem tất cả review", "Quay lại"};
            int rac = 0;
            Menu menu = new Menu(options_1, prompts);
            int choice = menu.Run(ref rac);

            switch (choice)
            {
                case 0:
                    Console.Clear();
                    For_User_Comment.AddReview(F_id, ac_Id); ; // truyền filmid, account Id
                    break;
                case 1:
                    Console.Clear();
                    For_User_Comment.ViewReview(F_id);
                    break;
                case 2:
                    break;
            }
        }
    }
}
