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
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private string connectionString;

        public FuncionarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Alterar(Funcionario obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("SP_AlterarFuncionario",
                    new
                    {
                        obj.IdFuncionario,
                        obj.Nome,
                        obj.Salario,
                        obj.DataAdmissao
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Funcionario> Consultar()
        {
            using (var connection = new SqlConnection(connectionString))
            {
               return connection.Query<Funcionario>("SP_ConsultarFuncionarios",
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void Excluir(Funcionario obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("SP_ExcluirFuncionario",
                    new
                    {
                       obj.IdFuncionario
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public void Inserir(Funcionario obj)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("SP_InserirFuncionario",
                    new 
                    {
                        obj.Nome,
                        obj.Salario,
                        obj.DataAdmissao
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public Funcionario ObterPorId(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Funcionario>("SP_ObterFuncionario",
                    new 
                    {
                        IdFuncionario = id
                    },
                     commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
