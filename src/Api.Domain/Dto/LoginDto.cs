using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Api.Domain.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email é uma campo obrigatorio.")]
        [EmailAddress(ErrorMessage = "Email invalido.")]
        [StringLength(100, ErrorMessage = "Email deve conter o maximo de {1} caracteres.")]
        public string Email { get; set; }
    }
}
