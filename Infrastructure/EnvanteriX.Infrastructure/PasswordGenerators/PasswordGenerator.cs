using EnvanteriX.Application.Interfaces.PasswordGenerator;
using System.Security.Cryptography;

namespace EnvanteriX.Infrastructure.PasswordGenerators
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string Generate(int length = 16)
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*()-_=+[]{}|;:,.<>?";

            string allChars = upper + lower + digits + special;
            var password = new char[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                // En az bir karakter türünden al
                password[0] = upper[RandomNumber(rng, upper.Length)];
                password[1] = lower[RandomNumber(rng, lower.Length)];
                password[2] = digits[RandomNumber(rng, digits.Length)];
                password[3] = special[RandomNumber(rng, special.Length)];

                // Kalan karakterleri rastgele seç
                for (int i = 4; i < length; i++)
                    password[i] = allChars[RandomNumber(rng, allChars.Length)];

                // Karakterleri karıştır
                return new string(password.OrderBy(x => RandomNumber(rng, int.MaxValue)).ToArray());
            }
        }

        private static int RandomNumber(RandomNumberGenerator rng, int max)
        {
            byte[] buffer = new byte[4];
            rng.GetBytes(buffer);
            int value = BitConverter.ToInt32(buffer, 0) & int.MaxValue;
            return value % max;
        }
    }
}
