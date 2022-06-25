using Newtonsoft.Json;

namespace SistemaLocacao.Models.ViewModels
{
    public class ViewModelGetLocacaoDTO
    {
        [JsonProperty("ClienteNome")]
        public string ClienteNome { get; set; }

        [JsonProperty("FilmeNome")]
        public string FilmeNome { get; set; }

        [JsonProperty("DataLocacao")]
        public string DataLocacao { get; set; }

        [JsonProperty("DataDevolucao")]
        public string DataDevolucao { get; set; }

        [JsonProperty("StatusDevolucao")]
        public string StatusDevolucao { get; set; }
    }
}
