using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    internal class support
    {
        static int GetNextFilmId()
        {
            if (File.Exists(dataFilePath))
            {
                string[] lines = File.ReadAllLines(dataFilePath, Encoding.Unicode);
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
        static string InputWithPrompt(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Vui lòng không để trống trường này.");
                }
            } while (string.IsNullOrEmpty(input));

            return input;
        }

        static int IntInputWithPrompt(string prompt)
        {
            int result;
            string input;
            bool isValid = false;

            do
            {
                input = InputWithPrompt(prompt);

                isValid = int.TryParse(input, out result);

                if (!isValid)
                {
                    Console.WriteLine("Vui lòng nhập một số nguyên hợp lệ.");
                }
            } while (!isValid);

            return result;
        }

        static double DoubleInputWithPrompt(string prompt)
        {
            double result;
            string input;
            bool isValid = false;

            do
            {
                input = InputWithPrompt(prompt);

                isValid = double.TryParse(input, out result);

                if (!isValid)
                {
                    Console.WriteLine("Vui lòng nhập một số thực hợp lệ.");
                }
            } while (!isValid);

            return result;
        }
    }
}
