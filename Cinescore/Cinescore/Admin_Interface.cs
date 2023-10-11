using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Outside_Interface;
using User;

namespace Admin
{
    internal class Admin_Interface
    {
        static string dataFilePath = @"C:\Users\84967\OneDrive\Máy tính\Movie0.txt";

        public static void ProcessSelectedOption(string filepath, string accountnotify, ref bool gate_end)
        {
            string[] menuOptions = { "Hiển thị danh sách phim", "Chỉnh sửa thông tin phim", "Thêm phim mới",
                "Quản Lý Comment của User", "Lọc user", "Thoát" };
            string prompts = accountnotify + "\n" + "GIAO DIỆN ADMIN";
            int rac = 0;

            Menu menu = new Menu(menuOptions, prompts);
            int selectedIndex = menu.Run(ref rac);

            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    Xem_Phim.DisplayAllMovieNames();
                    break;
                case 1:
                    Console.Clear();
                    Xem_Phim.DisplayAllMovieNames(1);
                    break;
                case 2:
                    Console.Clear();
                    AddNewMovie();
                    Console.ReadLine();
                    break;
                case 3:
                    CommentManager.Comment_Control();
                    Console.ReadLine();
                    break;
                case 4:
                    // lọc rác
                    Xoa_User.Bin(filepath, accountnotify, ref gate_end);  
                    break;
                case 5:
                    gate_end = true;
                    break;
            }
        }
       
        public static void EditMovie(int F_id, string thongbao, List<string> mangtam)
        {
            string[] lines = File.ReadAllLines(dataFilePath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length >= 7)
                {
                    int existingFilmid;
                    if (int.TryParse(parts[0], out existingFilmid) && existingFilmid == F_id)
                    {
                        Search_Film.Print_Normal("Thông tin hiện tại của phim:\n" + thongbao);
                        Console.WriteLine("Nhập các thông tin mới:");

                        string tenphim, noidung, theloai, nhasanxuat;
                        int luotxem;
                        double rating;

                        // Nhập thông tin mới với các hàm Print_Prompts
                        tenphim = Check_Nhapvao.Themdulieu_string("Tên phim: ");
                        noidung = Check_Nhapvao.Themdulieu_string("Nội dung: ");
                        theloai = Check_Nhapvao.Themdulieu_string("Thể loại: ");
                        nhasanxuat = Check_Nhapvao.Themdulieu_string("Nhà sản xuất: ");
                        luotxem = Convert.ToInt32(parts[5]);
                        rating = Check_Nhapvao.DoubleInputWithPrompt("Rating: ");

                        // Tạo thông tin phim mới
                        string newMovieInfo = $"{F_id},{tenphim},{noidung},{theloai},{nhasanxuat},{luotxem},{rating}";

                        // Cập nhật thông tin phim trong danh sách
                        lines[i] = newMovieInfo;

                        File.WriteAllLines(dataFilePath, lines, Encoding.Unicode);

                        string notif_1 = $"Phim Id: {F_id}\nTên Phim: {tenphim}\n" +
                    $"Nội dung: {noidung}\nThể loại: {theloai}\n" +
                    $"Nhà sản xuất: {nhasanxuat}\nLượt Xem: {luotxem}\n" +
                    $"Rating: {Convert.ToDouble(rating)}";

                        Console.WriteLine("Dữ liệu đã được cập nhật thành công.");
                        Search_Film.Print_Normal(notif_1); // Hiển thị thông tin chi tiết mới
                        Console.WriteLine("Thông tin hiện tại của bộ phim sau chỉnh sửa!");
                    }
                }
            }
        }

        static void AddNewMovie()
        {
            Console.WriteLine("Nhập thông tin phim mới:");
            string tenphim, noidung, theloai, nhasanxuat;
            int luotxem;
            double rating;

            // Nhập thông tin mới với các hàm Print_Prompts
            tenphim = Check_Nhapvao.Themdulieu_string("Tên phim: ");
            noidung = Check_Nhapvao.Themdulieu_string("Nội dung: ");
            theloai = Check_Nhapvao.Themdulieu_string("Thể loại: ");
            nhasanxuat = Check_Nhapvao.Themdulieu_string("Nhà sản xuất: ");
            luotxem = 0;
            rating = Check_Nhapvao.DoubleInputWithPrompt("Rating: ");

            // Tạo thông tin phim mới
            string newMovieInfo = $"{GetNextFilmId()},{tenphim},{noidung},{theloai},{nhasanxuat},{luotxem},{rating}";

            // Ghi thông tin phim mới vào danh sách
            using (StreamWriter writer = new StreamWriter(dataFilePath, true))
            {
                writer.WriteLine(newMovieInfo);
            }

            string notif_1 = $"Phim Id: {GetNextFilmId()}\nTên Phim: {tenphim}\n" +
                    $"Nội dung: {noidung}\nThể loại: {theloai}\n" +
                    $"Nhà sản xuất: {nhasanxuat}\nLượt Xem: {luotxem}\n" +
                    $"Rating: {Convert.ToDouble(rating)}";

            Console.WriteLine("Phim mới đã được thêm thành công.");
            Search_Film.Print_Normal(notif_1);
            Console.WriteLine("Đây là thông tin của bộ phim bạn vừa thêm!");
        }
        static int GetNextFilmId()
        {
            if (File.Exists(dataFilePath))
            {
                string[] lines = File.ReadAllLines(dataFilePath);
                if (lines.Length > 0)
                {
                    string lastLine = lines[lines.Length - 1];
                    string[] parts = lastLine.Split(',');
                    if (parts.Length >= 1)
                    {
                        int lastFilmId;
                        if (int.TryParse(parts[0], out lastFilmId))
                        {
                            return lastFilmId + 1;
                        }
                    }
                }
            }

            return 1; // Trả về 1 nếu không tìm thấy dữ liệu hoặc lỗi.
        }
    }
}
