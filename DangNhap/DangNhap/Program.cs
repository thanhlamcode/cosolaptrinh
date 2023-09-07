using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Login
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Project_DB.mdf;Integrated Security=True";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT username, password, accessright FROM Account";

                char accessauthority = ' ';

                try
                {
                    while (true)
                    {
                        Console.Write("Username: ");
                        string tk1 = Console.ReadLine();
                        Console.Write("Password: ");
                        string ps1 = Console.ReadLine();

                        var sqlreader = command.ExecuteReader();

                        while (sqlreader.Read())
                        {
                            var tk2 = sqlreader.GetString(0);
                            var ps2 = sqlreader.GetString(1);
                            bool ar = sqlreader.GetBoolean(2);

                            if (tk1 == tk2 && ps1 == ps2)
                            {
                                if (ar == true)
                                {
                                    accessauthority = 'A'; 
                                    break;
                                }
                                else
                                {
                                    accessauthority = 'U'; 
                                    break;
                                }
                            }

                        }

                        if (accessauthority == 'A' || accessauthority == 'U') break;
                        else Console.WriteLine("Your username or password is wrong. Please Try Again");
                    }
                }
                catch(Exception loi)
                {
                    Console.WriteLine("Error: {0}", loi.Message);
                }

                // Phần code từ chỗ này về sau là nơi thao tác vai trò Admin và User!!!!
                Console.WriteLine("\n\n\n{0}", accessauthority);

                connection.Close();
            }
        }

        public static int Nhap_Vao(int a, int b)
        {
            int n;
            while(true)
            {
                if(int.TryParse(Console.ReadLine(), out n) && Dieukien(a, b, n))
                {
                    break;
                }    
                else
                {
                    Console.WriteLine("Invalid Value. Please try again");
                }    
            }

            return n;
        }

        public static bool Dieukien(int a, int b, int n)
        {
            if (n >= a && n <= b) return true;
            else return false;
        }
    }
}
