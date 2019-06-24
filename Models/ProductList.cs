using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCforFunzies.Models
{
    public class ProductList: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductlistId { get; set;}

        public string name { get; set;}

        public int userId { get; set;}
        
        public User User { get; set;}

        public List<ListProduct> ListProduct { get; set; }

        public ProductList()
        {
            ListProduct = new List<ListProduct>();
        }
    }
}