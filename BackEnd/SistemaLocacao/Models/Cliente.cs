using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaLocacao.Models
{
    [Table("cliente")]
    public partial class Cliente
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        [MaxLength(200)]
        public string? Nome { get; set; }

        [Column("CPF")]
        [MaxLength(11)]
        public string? CPF { get; set; }

        [Column("DataNascimento")]
        public DateTime? DataNascimento { get; set; }

      
    }
}
