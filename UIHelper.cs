using System;

namespace MyProgram
{
    public static class UIHelper
    {
        public static void giaodien()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   Ứng dụng Quản lý Đánh giá              ║");
            Console.WriteLine("╟──────────────────────────────────────────────────────────╢");
            Console.WriteLine("║ 1. Hiển thị danh sách cmtid                              ║");
            Console.WriteLine("║ 2. Xoá comment theo Account ID                           ║");
            Console.WriteLine("║ 3. Thoát                                                 ║");
            Console.WriteLine("╟──────────────────────────────────────────────────────────╢");
            Console.WriteLine("║                  Hãy chọn một tùy chọn (1-3):            ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        }
    }
}
