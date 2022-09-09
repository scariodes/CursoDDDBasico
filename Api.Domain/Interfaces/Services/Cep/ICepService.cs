using Api.Domain.Dtos.Cep;

namespace Api.Domain.Interfaces.Services.Cep
{
    public interface ICepService
    {
        public Task<CepDto> Get(Guid id);
        public Task<CepDto> Get(string cep);
        public Task<CepDtoCreateResult> Post(CepDtoCreate cep);
        public Task<CepDtoUpdateResult> Put(CepDtoUpdate cep);
        public Task<bool> Delete(Guid id);
    }
}