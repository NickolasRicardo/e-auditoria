using Newtonsoft.Json;

namespace SistemaLocacao.Models.RequestModels
{
    public class RequestModelCreateFilmeDTO
    {
        public RequestModelCreateFilmeDTO(string titulo, string cpf)
        {
            Titulo = titulo;
        }

        [JsonProperty("Titulo")]
        public string Titulo { get; set; }

        [JsonProperty("Lancamento")]
        public bool Lancamento { get; set; }
    }
}
