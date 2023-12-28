using Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, string md5password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {

                var hashceheck = Encrypt.MD5Encrypt(password);
                var hash = md5password;
                for (int i = 0; i < hashceheck.Length; i++)
                {
                    if (hashceheck[i] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            if (password == md5password)
            {
                return false;
            }

            return true;
        }
    }
}
