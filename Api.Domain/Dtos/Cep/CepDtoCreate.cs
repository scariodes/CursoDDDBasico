using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Cep
{
    public class CepDtoCreate
    {
        [Required(ErrorMessage = "CEP é um campo obrigatório")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Logradouro é um campo obrigatório")]
        [MaxLength(60)]
        public string Logradouro { get; set; }

        [MaxLength(10)]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Município é um campo obrigatório")]
        public Guid MunicipioId { get; set; }        
    }
}