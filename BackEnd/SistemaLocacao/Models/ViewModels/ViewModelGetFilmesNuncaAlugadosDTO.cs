using Newtonsoft.Json;

namespace SistemaLocacao.Models.ViewModels
{
    public class ViewModelGetFilmesNuncaAlugadosDTO
    {
        public ViewModelGetFilmesNuncaAlugadosDTO()
        {
            Data = new List<FilmeDTO>();
        }

        [JsonProperty("Quantidade")]
        public int Quantidade { get; set; }

        [JsonProperty("Data")]
        public List<FilmeDTO> Data { get; set; }
    }

    public class FilmeDTO
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Nome")]
        public string Nome { get; set; }
    }
}
