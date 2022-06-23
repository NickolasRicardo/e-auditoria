using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaLocacao.Models
{
    [Table("locacao")]
    public class Locacao
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [ForeignKey("Id_Cliente")]
        public Cliente Cliente { get; set; }

        [ForeignKey("Id_Filme")]
        public Filme Filme { get; set; }

        [Column("DataLocacao")]
        public DateTime DataLocacao { get; set; }

        [Column("DataDevolucao")]
        public DateTime DataDevolucao { get; set; }

        
    }
}
