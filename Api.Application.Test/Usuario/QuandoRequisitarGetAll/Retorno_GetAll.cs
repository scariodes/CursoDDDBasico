using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Usuario.QuandoRequisitarGetAll
{
    public class Retorno_Get
    {
        private UsersController _controller;

        [Fact(DisplayName = "É possivel realizar o GetAll.")]
        public async Task E_Possivel_Invocar_a_Controller_GetAll()
        {
            var serviceMock = new Mock<IUserService>();

            List<UserDto> userDtoList = new List<UserDto>();
            for (int i = 0; i < 5; i++)
            {
                var objeto = new UserDto
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow
                };
                userDtoList.Add(objeto);
            }

            serviceMock.Setup(m => m.GetAll()).ReturnsAsync(userDtoList);

            _controller = new UsersController(serviceMock.Object);

            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as List<UserDto>;
            Assert.NotNull(resultValue);
            Assert.True(resultValue.Count == userDtoList.Count);

            for (int i = 0; i < resultValue.Count(); i++)
            {
                var dadosEntrada = userDtoList[i];
                var dadosSaida = resultValue[i];

                Assert.Equal(dadosEntrada.Id, dadosSaida.Id);
                Assert.Equal(dadosEntrada.Name, dadosSaida.Name);
                Assert.Equal(dadosEntrada.Email, dadosSaida.Email);
                Assert.Equal(dadosEntrada.CreateAt, dadosSaida.CreateAt);
            }            
        }
    }
}