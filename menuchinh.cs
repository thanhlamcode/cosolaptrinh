using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    public class support
    {
        public static void Menuchonlua()
        {
            string[] menuOptions = { "Hiển thị danh sách phim", "Chỉnh sửa thông tin phim", "Thêm phim mới", "Thoát" };
            string prompts = "Quản lý danh sách phim\n" +
                             "Chọn tùy chọn (1/2/3/4):";

            Menu menu = new Menu(menuOptions, prompts);
            int selectedIndex = menu.Run();

            ProcessSelectedOption(selectedIndex);

            Console.WriteLine("Nhấn phím bất kỳ để thoát.");
            Console.ReadKey();
        }
        static void ProcessSelectedOption(int selectedIndex)
        {
            bool check = true;
            do
            {
                switch (selectedIndex)
                {
                    case 0:
                        // Xử lý tùy chọn 1
                        Console.Clear();
                        Console.WriteLine("Bạn đã chọn Hiển thị danh sách phim.");
                        DisplayAllMovieNames();
                        Console.ReadLine();
                        Menuchonlua();
                        break;
                    case 1:
                        // Xử lý tùy chọn 2
                        Console.WriteLine("Bạn đã chọn Chỉnh sửa thông tin phim.");
                        Hienthidongian();

                        int filmid;
                        bool isNumeric = false;

                        do
                        {
                            Console.Write("Nhập filmid của bản ghi bạn muốn chỉnh sửa: ");
                            string input = Console.ReadLine();

                            isNumeric = int.TryParse(input, out filmid);

                            if (!isNumeric)
                            {
                                Console.WriteLine("Vui lòng nhập một số nguyên hợp lệ.");
                            }
                        } while (!isNumeric);

                        EditMovie(filmid);
                        Console.ReadLine();
                        Menuchonlua();
                        break;
                    case 2:
                        // Xử lý tùy chọn 3
                        Console.Clear();
                        Console.WriteLine("Bạn đã chọn Thêm phim mới.");
                        AddNewMovie();
                        Console.ReadLine();
                        Menuchonlua();
                        break;
                    case 3:
                        // Xử lý tùy chọn 4
                        Console.WriteLine("Bạn đã chọn Thoát.");
                        check = false;
                        Console.WriteLine("Chương trình đang kết thúc...................");
                        break;
                    case 4:
                        // lọc rác
                        Loc_USER.Bin(filepath, accountnotify, ref gate_next, ref gate_end);
                        Console.ReadKey(); // Dùng để nhận giá trị nhập vào từ bàn phím user
                                           // -> Mục đích để hiện thị thông báo sau khi lọc rác thành công
                }
            } while (check);

        }
    }
}
