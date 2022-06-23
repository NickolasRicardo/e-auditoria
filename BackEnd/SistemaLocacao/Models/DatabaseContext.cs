using Microsoft.EntityFrameworkCore;
using SistemaLocacao.Models;

namespace SistemaLocacao
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public virtual DbSet<Cliente> Cliente { get; set; }

        public virtual DbSet<Filme> Filme { get; set; }

        public virtual DbSet<Locacao> Locacao { get; set; }

       
    }



}
