using System;

namespace DesignPatternInCSharp.Generic
{
    internal class GenericMethod
    {
        private static int ConvertToInt(object value, object defaultValue)
        {
            if (value == null || value == DBNull.Value)
            {
                return Convert.ToInt32(defaultValue);
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

        private static DateTime ConvertToDateTime(object value, object defaultValue)
        {
            if (value == null || value.Equals(DBNull.Value))
            {
                return Convert.ToDateTime(defaultValue);
            }
            else
            {
                return Convert.ToDateTime(value);
            }
        }

        // Combine to one generic method
        private static T ConvertTo<T>(object value, object defaultValue)
        {
            if (value == null || value.Equals(DBNull.Value))
            {
                return (T)Convert.ChangeType(defaultValue, typeof(T));
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine(ConvertToInt("123", default(int))+1);
            Console.WriteLine(ConvertToDateTime("12/30/2015", default(DateTime)));
            Console.WriteLine(ConvertTo<int>("999", default(int))+1);
            Console.WriteLine(ConvertTo<DateTime>("11/19/1111", default(DateTime)));

            Console.Read();
        }
    }
}