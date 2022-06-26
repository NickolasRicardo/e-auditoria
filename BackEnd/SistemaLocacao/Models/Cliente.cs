using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SistemaLocacao.Models
{
    [Table("cliente")]
    public partial class Cliente
    {
        public Cliente()
        {
            Locacoes = new List<Locacao>();
        }

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Column("CPF")]
        [MaxLength(11)]
        public string CPF { get; set; }

        [Column("DataNascimento")]
        public DateTime? DataNascimento { get; set; }

        [NotMapped]
        public virtual ICollection<Locacao> Locacoes { get; set; }
    }
}
