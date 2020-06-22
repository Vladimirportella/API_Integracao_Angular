using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Cryptography.Contracts
{
    public interface IMD5Service
    {
        string Encriptar(string valor);
    }
}
