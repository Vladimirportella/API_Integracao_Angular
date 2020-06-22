using Dapper;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Projeto.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string connectionString;

        public UsuarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Alterar(Usuario obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("SP_AlterarUsuario",
                       new
                       {
                           obj.IdUsuario,
                           obj.Nome,
                           obj.Email,
                           obj.Senha,
                           obj.DataCriacao
                       }, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Usuario> Consultar()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>("SP_ConsultarUsuarios",
                       commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public Usuario Consultar(string email)
        {
            var query = "select * from Usuario where Email = @Email";

            using (var connection = new SqlConnection(connectionString)) 
            {
                return connection.Query<Usuario>(query, new { Email = email }).FirstOrDefault();
            }
        }

        public Usuario Consultar(string email, string senha)
        {
            var query = "select * from Usuario where Email = @Email and Senha = @Senha";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>(query, new { Email = email, Senha = senha }).FirstOrDefault();
            }
        }

        public void Excluir(Usuario obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("SP_ExcluirUsuario",
                       new
                       {
                           obj.IdUsuario,                       
                       }, commandType: CommandType.StoredProcedure);
            }
        }

        public void Inserir(Usuario obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("SP_InserirUsuario",
                       new 
                       {
                           obj.Nome,
                           obj.Email,
                           obj.Senha,
                           obj.DataCriacao
                       }, commandType: CommandType.StoredProcedure);
            }
        }

        public Usuario ObterPorId(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>("SP_ObterUsuario",
                    new 
                    {
                        IdFuncionario = id
                    },
                       commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
