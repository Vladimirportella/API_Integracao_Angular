using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models
{
    public class UsuarioCadastroModel
    {
        [Required(ErrorMessage ="Por favor, informe o nome do usuário.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage ="Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do usuário.")]
        public string Email { get; set; }

        [MaxLength(12, ErrorMessage ="Por favor, informe no máximo {1} caracteres.")]
        [MinLength(6, ErrorMessage ="Por favor, informe no mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a senha do usuário.")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem.")]
        [Required(ErrorMessage = "Por favor, confirme a senha.")]
        public string SenhaConfirmacao { get; set; }
    }
}
