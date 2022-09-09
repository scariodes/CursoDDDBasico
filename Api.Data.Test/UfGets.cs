using System;
using System.Collections.Generic;
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
    public class UfGets: DbTeste, IClassFixture<DbTeste>
    {
        public ServiceProvider _ServiceProvider;
        public UfGets(DbTeste dbTeste)
        {
            _ServiceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "Gets de UF")]
        [Trait("Gets", "UfEntity")]
        public async Task E_Possivel_Realizar_Gets_UF()
        {
            using (var context = _ServiceProvider.GetService<MyContext>())
            {
                UfImplementation _repositorio = new UfImplementation(context);

                UfEntity _entity = new UfEntity
                {
                    Id = new Guid("E7E416DE-477C-4FA3-A541-B5AF5F35CCF6"),
                    Sigla = "SP",
                    Nome = "São Paulo"
                };

                var _registroExiste = await _repositorio.SelectAsync(_entity.Id);
                Assert.NotNull(_registroExiste);
                Assert.Equal(_entity.Sigla, _registroExiste.Sigla);
                Assert.Equal(_entity.Nome, _registroExiste.Nome);
                Assert.Equal(_entity.Id, _registroExiste.Id); 

                var _todosRegistros  = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() == 27); 
            }
        }
    }
}