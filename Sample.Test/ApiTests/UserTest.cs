using Sample.Application.Commands;
using Sample.Test.Infraestructure;
using System.Text;
using System.Text.Json;

namespace Sample.Test.ApiTests
{
    public class UserTest(GenericWebApplicationFactory<Program> factory) : IClassFixture<GenericWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Crear_Usuarios_DeberiaRetornar200()
        {
            var id = Guid.Parse("44197386-3893-4505-869d-04ea2187d293");
            CreateUserCommand userCommand = new() { Id = id, Name = "Oscar Test", Email = "Test@hotmail.com", Number = "3212246801" };

            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(userCommand), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/User/Create", httpContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task Obtener_Usuarios_DeberiaRetornar200()
        {
            var response = await _client.GetAsync("/api/User/GetAll");

            response.EnsureSuccessStatusCode(); 

            var content = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task Obtener_UsuariosPorId_DeberiaRetornar200()
        {
            var id = Guid.Parse("44197386-3893-4505-869d-04ea2187d293");

            var getResponse = await _client.GetAsync($"/api/User/GetById?query={id}");

            getResponse.EnsureSuccessStatusCode();

            var content = await getResponse.Content.ReadAsStringAsync();

            Assert.NotEmpty(content);
        }
    }
}
