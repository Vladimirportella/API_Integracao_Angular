using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models
{
    public class FuncionarioCadastroModel
    {
        [MaxLength(150, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do funcionário.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o salário do funcionário.")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de admissão.")]
        public DateTime DataAdmissao { get; set; }
    }
}
