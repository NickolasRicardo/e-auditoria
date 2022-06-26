using Newtonsoft.Json;

namespace SistemaLocacao.Models.RequestModels
{
    public class RequestModelDevolverFilmeDTO
    {
        [JsonProperty("data_devolucao")]
        public DateTime DataDevolucao { get; set; }
    }
}
