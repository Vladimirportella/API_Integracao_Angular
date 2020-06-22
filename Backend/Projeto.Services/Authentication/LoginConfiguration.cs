using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Authentication
{
    public class LoginConfiguration
    {
        public static string Audience = "COTI Informática";
        public static string Issuer = "http://cotiinformatica.com.br";
        public static int Seconds = 30000;
    }
}
