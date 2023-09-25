using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loginsan1
{
    internal class First_Interface
    {
        public static List<List<string>> Read_File(string filepath)
        {
            List<List<string>> database = new List<List<string>>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    List<string> dbphu = new List<string>(values);
                    database.Add(dbphu);
                }
            }
            return database;
        }
        public static void First_I(string filepath, string title, ref string accountnotify, ref bool accessauthority, ref bool gate_next, ref bool gate_end)
        {
            List<List<string>> database = new List<List<string>>();
            string TenNguoiDung = " ";
            string AccountId = " ";
            try
            {
                database = Read_File(filepath);

                int rowC = database.Count;

                string[] option1 = new string[] { "Đăng Nhập", " Đăng Ký ", "  Thoát  " };
                Menu menu = new Menu(option1, title);
                int choice1 = menu.Run();

                switch (choice1)
                {
                    case 0:
                        accessauthority = DangNhap(filepath, rowC, database,ref TenNguoiDung, ref AccountId);
                        Console.Clear();
                        if (accessauthority) accountnotify = $"{AccountId} - {TenNguoiDung} - Authority is Admin";
                        else accountnotify = $"{AccountId} - {TenNguoiDung} - Authority is User";
                        gate_next = true;
                        break;
                    case 1:
                        DangKy(filepath, rowC, database);
                        gate_next = true;
                        break;
                    case 2:
                        gate_end = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public static void DangKy(string filepath, int rowC, List<List<string>> database)
        {
            using (StreamWriter sm = new StreamWriter(filepath, true))
            {
                string accountId = Check_Nhapvao.Check_Id(rowC, database); // Check Id tự bổ sung vào chỗ trống hoặc tự tăng
                string Username = Check_Nhapvao.Check_Same_Name(rowC, database); // Kiểm tra xem liệu có trùng tên không
                string Password = Check_Nhapvao.Themdulieu_string("Nhập vào Password: ");

                int logintime = 1;
                int reporttime = 0;
                int commenttime = 0;
                string accessright = "U";

                sm.WriteLine($"{accountId},{Username},{Password},{logintime},{reporttime},{commenttime},{accessright}");
            }

            Console.WriteLine("Dữ liệu đã được ghi vào tệp tin.");
        }

        public static bool DangNhap(string filepath, int rowC, List<List<string>> database, ref string TenNguoiDung, ref string AccountId)
        {
            string Username;
            string Password;
            bool errormsgflag = false;
            while (true)
            {
                Console.Clear();
                if(errormsgflag) Console.WriteLine("Username hoặc Password sai. Vui lòng nhập lại");
                Username = Check_Nhapvao.Themdulieu_string("Username: ");
                Password = Check_Nhapvao.Themdulieu_string("Password: ");

                using (StreamReader sr = new StreamReader(filepath))
                {
                    for (int i = 0; i < rowC; i++)
                    {
                        if (Username == database[i][1] && Password == database[i][2])
                        {
                            AccountId = database[i][0];
                            TenNguoiDung = database[i][1];
                            if (database[i][6] == "A") return true;
                            else return false;
                        }
                    }
                }
                errormsgflag = true;
            }

        }
    }
}
