using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Loginsan1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            bool gate_next = false;
            bool gate_end = false;
            bool accessauthority = false;
            string accountnotify = " ";
            string filepath = @"C:\Users\84967\OneDrive\Máy tính\Truyvan.txt";

            string title = Logo.Logo_App();
            Logo.Run_Intro();

            while (!gate_next && !gate_end) 
            {
                First_Interface.First_I(filepath, title, ref accountnotify,ref accessauthority, ref gate_next, ref gate_end);
            }
            gate_next = false;

            if(accessauthority)
            {
                // Phần làm admin
                while (!gate_next && !gate_end)
                {
                    Admin.Bin(filepath, accountnotify, ref gate_next, ref gate_end);
                }
            }    
            else
            {
                // Phần làm User
            }    
        }
    }

}
