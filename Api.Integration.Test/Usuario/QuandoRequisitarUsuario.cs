using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Newtonsoft.Json;

namespace Api.Integration.Test.Usuario
{
    public class QuandoRequisitarUsuario : BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }

        public QuandoRequisitarUsuario()
        {

        }

        [Fact(DisplayName = "É possivel realizar o Crud do Usuário.")]
        public async Task E_Possivel_Realizar_Crud_Usuario()
        {
            await AdicionarToken();
            _name = Faker.Name.First();
            _email = Faker.Internet.Email();

            var userDto = new UserDtoCreate()
            {
                Name = _name,
                Email = _email
            };

            // Post
            var response = await PostJsonAsync(userDto, $"{HostApi}users", Client);

            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<UserDtoCreateResult>(postResult);

            Assert.Equal(_name, registroPost.Name);
            Assert.Equal(_email, registroPost.Email);
            Assert.True(registroPost.Id != default(Guid));

            // GetAll
            response = await Client.GetAsync($"{HostApi}users");
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(jsonResult);
            Assert.NotNull(listaFromJson);
            Assert.True(listaFromJson.Count() > 1);
            Assert.True(listaFromJson.Where(x => x.Id == registroPost.Id).Count() == 1);

            // Put
            var updateUserDto = new UserDtoUpdate()
            {
                Id = registroPost.Id,
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            var stringContent = new StringContent(
                        JsonConvert.SerializeObject(updateUserDto),
                        Encoding.UTF8,
                        "application/json"
                        );
            response = await Client.PutAsync($"{HostApi}users", stringContent);
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<UserDtoUpdateResult>(jsonResult);
            
            Assert.Equal(registroPost.Id, registroAtualizado.Id);
            Assert.NotEqual(registroPost.Name, registroAtualizado.Name);
            Assert.NotEqual(registroPost.Email, registroAtualizado.Email);

            // GET Id
            response = await Client.GetAsync($"{HostApi}users/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<UserDtoUpdateResult>(jsonResult);

            Assert.NotNull(response);
            Assert.Equal(registroAtualizado.Id, registroSelecionado.Id);
            Assert.Equal(registroAtualizado.Name, registroSelecionado.Name);
            Assert.Equal(registroAtualizado.Email, registroSelecionado.Email);

            // Delete
            response = await Client.DeleteAsync($"{HostApi}users/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // GET Id depois do DELETE
            response = await Client.GetAsync($"{HostApi}users/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        }
    }
}