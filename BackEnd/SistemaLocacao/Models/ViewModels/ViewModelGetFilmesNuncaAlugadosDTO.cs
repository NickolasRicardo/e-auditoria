using Newtonsoft.Json;

namespace SistemaLocacao.Models.ViewModels
{
    public class ViewModelGetFilmesNuncaAlugadosDTO
    {
        public ViewModelGetFilmesNuncaAlugadosDTO()
        {
            Filmes = new List<FilmeDTO>();
        }

        [JsonProperty("Quantidade")]
        public int Quantidade { get; set; }

        [JsonProperty("Filmes")]
        public List<FilmeDTO> Filmes { get; set; }
    }

    public class FilmeDTO
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Nome")]
        public string Nome { get; set; }
    }
}
