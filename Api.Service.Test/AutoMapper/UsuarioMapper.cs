using System.Collections.Generic;
using System;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;
using System.Linq;

namespace Api.Service.Test.AutoMapper
{
    public class UsuarioMapper : BaseTesteService
    {
        [Fact(DisplayName = "É Possivel Mapear os Modelos")]
        public void E_Possivel_Mapear_os_Modelos()
        {
            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            var listEntity = new List<UserEntity>();
            for (int i = 0; i < 5; i++)
            {
                var item = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                listEntity.Add(item);
            }

            // Model => Entidade
            var entity = Mapper.Map<UserEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Name, model.Name);
            Assert.Equal(entity.Email, model.Email);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            // Entity para dto
            var userDto = Mapper.Map<UserDto>(entity);
            Assert.Equal(entity.Id, userDto.Id);
            Assert.Equal(entity.Name, userDto.Name);
            Assert.Equal(entity.Email, userDto.Email);
            Assert.Equal(entity.CreateAt, userDto.CreateAt);

            var listaDto = Mapper.Map<List<UserDto>>(listEntity);
            Assert.True(listaDto.Count() == listEntity.Count());
            for (int i = 0; i < listaDto.Count(); i++)
            {
                Assert.Equal(listaDto[i].Id, listEntity[i].Id);
                Assert.Equal(listaDto[i].Name, listEntity[i].Name);
                Assert.Equal(listaDto[i].Email, listEntity[i].Email);
                Assert.Equal(listaDto[i].CreateAt, listEntity[i].CreateAt);
            }

            var userDtoCreateResult = Mapper.Map<UserDtoCreateResult>(entity);
            Assert.Equal(userDtoCreateResult.Id, entity.Id);
            Assert.Equal(userDtoCreateResult.Name, entity.Name);
            Assert.Equal(userDtoCreateResult.Email, entity.Email);
            Assert.Equal(userDtoCreateResult.CreateAt, entity.CreateAt);

            var userDtoUpdateResult = Mapper.Map<UserDtoUpdateResult>(entity);
            Assert.Equal(userDtoUpdateResult.Id, entity.Id);
            Assert.Equal(userDtoUpdateResult.Name, entity.Name);
            Assert.Equal(userDtoUpdateResult.Email, entity.Email);
            Assert.Equal(userDtoUpdateResult.UpdateAt, entity.UpdateAt);

            //Dto para Model
            var userModel = Mapper.Map<UserModel>(userDto);
            Assert.Equal(userModel.Id, userDto.Id);
            Assert.Equal(userModel.Name, userDto.Name);
            Assert.Equal(userModel.Email, userDto.Email);
            Assert.Equal(userModel.CreateAt, userDto.CreateAt);

            var userDtoCreate = Mapper.Map<UserDtoCreate>(userModel);
            Assert.Equal(userDtoCreate.Name, userModel.Name);
            Assert.Equal(userDtoCreate.Email, userModel.Email);

            var userDtoUpdate = Mapper.Map<UserDtoUpdate>(userModel);
            Assert.Equal(userDtoUpdate.Id, userModel.Id);
            Assert.Equal(userDtoUpdate.Name, userModel.Name);
            Assert.Equal(userDtoUpdate.Email, userModel.Email);       
        }
    }
}