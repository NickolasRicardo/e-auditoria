using Newtonsoft.Json;

namespace SistemaLocacao.Models.ViewModels
{
    public class ViewModelGetClientesAtrasoDTO
    {
        public ViewModelGetClientesAtrasoDTO()
        {
            Data = new List<ClienteDTO>();
        }

        [JsonProperty("Quantidade")]
        public int Quantidade { get; set; }

        [JsonProperty("data")]
        public List<ClienteDTO> Data { get; set; }
    }

    public class ClienteDTO
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Nome")]
        public string Nome { get; set; }
    }
}
