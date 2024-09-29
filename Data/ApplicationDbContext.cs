using Microsoft.EntityFrameworkCore;
using CRUDCRUD.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDCRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Compras> compras { get; set; }
        public DbSet<DetalleCompra> detallecompra { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Producto>(Tabla =>
            {
                Tabla.HasKey(columnaP => columnaP.prductID);
                Tabla.Property(columnaP => columnaP.prductID)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();
                
                Tabla.Property(columnaP => columnaP.productName).HasMaxLength(80);
                Tabla.Property(columnaP => columnaP.productPrice).HasColumnType("decimal(18,4)");
                
            });
            modelBuilder.Entity<Cliente>(Tabla =>
            {
                Tabla.HasKey(columnaCL => columnaCL.IDCustomer);
                Tabla.Property(columnaCL => columnaCL.IDCustomer)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                Tabla.Property(columnaCL => columnaCL.customerName).HasMaxLength(80);
                Tabla.Property(columnaCL => columnaCL.Email).HasMaxLength(80);
            });
            modelBuilder.Entity<Compras>(Tabla =>
            {
                Tabla.HasKey(columnaOR => columnaOR.IDOrder);
                Tabla.Property (columnaOR => columnaOR.IDOrder)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Compras>()
            .Property(c => c.totalValue)
            .HasColumnType("decimal(18,4)");  // Define el tipo de columna SQL


            // Relación de uno a muchos entre Cliente y Compra
            modelBuilder.Entity<Compras>()
                .HasOne(c => c.customerName)
                .WithMany(cli => cli.Compras)
                .HasForeignKey(c => c.IDCustomer);

            // Configuración de las relaciones entre Compras y DetalleCompra
            modelBuilder.Entity<DetalleCompra>()
                .HasOne(d => d.compras) // Relación con Compras
                .WithMany(c => c.DetalleCompras) // Una compra tiene muchos detalles
                .HasForeignKey(d => d.IDOrder); // Llave foránea en DetalleCompra apuntando a IDOrder en Compras

            // Configuración de la relación entre DetalleCompra y Producto
            modelBuilder.Entity<DetalleCompra>()
                .HasOne(d => d.Producto) // Relación con Producto
                .WithMany(p => p.DetalleCompras) // Un producto puede estar en muchos detalles
                .HasForeignKey(d => d.prductID); // Llave foránea en DetalleCompra apuntando a prductID en Producto


            //modelBuilder.Entity<DetalleCompra>()
            //    .HasKey(dc => new { dc.prductID, dc.IDOrder });

            //modelBuilder.Entity<DetalleCompra>()
            //    .HasOne(dc => dc.Producto)
            //    .WithMany(p => p.DetalleCompras)  // Asegúrate de que haya una propiedad 'DetallesCompra' en Producto
            //    .HasForeignKey(dc => dc.prductID);

            //modelBuilder.Entity<DetalleCompra>()
            //    .HasOne(dc => dc.compras)
            //    .WithMany(c => c.DetalleCompras) // Asegúrate de que haya una propiedad 'DetallesCompra' en Compra
            //    .HasForeignKey(dc => dc.IDOrder);
            //modelBuilder.Entity<DetalleCompra>()
            //.Property(c => c.Precio)
            //.HasColumnType("decimal(18,4)");  // Define el tipo de columna SQL
            //modelBuilder.Entity<DetalleCompra>()
            //    .Property(col => col.productName).HasMaxLength(80);


            // Configurar la llave primaria compuesta para DetalleCompra
            //modelBuilder.Entity<DetalleCompra>()
            //    .HasKey(dc => new { dc.prductID, dc.IDOrder });

            //// Configuración de las relaciones
            //modelBuilder.Entity<DetalleCompra>()
            //    .HasOne(dc => dc.Producto)  // Relación con Producto
            //    .WithMany(p => p.DetalleCompras)  // Un Producto puede tener muchos DetallesCompra
            //    .HasForeignKey(dc => dc.prductID);  // Llave foránea en DetalleCompra

            //modelBuilder.Entity<DetalleCompra>()
            //    .HasOne(dc => dc.compras)  // Relación con Compra
            //    .WithMany(c => c.DetalleCompras)  // Una Compra puede tener muchos DetallesCompra
            //    .HasForeignKey(dc => dc.compras);  // Llave foránea en DetalleCompra

            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<DetalleCompra>()
            //    .HasKey(dc => new { dc.prductID, dc.IDOrder });

            //modelBuilder.Entity<DetalleCompra>()
            //    .HasOne(dc => dc.Producto)
            //    .WithMany(p => p.DetalleCompras)  // Asegúrate de que haya una propiedad 'DetallesCompra' en Producto
            //    .HasForeignKey(dc => dc.prductID);

            //modelBuilder.Entity<DetalleCompra>()
            //    .HasOne(dc => dc.compras)
            //    .WithMany(c => c.DetalleCompras) // Asegúrate de que haya una propiedad 'DetallesCompra' en Compra
            //    .HasForeignKey(dc => dc.IDOrder);

        }

        // Configuración de la conexión a la base de datos (si es necesario)
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Aquí defines tu cadena de conexión a la base de datos
        //    optionsBuilder.UseSqlServer("Server=your_server;Database=your_db;Trusted_Connection=True;");
        //}

    }
}
