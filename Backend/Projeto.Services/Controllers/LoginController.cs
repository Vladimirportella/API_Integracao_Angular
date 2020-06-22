using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Projeto.Cryptography.Services;
using Projeto.Data.Repositories;
using Projeto.Services.Authentication;
using Projeto.Services.Models;
using LoginModel = Projeto.Services.Models.LoginModel;

namespace Projeto.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(LoginModel model, [FromServices] UsuarioRepository repository,
                                                    [FromServices] MD5Service service,
                                                    [FromServices] TokenConfiguration tokenConfiguration) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = repository.Consultar(model.Email, service.Encriptar(model.Senha));

                    if (usuario != null)
                        {
                            //GERANDO O TOKEN!!
                            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(usuario.Email, "Login"),
                                new[] {
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                                new Claim(JwtRegisteredClaimNames.UniqueName, model.Email)
                                });

                            DateTime dataCriacao = DateTime.Now;
                            DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(LoginConfiguration.Seconds);

                            var handler = new JwtSecurityTokenHandler();
                            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                            {
                                Issuer = LoginConfiguration.Issuer,
                                Audience = LoginConfiguration.Audience,
                                SigningCredentials = tokenConfiguration.SigningCredentials,
                                Subject = identity,
                                NotBefore = dataCriacao,
                                Expires = dataExpiracao
                            });

                            var token = handler.WriteToken(securityToken);
                            return Ok(token); //retornando sucesso e enviando o TOKEN!
                        }
                    else
                    {
                        return Unauthorized();
                    }
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
    }
}