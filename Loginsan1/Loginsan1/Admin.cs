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
        public static void Bin(string filepath, string title, string accountnotify, ref bool gate_next, ref bool gate_end)
        {
            List<List<string>> database = new List<List<string>>();
            List<List<string>> Mangtam = new List<List<string>>();
            List<List<string>> Mangbin = new List<List<string>>();
            try
            {
                database = First_Interface.Read_File(filepath);
                string[] option2 = new string[] {"Lọc theo số lần đăng nhập", "Lọc theo số lần bị report", 
                    "Lọc theo số lần comment", "Lọc theo AccountId", "Thoát"};
                Menu menu = new Menu(option2, title, accountnotify);
                int choice2 = menu.Run();
                switch (choice2)
                {
                    case 0:
                        Login_Report_CommentTime(database, Mangtam, Mangbin, 3);
                        Print_Loop_File(Mangtam, filepath);
                        break;
                    case 1:
                        Login_Report_CommentTime(database, Mangtam, Mangbin);
                        Print_Loop_File(Mangtam, filepath);
                        break;
                    case 2:
                        Login_Report_CommentTime(database, Mangtam, Mangbin, 5);
                        Print_Loop_File(Mangtam, filepath);
                        break;
                    case 3:
                        Loc_AccountId(database, Mangtam, Mangbin);
                        Print_Loop_File(Mangtam, filepath);
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

        public static void Print_Loop_File(List<List<string>> Mangtam, string filepath)
        {
            using (StreamWriter st = new StreamWriter(filepath))
            {
                for (int i = 0; i < Mangtam.Count; i++)
                {
                    for (int j = 0; j < Mangtam[0].Count; j++)
                    {
                        st.Write(Mangtam[i][j]);
                        if (j + 1 < Mangtam[0].Count) st.Write(',');
                    }
                    st.Write('\n');
                }
            }
        }
        private static void Login_Report_CommentTime(List<List<string>> database, List<List<string>> Mangtam, List<List<string>> Mangbin, int b = 4)
        {
            Console.Clear();
            int a = Check_Nhapvao.Themdulieu_int("Nhập số từ 1 đến 999: ");
            int n = 0;
            for(int i = 0; i < database.Count; i++)
            {
                if (int.TryParse(database[i][b], out n) && database[i][6] == "U")
                {
                    if (b == 4)
                    {
                        if (n > a) Mangbin.Add(database[i]);
                        else Mangtam.Add(database[i]);
                    }
                    else
                    {
                        if (n < a) Mangbin.Add(database[i]);
                        else Mangtam.Add(database[i]);
                    }
                }
                else Mangtam.Add(database[i]);
            } 
            
            if(Mangtam.Count < database.Count)
            {
                Console.WriteLine("Đã xóa thành công, những user bị xóa là: ");
                for(int i = 0; i < Mangbin.Count; i++)
                {
                    Console.WriteLine($"{Mangbin[i][1]} - AccountId: {Mangbin[i][0]} - Số lần đăng nhập: {Mangbin[i][b]}");
                }    
            }
            else
            {
                Console.WriteLine("Không có User nào thỏa điều kiện.");
            }    
        }

        private static void Loc_AccountId(List<List<string>> database, List<List<string>> Mangtam, List<List<string>> Mangbin)
        {
            int a = Check_Nhapvao.Themdulieu_int("Nhập số từ 100 đến 999: ");
            int n = 0;
            for (int i = 0; i < database.Count; i++)
            {
                if (int.TryParse(database[i][0], out n) && database[i][6] == "U")
                {
                    if (n == a) Mangbin.Add(database[i]);
                    else Mangtam.Add(database[i]);
                }
                else Mangtam.Add(database[i]);
            }

            if (Mangtam.Count < database.Count)
            {
                Console.WriteLine("Đã xóa thành công, User bị xóa là: ");
                for (int i = 0; i < Mangbin.Count; i++)
                {
                    Console.WriteLine($"{Mangbin[i][1]} - AccountId: {Mangbin[i][0]}");
                }
            }
            else
            {
                Console.WriteLine($"Không tồn tại User {a} thỏa điều kiện.");
            }
        }

       
    }
}
