using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace BanksLab
{
    partial class Program
    {
        public static void Main()
        {
            WriteLine(SystemTime.Now.Invoke());
            SystemTime.SetDateTime(DateTime.Now.AddMonths(3));
            WriteLine(SystemTime.Now.Invoke());
            SystemTime.ResetDateTime();
            WriteLine(SystemTime.Now.Invoke());
        }
    }
}