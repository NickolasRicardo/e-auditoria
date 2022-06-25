using Newtonsoft.Json;

namespace SistemaLocacao.Models.RequestModels
{
    public class RequestModelCreateClienteDTO
    {
        public RequestModelCreateClienteDTO(string nome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
        }

        [JsonProperty("Nome")]
        public string Nome { get; set; }

        [JsonProperty("Cpf")]
        public string Cpf { get; set; }

        [JsonProperty("DataNascimento")]
        public DateTime DataNascimento { get; set; }
    }
}
