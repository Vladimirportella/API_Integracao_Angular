using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Cryptography.Services;
using Projeto.Data.Entities;
using Projeto.Data.Repositories;
using Projeto.Services.Models;

namespace Projeto.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(UsuarioCadastroModel model, 
                                [FromServices] UsuarioRepository repository,
                                [FromServices] MD5Service service) 
        {
            if (ModelState.IsValid)
            {
                if (repository.Consultar(model.Email) != null)
                {
                    return BadRequest("Erro. O email informado já encontra-se cadastrado.");
                }
                else
                {
                    try
                    {
                        var usuario = new Usuario();
                        usuario.Nome = model.Nome;
                        usuario.Email = model.Email;
                        usuario.Senha = service.Encriptar(model.Senha);
                        usuario.DataCriacao = DateTime.Now;

                        repository.Inserir(usuario);
                        return Ok("Usuário cadastrado com sucesso");
                    }
                    catch (Exception e)
                    {
                        return StatusCode(500, e.Message);                       
                    }
                }
            }
            else
            {
                return BadRequest();
            }
                 
        }
    }
}