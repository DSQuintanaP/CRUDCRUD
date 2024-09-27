using CRUDCRUD.Models;
using CRUDCRUD.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDCRUD.Models
{
    public class DetalleCompra
    {
        // Llave foránea de Producto y parte de la llave primaria compuesta
        [Key, Column(Order = 0)]
        public int prductID { get; set; }
        public string productName { get; set; } = string.Empty;

        // Llave foránea de Compra y parte de la llave primaria compuesta
        [Key, Column(Order = 1)]
        public int IDOrder { get; set; }

        // Propiedad adicional
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        // Navegación a Producto
        [ForeignKey("prductID")]
        public Producto Producto { get; set; } = new Producto();

        // Navegación a Compra
        [ForeignKey("IDOrder")]
        public Compras compras { get; set; } = new Compras();
        
        ////[ForeignKey(nameof(IDOrder))]
        ////public Compras Compra { get; set; }
        //public int IDOrder { get; set; }
        ////[ForeignKey(nameof(IDCustomer))]
        ////public Cliente Cliente { get; set; }
        ////public int IDCustomer { get; set; }
        ////[ForeignKey(nameof(prductID))]
        ////public Producto Producto { get; set; }
        //public int prductID { get; set; }        
        //public int Cantidad { get; set; }
    }
}
