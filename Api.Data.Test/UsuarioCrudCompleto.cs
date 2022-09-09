using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Teste;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "Crud de Usuario")]
        [Trait("Crud", "UserEntity")]
        public async Task E_Possivel_Realizar_CRUD_Usuario()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repositorio = new UserImplementation(context);

                var user = new UserEntity()
                {
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                };
                var _registroCriado = await _repositorio.InsertAsync(user);
                Assert.NotNull(_registroCriado);
                Assert.Equal(user.Name, _registroCriado.Name);
                Assert.Equal(user.Email, _registroCriado.Email);
                Assert.False(_registroCriado.Id == Guid.Empty);

                user.Name = Faker.Name.First();

                var _registroAtualizado = await _repositorio.UpdateAsync(user);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(user.Name, _registroAtualizado.Name);
                Assert.Equal(user.Email, _registroAtualizado.Email);

                var registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(registroSelecionado);
                Assert.Equal(_registroAtualizado.Name, registroSelecionado.Name);
                Assert.Equal(_registroAtualizado.Email, registroSelecionado.Email);

                var _todosRegistros = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 0);
                Assert.True(_todosRegistros.Count() > 1);

                var _removeu = await _repositorio.DeleteAsync(registroSelecionado.Id);
                Assert.True(_removeu);

                var _usuarioPadrao = await _repositorio.FindByLogin("scariodes1895@gmail.com");
                Assert.NotNull(_usuarioPadrao);
                Assert.Equal("Adminitrador", _usuarioPadrao.Name);
                Assert.Equal("scariodes1895@gmail.com", _usuarioPadrao.Email);
            }
        }
    }
}