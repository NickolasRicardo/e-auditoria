using Newtonsoft.Json;

namespace SistemaLocacao.Models.ViewModels
{
    public class ViewModelGetLocacaoDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("ClienteID")]
        public int ClienteID { get; set; }

        [JsonProperty("ClienteNome")]
        public string ClienteNome { get; set; }

        [JsonProperty("FilmeID")]
        public int FilmeID { get; set; }

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
