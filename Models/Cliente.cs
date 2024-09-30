using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using CRUDCRUD.Models;
using CRUDCRUD.Data;
using System.ComponentModel.DataAnnotations;

namespace CRUDCRUD.Models
{
    public class Cliente
    {
        [Key]
        public int IDCustomer { get; set; }
        public string customerName { get; set; } = string.Empty;
        public int numberPhone { get; set; }
        public string Email { get; set; } = string.Empty ;
        public ICollection<Compras> Compras { get; set; } = new List<Compras>();
    }
}
