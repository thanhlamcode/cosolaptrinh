using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DangNhap
{
    internal class Admin
    {
        public static void Loc_Rac(SqlConnection connection, int num)
        {
            // dùng gán với hàm Nhap_Vao
            using(var command = new SqlCommand())
            {
                command.Connection = connection; 
                try
                {
                    switch (num)
                    {
                        case 1:
                            command.CommandText = "DELETE FROM Account WHERE logintime <= @lgt " +
                    "AND accessright = 0";
                            command.Parameters.AddWithValue("@lgt", 
                                Rogin.Nhap_Vao<int>("Enter the number (1 -> 999): ", n => n >= 1 && n <= 999));
                            break;
                        case 2:
                            command.CommandText = "DELETE FROM Account WHERE reportntime >= @rt " +
                    "AND accessright = 0";
                            command.Parameters.AddWithValue("@rt", 
                                Rogin.Nhap_Vao<int>("Enter the number (1 -> 999): ", n => n >= 1 && n <= 999));
                            break;
                        case 3:
                            command.CommandText = "DELETE FROM Account WHERE commenttime <= @ct " +
                    "AND accessright = 0";
                            command.Parameters.AddWithValue("@ct", 
                                Rogin.Nhap_Vao<int>("Enter the number (1 -> 999): ", n => n >= 1 && n <= 999));
                            break;
                        case 4:
                            command.CommandText = "DELETE FROM Account WHERE accountId = @id " +
                    "AND accessright = 0";
                            command.Parameters.AddWithValue("@id", 
                                Rogin.Nhap_Vao<int>("Enter the number (100 -> 999): ", n => n >= 100 && n <= 999));
                            break;
                        case 5://Exit
                            Console.WriteLine("Thanks for using our application!");
                            return;

                    }

                    command.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error Loc_Logintime: {0}", ex.Message);
                }
                
            }    
        }

        // Thêm hàm vào dưới đây
    }
}
