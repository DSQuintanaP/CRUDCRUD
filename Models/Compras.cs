using CRUDCRUD.Models;
using CRUDCRUD.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CRUDCRUD.Models
{
    public class Compras
    {
        public int IDOrder { get; set; }

        // Llave foránea para Cliente
        public int IDCustomer { get; set; }

        // Propiedad de navegación a Cliente
        [ForeignKey("IDCustomer")]
        public Cliente customerName { get; set; } = new Cliente();


        //public int IDCustomer { get; set; }
        ////[ForeignKey(nameof(IDCustomer))]
        ////public Cliente Cliente { get; set; }
        //public int prductID { get; set; }
        ////[ForeignKey(nameof(prductID))]
        ////public Producto Producto { get; set; }
        


        public DateOnly fecha { get; set; }
        public int Cantidad { get; set; }
        public decimal totalValue { get; set; }
        public virtual Cliente? IDCustomerNavigation { get; set; }
        public List<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();
    }
}
