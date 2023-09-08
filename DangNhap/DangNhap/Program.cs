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

                int choose1 = Nhap_Vao(1, 3);
                switch (choose1)
                {
                    case 1://Login
                        char accessauthority = Login(connection);

                        // Từ đoạn code sau là Phần Admin và User
                        Console.WriteLine($"Authority is {accessauthority}");
                        break;

                    case 2: //Register
                        int biggest_num = Biggest_id(connection);
                        try
                        {
                            for (int i = 100; i <= biggest_num; i++)
                            {
                                // Nếu có accountId vào trống ví dụ như ta có trong bảng accountId 100, 101, 104, 105
                                // Thì for sẽ lặp qua từ 100 -> 105, nếu đến giá trị 102 mà nó không tìm thấy
                                // trong table thì nó sẽ tạo một accountId mới trong bảng với giá trị là 102 rồi chèn các data vào
                                if (IsEmptyNumber_Namesake(connection, i, ""))
                                {
                                    Register(connection, i);
                                    break;
                                }
                                // Nếu không tìm thấy giá trị trống nào giữa các dòng data thì nó sẽ tự động chèn xuống dưới vị trí bên dưới còn trống
                                else if ((i + 1) > biggest_num)
                                {
                                    Register(connection, i + 1);
                                }
                            }
                            Console.WriteLine("Success Register.");
                        }
                        catch (Exception loi)
                        {
                            Console.WriteLine("False Register. ");
                            Console.WriteLine("Error Main Register: {0}", loi.Message);
                        }
                        break;

                    case 3://Exit
                        Console.WriteLine("Thanks for using our application!");
                        return;
                }

                connection.Close();
            }
        }

        public static int Nhap_Vao(int a, int b)
        {
            //Kiểm tra giá trị số nhập vào
            int n;
            while(true)
            {
                Console.Write($"Enter the number between {a} and {b}: ");
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

        public static char Login(SqlConnection connection)
        {
            char accessauthority = ' ';
            using (var command = new SqlCommand("SELECT username, password, accessright FROM Account", connection))
            {
                try
                {
                    while (true)
                    {
                        //Nhập vào TK và MK
                        Console.Write("Username: ");
                        string tk1 = Console.ReadLine();
                        Console.Write("Password: ");
                        string ps1 = Console.ReadLine();

                        var sqlreader = command.ExecuteReader();

                        // Đọc từng dòng data và lấy nó ra, kiểm tra với tk1,ps1 vừa nhập vào cho đến khi
                        // sqlreader.Read() trả về false -> Tức là dòng qua dòng data cuối.
                        while (sqlreader.Read())
                        {
                            var tk2 = sqlreader.GetString(0);
                            var ps2 = sqlreader.GetString(1);
                            var ar = sqlreader.GetBoolean(2);

                            if (tk1 == tk2 && ps1 == ps2)
                            {
                                // Để xác định vai trò Admin hay User và dừng vòng lặp trong khi
                                // đã kiểm tra đúng TK và MK
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
                        sqlreader.Close();

                        // Nếu đúng thì ngừng vòng lặp ngoài, sai thì thông báo nhập lại
                        if (accessauthority == 'A' || accessauthority == 'U') break;
                        else Console.WriteLine("Your username or password is wrong. Please Try Again.");
                    }
                }
                catch (Exception loi)
                {
                    Console.WriteLine("Error Login: {0}", loi.Message);
                }
            }

            return accessauthority;
        }

        public static int Biggest_id(SqlConnection connection)
        {
            int num = 0;
            // Truy vấn chỉ muốn lấy ra 1 dòng kết quả  (TOP 1) từ cột accountId từ bảng Account theo thứ tự accountId giảm dần
            // Lấy giá trị đầu tiên sau khi sắp xếp
            using (var command = new SqlCommand("SELECT TOP 1 accountId FROM Account ORDER BY accountId DESC", connection))
            {
                var sqlreader = command.ExecuteReader();
                while (sqlreader.Read())
                {
                    num = sqlreader.GetInt32(0);
                }
                sqlreader.Close();
            }
            return num;
        }

        public static bool IsEmptyNumber_Namesake(SqlConnection connection, int number, string ten)
        {
            // Đếm số lượng record có trong Table Account với điều kiện giá trị cột accountId phải trùng với 
            // tham số truyền vào @id
            // Nếu tìm thấy thì trả về false, không thì true -> Dùng làm điều kiện thực thi Register
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                // Lý do kiểm tra xem string ten rỗng hay null
                if (string.IsNullOrEmpty(ten))
                {
                    command.CommandText = "SELECT COUNT(*) FROM Account WHERE accountId = @id";
                    command.Parameters.AddWithValue("@id", number);

                }
                else
                {
                    command.CommandText = "SELECT COUNT(*) FROM Account WHERE username = @username";
                    command.Parameters.AddWithValue("@username", ten);
                }    


                var count = (int)command.ExecuteScalar();

                return count == 0;
            }
        }

        public static void Register(SqlConnection connection, int number)
        {
            // Chèn giá trị mới vào cột Id, username, password bằng các tham số truyền vào như
            // @id, @tk, @mk
            using (var command = new SqlCommand("insert into Account (accountId, username, password, logintime, reporttime, accessright) " +
                "values (@id, @tk, @mk, @lgt, @rt, @ar)", connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@id", number);
                    
                    //kiểm tra xem username có trùng không
                    var userten = command.Parameters.AddWithValue("@tk", Check_Name(connection));

                    Console.Write("Password: ");
                    command.Parameters.AddWithValue("@mk", Console.ReadLine());
                    command.Parameters.AddWithValue("@lgt", 1);
                    command.Parameters.AddWithValue("@rt", 0);
                    command.Parameters.AddWithValue("@ar", false);

                    command.ExecuteNonQuery();
                }
                catch (Exception loi)
                {
                    Console.WriteLine("Error Register: {0}", loi.Message);
                }

            }
        }

        public static string Check_Name(SqlConnection connection)
        {
            // Function này dùng nhằm kiểm tra xem username có bị trùng không, nếu trùng thì bắt nhập lại
            string num = " ";
            while(true)
            {
                Console.Write("Username: ");
                num = Console.ReadLine();
                if (IsEmptyNumber_Namesake(connection, 0, num))
                {
                    return num;
                }  
                else
                {
                    Console.WriteLine("The name you entered already exists. Please try the other name.");
                }    
            }    
        }
    }
}
