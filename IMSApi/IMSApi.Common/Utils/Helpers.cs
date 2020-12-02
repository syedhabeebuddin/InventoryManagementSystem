using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IMSApi.Common.Utils
{
    public static class Helpers
    {
        public static string GetSHA256Hash(string value)
        {
            using(var hashAlgo = SHA256.Create())
            {
                return GetHash(hashAlgo, value);
            }
        }

        private static string GetHash(HashAlgorithm algorithm,string value)
        {
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            var sb = new StringBuilder();
            for(int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }

            return sb.ToString();
        }
        
    }
}
