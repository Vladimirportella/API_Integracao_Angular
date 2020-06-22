using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Data.Entities;
using Projeto.Data.Repositories;
using Projeto.Services.Models;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [EnableCors("DefaultPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(FuncionarioCadastroModel model, 
                                  [FromServices] FuncionarioRepository repository) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var funcionario = new Funcionario();
                    funcionario.Nome = model.Nome;
                    funcionario.Salario = model.Salario;
                    funcionario.DataAdmissao = model.DataAdmissao;

                    repository.Inserir(funcionario);
                    return Ok("Funcionário cadastrado com sucesso.");
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPut]
        public IActionResult Put(FuncionarioEdicaoModel model, 
                                 [FromServices] FuncionarioRepository repository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var funcionario = new Funcionario();
                    funcionario.IdFuncionario = model.IdFuncionario;
                    funcionario.Nome = model.Nome;
                    funcionario.Salario = model.Salario;
                    funcionario.DataAdmissao = model.DataAdmissao;

                    repository.Alterar(funcionario);
                    return Ok("Funcionário atualizado com sucesso.");
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] FuncionarioRepository repository)
        {
            try
            {
                var funcionario = repository.ObterPorId(id);

                if (funcionario != null)
                {
                    repository.Excluir(funcionario);
                    return Ok("Funcionário excluído com sucesso.");
                }
                else
                {
                    return BadRequest("Funcionário não foi encontrado.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll([FromServices] FuncionarioRepository repository)
        {

            try
            {
                var result = repository.Consultar();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id, [FromServices] FuncionarioRepository repository)
        {
            try
            {
                var result = repository.ObterPorId(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}