using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loginsan1
{
    internal class Admin
    {
        public static void Bin(string filepath, string accountnotify, ref bool gate_next, ref bool gate_end)
        {
            List<List<string>> database = new List<List<string>>();
            List<List<string>> Mangtam = new List<List<string>>();
            List<List<string>> Mangbin = new List<List<string>>();
            try
            {
                database = First_Interface.Read_File(filepath);
                string[] option2 = new string[] {"Lọc theo số lần đăng nhập", "Lọc theo số lần bị report", 
                    "Lọc theo số lần comment", "Lọc theo AccountId", "Thoát"};
                Menu menu = new Menu(option2, accountnotify + "\n" +"LỌC USER");
                int choice2 = menu.Run();
                switch (choice2)
                {
                    case 0:
                        Loc_User(database, Mangtam, Mangbin, 3);
                        First_Interface.Print_Loop_File(Mangtam, filepath);
                        break;
                    case 1:
                        Loc_User(database, Mangtam, Mangbin, 4);
                        First_Interface.Print_Loop_File(Mangtam, filepath);
                        break;
                    case 2:
                        Loc_User(database, Mangtam, Mangbin, 5);
                        First_Interface.Print_Loop_File(Mangtam, filepath);
                        break;
                    case 3:
                        Loc_User(database, Mangtam, Mangbin, 0);
                        First_Interface.Print_Loop_File(Mangtam, filepath);
                        break;
                    case 4:
                        gate_end = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void Loc_User(List<List<string>> database, List<List<string>> Mangtam, List<List<string>> Mangbin, int b)
        {
            Console.Clear();
            string text = "Lọc theo ";
            string text2 = null;
            if (b == 3)
            {
                text2 = "Số lần đăng nhập";
                text += text2 + " nhỏ hơn: ";
            }
            else if (b == 4)
            {
                text2 = "Số lần report";
                text += text2 + " lớn hơn: ";
            }
            else if (b == 5)
            { 
                text2 = "Số lần review phim";
                text += text2 + " nhỏ hơn: ";
            }
            else
            {
                text2 = "Số lần Account Id";
                text += text2 + " bằng: ";
            } 
                

            while(true)
            {
                int a = Check_Nhapvao.Themdulieu_int(text);
                int n = 0;
                for (int i = 0; i < database.Count; i++)
                {
                    if (int.TryParse(database[i][b], out n) && database[i][6] == "U")
                    {
                        if (b == 4 && n >= a)
                        {
                            Mangbin.Add(database[i]);
                        }
                        else if (b == 0 && n == a)
                        {
                            Mangbin.Add(database[i]);
                        }
                        else if ((b == 3 || b == 5) && n <= a)
                        {
                            Mangbin.Add(database[i]);
                        }    
                        else Mangtam.Add(database[i]);
                    }
                    else Mangtam.Add(database[i]);
                }

                if (Mangtam.Count < database.Count)
                {
                    Console.Clear();
                    Console.WriteLine("Đã xóa thành công, những user bị xóa là: ");
                    for (int i = 0; i < Mangbin.Count; i++)
                    {
                        text = $"{Mangbin[i][1]} - AccountId: {Mangbin[i][0]}";
                        text += b != 0 ? $" - {text2}: { Mangbin[i][b]}" : " "; 
                        Console.WriteLine(text);                       
                    }
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Không có User nào thỏa điều kiện.");
                    // Xóa mảng để tránh việc khi ta nhập vào a mà đến else này thì nó sẽ xóa tất cả data có trong hai mảng dưới
                    // Tránh trường hợp là dữ liệu vẫn còn lưu lại dẫn đến lỗi khi ta nhập lại a để lọc user.
                    Mangtam.Clear();
                    Mangbin.Clear();
                }
            }    
        }     
    }
}
