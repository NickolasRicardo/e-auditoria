using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaLocacao.Models
{
    [Table("filme")]
    public class Filme
    {
        public Filme()
        {
            Locacoes = new List<Locacao>();
        }

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Titulo")]
        public string Titulo { get; set; }

        [Column("ClassificacaoIndicativa")]
        public int Classificacao { get; set; }

        [Column("Lancamento")]
        public bool Lancamento { get; set; }

        [NotMapped]
        public virtual ICollection<Locacao> Locacoes { get; set; }
    }
}
