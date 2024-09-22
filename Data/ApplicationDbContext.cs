using Microsoft.EntityFrameworkCore;
using CRUDCRUD.Models;

namespace CRUDCRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Producto>(Tabla =>
            {
                Tabla.HasKey(columna => columna.prductID);
                Tabla.Property(columna => columna.prductID)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();
                
                Tabla.Property(columna=> columna.productName).HasMaxLength(80);
            });
        }

    }
}
