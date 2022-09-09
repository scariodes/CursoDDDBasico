
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "{0} é um campo obrigatório")]
        [EmailAddress(ErrorMessage = "{0} inválido")]
        [StringLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Email { get; set; }
    }
}