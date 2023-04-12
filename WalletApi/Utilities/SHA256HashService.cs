using System;
using System.Security.Cryptography;
using System.Text;

namespace WalletApi.Utilities
{
    public class SHA256HashService : IHashService
    {
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashedBytes = Convert.FromBase64String(hashedPassword);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] computedHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hashedBytes[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
