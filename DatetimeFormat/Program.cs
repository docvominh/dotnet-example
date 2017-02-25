using System;
using System.Globalization;

namespace DatetimeFormat
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CultureInfo us = new CultureInfo("en-US");
            string format = "MMM, dd yyyy";
            Console.WriteLine(DateTime.Now.ToString());
            Console.WriteLine(DateTime.ParseExact(DateTime.Now.ToString(format), format, us).ToString(format));

            Console.Read();
        }
    }
}