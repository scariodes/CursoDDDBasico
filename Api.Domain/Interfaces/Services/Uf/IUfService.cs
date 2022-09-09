using Api.Domain.Dtos.Uf;

namespace Api.Domain.Interfaces.Services.Uf
{
    public interface IUfService
    {
        public Task<UfDto> Get(Guid id);
        public Task<IEnumerable<UfDto>> GetAll();
    }
}