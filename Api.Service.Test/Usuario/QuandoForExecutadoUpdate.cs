using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecutadoUpdate: UsuarioTestes
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É Possivel Executar o Método Update.")]
        public async Task E_Possivel_Executar_Metodo_Update()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Post(UserDtoCreate)).ReturnsAsync(UserDtoCreateResult);
            _service = _serviceMock.Object;

            var resultCreate = await _service.Post(UserDtoCreate);
            Assert.NotNull(resultCreate);            
            Assert.Equal(NomeUsuario, resultCreate.Name);
            Assert.Equal(EmailUsuario, resultCreate.Email);

            // Testa Update
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Put(UserDtoUpdate)).ReturnsAsync(UserDtoUpdateResult);
            _service = _serviceMock.Object;

            var resultUpdate = await _service.Put(UserDtoUpdate);
            Assert.NotNull(resultUpdate);            
            Assert.Equal(NomeUsuarioAlterado, resultUpdate.Name);
            Assert.Equal(EmailUsuarioAlterado, resultUpdate.Email);
        }
    }
}