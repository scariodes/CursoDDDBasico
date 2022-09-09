using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public class UfEntity: BaseEntity
    {
        [Required]
        [StringLength(2, ErrorMessage = "O campo {0} deve conter {1} a {2} caracteres.", MinimumLength =2)]
        public string Sigla { get; set; }

        [Required]
        [MaxLength(45)]
        public string Nome { get; set; }

        public IEnumerable<MunicipioEntity> Municipios { get; set; }        
    }
}