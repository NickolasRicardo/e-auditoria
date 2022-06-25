using Newtonsoft.Json;

namespace SistemaLocacao.Models.RequestModels
{
    public class RequestModelCreateLocacaoDTO
    {
        [JsonProperty("id_Cliente")]
        public int IdCliente { get; set; }

        [JsonProperty("id_Filme")]
        public int IdFilme { get; set; }

        [JsonProperty("data_locacao")]
        public DateTime DataLocacao { get; set; }
    }
}
