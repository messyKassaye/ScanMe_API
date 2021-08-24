using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Helpers
{
    public class EncryptionHandler
    {
        public static string HashPassword(string data)
        {
            return BCrypt.Net.BCrypt.HashPassword(data);
        }

        public static bool VerifyPassword(string password,string HashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, HashedPassword);
        }
    }
}
