
namespace Api.Integration.Test
{
    public class TesteLogin: BaseIntegration
    {
        [Fact(DisplayName = "Teste do Token")]
        public async Task TesteDoToken()
        {
            await AdicionarToken();
        }
    }
}