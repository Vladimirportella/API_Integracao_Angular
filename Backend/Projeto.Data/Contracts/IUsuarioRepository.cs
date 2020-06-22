using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario Consultar(string email);
        Usuario Consultar(string email, string senha);
    }
}
