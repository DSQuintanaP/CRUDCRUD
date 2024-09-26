using CRUDCRUD.Models;
using CRUDCRUD.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDCRUD.Models
{
    public class Producto
    {
        public int prductID { get; set; }
        public string productName { get; set; } = string.Empty;
        public int productsInStock { get; set; } 
        public ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    }
}
