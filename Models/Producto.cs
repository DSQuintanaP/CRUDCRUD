using CRUDCRUD.Models;
using CRUDCRUD.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;


namespace CRUDCRUD.Models
{
    public class Producto
    {
        public int prductID { get; set; }
        public string productName { get; set; } = string.Empty;
        public int productsInStock { get; set; } 
        public decimal productPrice { get; set; }
        public ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();
        
        // Método para acceder a DetalleCompras por índice
        public DetalleCompra GetDetalleCompra(int index)
        {
            var list = DetalleCompras as List<DetalleCompra>;
            return list?[index]; // Maneja caso donde la colección no es una lista
        }

    }
}
