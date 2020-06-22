using Projeto.Cryptography.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Projeto.Cryptography.Services
{
    public class MD5Service : IMD5Service
    {
        public string Encriptar(string valor)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(valor));

            var result = string.Empty;

            foreach (var item in hash)
            {
                result += item.ToString("x2");
            }

            return result;
        }
    }
}
