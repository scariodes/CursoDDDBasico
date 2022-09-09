using Api.Domain.Dtos.User;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        public Task<UserDto> Get(Guid id);
        public Task<IEnumerable<UserDto>> GetAll();
        public Task<UserDtoCreateResult> Post(UserDtoCreate user);
        public Task<UserDtoUpdateResult> Put(UserDtoUpdate user);
        public Task<bool> Delete(Guid id);
    }
}