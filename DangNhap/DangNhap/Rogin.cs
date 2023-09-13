using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DangNhap
{
    internal class Rogin
    {
        public static void First_Move(SqlConnection connection, ref char accessauthority, int choose1)
        {
            // Giao diện đăng nhập
            switch (choose1)
            {
                case 1://Login
                    accessauthority = Login(connection);
                    Console.WriteLine($"Authority is {accessauthority}");
                    break;

                case 2: //Register
                    RegisterInterface(connection);
                    break;

                case 3://Exit
                    Console.WriteLine("Thanks for using our application!");
                    return;
            }
        }
        public static char Login(SqlConnection connection)
        {
            char accessauthority = ' ';
            string query = "SELECT username, password, accessright FROM Account";
            using (var command = new SqlCommand(query, connection))
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
                        // sqlreader.Read() trả về false -> Tức là đi qua dòng data cuối.
                        while (sqlreader.Read())
                        {
                            var tk2 = sqlreader.GetString(0);
                            var ps2 = sqlreader.GetString(1);
                            var ar = sqlreader.GetBoolean(2);

                            if (tk1 == tk2 && ps1 == ps2)
                            {
                                // Để xác định vai trò Admin hay User và dừng vòng lặp trong khi
                                // đã kiểm tra đúng TK và MK
                                accessauthority = ar ? 'A' : 'U';
                            }


                        }
                        Console.Clear();
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
        public static void RegisterInterface(SqlConnection connection)
        {
            int biggest_num = Biggest_id(connection);
            try
            {
                for (int i = 100; i <= biggest_num; i++)
                {
                    // Nếu có accountId vào trống ví dụ như ta có trong bảng accountId 100, 101, 104, 105
                    // Thì for sẽ lặp qua từ 100 -> 105, nếu đến giá trị 102 mà nó không tìm thấy
                    // trong table thì nó sẽ tạo một accountId mới trong bảng với giá trị là 102 rồi chèn các data vào
                    if (IsEmptyNumber_Namesake(connection, i))
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
        }
        public static void Register(SqlConnection connection, int number)
        {
            // Chèn giá trị mới vào các cột bằng các tham số truyền vào như
            // @id, @tk, @mk,...
            string query = "insert into Account (accountId, username, password, logintime, reporttime, commenttime, accessright) " +
                "values (@id, @tk, @mk, @lgt, @rt, @ct, @ar)";

            using (var command = new SqlCommand(query, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@id", number);

                    //kiểm tra xem username có trùng không
                    var userten = command.Parameters.AddWithValue("@tk",
                        Nhap_Vao<string>("Username: ", s => string.IsNullOrEmpty(s), s => IsEmptyNumber_Namesake(connection, 0, s)));

                    Console.Write("Password: ");
                    command.Parameters.AddWithValue("@mk", Console.ReadLine());
                    command.Parameters.AddWithValue("@lgt", 1);
                    command.Parameters.AddWithValue("@rt", 0);
                    command.Parameters.AddWithValue("@ct", 0);
                    command.Parameters.AddWithValue("@ar", false);

                    command.ExecuteNonQuery();
                }
                catch (Exception loi)
                {
                    Console.WriteLine("Error Register: {0}", loi.Message);
                }

            }
        }
        // T Nhap_Vao <T> Nhap_Vao<T>: T phía trước: giá trị generic cho phép làm việc với kiểu data xác định ngay thời điểm biên dịch
        // T phía sau: giá trị trả về của tham số

        // Func<T, bool> dieukien : Hàm delegate trả về giá trị bool.

        // bool> Name_Same = null : tham số có giá trị mặc định là null, có nghĩa rằng ko cần kiểm tra điều kiện trong tham số
        // này nếu không muốn.

        // T result = (T)Convert.ChangeType(input, typeof(T)); -> Ép kiểu hai lần do C.C lần đầu trả về kiểu object.
        // bên trong đổi giá trị kiểu data của input sang typeof(T)
        public static T Nhap_Vao<T>(string text, Func<T, bool> dieukien, Func<T, bool> Name_Same = null)
        {
            T result;
            bool convert1 = false;
            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine();
                try
                {
                    try
                    {
                        // Thử check kiểu nếu lỗi, không thể chuyển thì sẽ mặc định false.
                        // result = default(T); : đảm bảo luôn có một giá trị hợp lệ khi ép kiểu ko thành công
                        result = (T)Convert.ChangeType(input, typeof(T));
                        convert1 = true;
                    }
                    catch 
                    {
                        result = default(T);
                    }
                    // Kiểm tra TH nếu nhập vào username: KT phần Func dieukien -> Nếu trống thì nhảy vào.
                    // Bên trong kiểm tra nếu tham số 3 không để trống và Kiểm tra điều kiện trong Func Name_Same
                    // liệu tên có trùng không. Nếu có suy ra else không thì trả về giá trị result.

                    // Kiểm tra TH nếu nhập vào số: KT phần Func dieukien -> Nếu thỏa trong khoảng đó thì nhảy vào
                    // Kiểm tra liệu tham số 3 có null không nếu null nhảy vào không thì xét tiếp
                    if (convert1 && dieukien(result))
                    {
                        if (Name_Same == null || (Name_Same != null && Name_Same(result)))
                        {
                            Console.Clear();
                            return result;
                        }
                        else Console.WriteLine("Giá trị bạn nhập đã tồn tại. Vui lòng thử tên khác.");

                    }
                    else 
                    {
                        Console.Clear();
                        Console.WriteLine("Giá trị không hợp lệ. Vui lòng thử lại."); 
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error Nhap_Vao: {e}");
                }
            }
        }
        public static int Biggest_id(SqlConnection connection)
        {
            int num = 0;
            // Truy vấn chỉ muốn lấy ra 1 dòng kết quả  (TOP 1) từ cột accountId từ bảng Account theo thứ tự accountId giảm dần
            // Lấy giá trị đầu tiên sau khi sắp xếp
            string query = "SELECT TOP 1 accountId FROM Account ORDER BY accountId DESC";
            using (var command = new SqlCommand(query, connection))
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
        public static bool IsEmptyNumber_Namesake(SqlConnection connection, int number, string ten = null)
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
    }
}
