using Newtonsoft.Json;

namespace SistemaLocacao.Models.RequestModels
{
    public class RequestModelUpdateLocacaoDTO
    {
        [JsonProperty("id_Cliente")]
        public int IdCliente { get; set; }

        [JsonProperty("id_Filme")]
        public int IdFilme { get; set; }

        [JsonProperty("data_locacao")]
        public DateTime DataLocacao { get; set; }

        [JsonProperty("data_devolucao")]
        public DateTime DataDevolucao { get; set; }
    }
}
