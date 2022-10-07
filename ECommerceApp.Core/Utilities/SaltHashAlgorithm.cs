using System.Security.Cryptography;
using System.Text;
namespace ECommerceApp.Core.Utilities
{
    public class SaltHashAlgorithm
    {
        private const int MINSALTSIZE = 12;
        private const int MAXSALTSIZE = 16;

        public static byte[] GenerateSalt()
        {
            var random = new Random();
            int saltSize = random.Next(MINSALTSIZE, MAXSALTSIZE);
            byte[] saltBytes = new byte[saltSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetNonZeroBytes(saltBytes);
            return saltBytes;
        }
        public static byte[] GenerateHash(string password, byte[] salt)
        {
            string base64Salt = Convert.ToBase64String(salt);
            byte[] unhashedBytes = Encoding.UTF8.GetBytes(String.Concat(base64Salt, password));
            using var sha512 = SHA512.Create();
            return sha512.ComputeHash(unhashedBytes);
        }
        public static bool CompareHash(string attemptedPassword, byte[] hash, byte[] salt)
        {
            string base64Hash = Convert.ToBase64String(hash);
            string base64AttemptedHash = Convert.ToBase64String(GenerateHash(attemptedPassword, salt));
            return base64Hash == base64AttemptedHash;
        }
    }
}