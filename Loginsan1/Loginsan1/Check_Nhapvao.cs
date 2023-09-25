using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loginsan1
{
    internal class Check_Nhapvao
    {
        public static string Themdulieu_string(string text)
        {
            string at1;
            while (true)
            {
                Console.Write(text);
                at1 = Console.ReadLine();
                if (!string.IsNullOrEmpty(at1) && !string.IsNullOrWhiteSpace(at1))
                {
                    return at1;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Giá trị nhập vào không hợp lệ. Vui lòng nhập lại");
                }
            }
        }

        public static int Themdulieu_int(string text)
        {
            int num;
            while (true)
            {
                Console.Write(text);
                if (int.TryParse(Console.ReadLine(), out num) && num > 0)
                {
                    return num;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Giá trị nhập vào không hợp lệ. Vui lòng nhập lại");
                }
            }
        }

        public static string Check_Id(int rowC, List<List<string>> database)
        {
            int max = 0;
            int c = 0;
            int accountId = 0;
            for (int i = 0; i < rowC; i++)
            {
                if (int.TryParse(database[i][0], out c))
                {
                    if (max < c) max = c;
                }
            }

            for (int i = 100; i <= max; i++)
            {
                if (Have_Space(i, rowC, database)) 
                {
                    accountId = i;
                    break;
                }
                else if ((i + 1) > max) accountId = i + 1;
            }

            return accountId.ToString();
        }

        private static bool Have_Space(int num_check, int rowC, List<List<string>> database)
        {
            int c = 0;
            for (int i = 0; i < rowC; i++)
            {
                if (int.TryParse(database[i][0], out c))
                {
                    if (c == num_check) return false;
                }
            }
            return true;
        }

        public static string Check_Same_Name(int rowC, List<List<string>> database)
        {
            string Username = " ";
            do
            {
                Console.Clear();
                if(!string.IsNullOrEmpty(Username) && Same_Name(Username, rowC, database))
                {
                    Console.WriteLine("Username đã tồn tại. Vui lòng nhập lại.");
                }    
                Username = Themdulieu_string("Nhập vào Username: ");
            } while (Same_Name(Username, rowC, database));

            return Username;
        }

        private static bool Same_Name(string Username, int rowC, List<List<string>> database)
        {
            for (int i = 0; i < rowC; i++)
            {
                if (Username == database[i][1]) return true;
            }
            return false;
        }
    }
}
