using System;

namespace hashing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // HashPassword with no salt auto generate ramdom salt
            // So each time we HashPassword the hash result is differen !

            string strSalt = BCryptUtil.GenerateSalt();
            string strSalt2 = BCryptUtil.GenerateSalt(10);
            string strSaltRandom = "$2a$10$KL4bRGKuNMC5kNFLj8bjbu";
            string password1 = BCryptUtil.HashPassword("Hello12#", strSaltRandom);

            //bool isValid = BCrypt.Net.BCrypt.Verify("Hello12#", BCryptUtil.HashPassword("Hello12#"));
            //Console.WriteLine(strSalt);
            //Console.WriteLine(strSalt2);
            Console.WriteLine(password1);
            //Console.WriteLine(isValid);

            Console.Read();

            //satl : $2a$10$KL4bRGKuNMC5kNFLj8bjbu

            //hash by salt 1 : $2a$10$KL4bRGKuNMC5kNFLj8bjbu00p4t9szBOhrOy1nbyR6D/Be93UK69y

            //hash by salt 2 : $2a$10$KL4bRGKuNMC5kNFLj8bjbu00p4t9szBOhrOy1nbyR6D/Be93UK69y
        }
    }

    public class BCryptUtil
    {
        public static string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }

        public static string GenerateSalt(int workFactor)
        {
            return BCrypt.Net.BCrypt.GenerateSalt(workFactor);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static string HashPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
    }
}