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
    public class Retorno_BadRequest
    {
        private UsersController _controller;

        [Fact(DisplayName = "Não é possivel realizar o Get.")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
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
            _controller.ModelState.AddModelError("Id", "Formato inválido"); /// apenas para gerar erro

            var result = await _controller.GetAll();
            Assert.True(result is BadRequestObjectResult);           
        }
    }
}