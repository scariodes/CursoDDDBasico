using Api.Domain.Dtos;

namespace Api.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        public Task<object> FindByLogin(LoginDto user);
    }
}