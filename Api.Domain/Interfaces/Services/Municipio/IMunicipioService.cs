using Api.Domain.Dtos.Municipio;

namespace Api.Domain.Interfaces.Services.Municipio
{
    public interface IMunicipioService
    {
        public Task<MunicipioDto> Get(Guid id);
        public Task<MunicipioDtoCompleto> GetCompleteById(Guid id);
        public Task<MunicipioDtoCompleto> GetCompleteByIBGE(int codIBGE);
        public Task<IEnumerable<MunicipioDto>> GetAll();
        public Task<MunicipioDtoCreateResult> Post(MunicipioDtoCreate municipio);
        public Task<MunicipioDtoUpdateResult> Put(MunicipioDtoUpdate municipio);
        public Task<bool> Delete(Guid id);
    }
}