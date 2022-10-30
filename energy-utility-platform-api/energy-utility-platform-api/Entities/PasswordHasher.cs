using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace energy_utility_platform_api.Entities
{
    public static class PasswordHasher
    {
        public static byte[] salt = Encoding.ASCII.GetBytes("f1nd1ngn3m0");
        public static string HashPassword(string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password!,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));
        }
    }
}
