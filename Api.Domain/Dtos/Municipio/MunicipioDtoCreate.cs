using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioDtoCreate
    {
        [Required(ErrorMessage = "Nome do Município é um campo obrigatório")]
        [StringLength(60, ErrorMessage = "Nome do Município deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Código do IBGE inválido.")]
        public int CodIBGE { get; set; }

        [Required(ErrorMessage = "Id da UF é um campo obrigatório")]
        public Guid UfId { get; set; }
    }
}