using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Api.Domain.Dto.User
{
    public class UserDtoUpdate
    {
        [Key]
        [Required(ErrorMessage = "Id é um campo obrigatorio.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é uma campo obrigatorio.")]
        [StringLength(60, ErrorMessage = "Nome deve conter o maximo de {1} caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é uma campo obrigatorio.")]
        [EmailAddress(ErrorMessage = "Email invalido.")]
        [StringLength(100, ErrorMessage = "Email deve conter o maximo de {1} caracteres.")]
        public string Email { get; set; }
    }
}
