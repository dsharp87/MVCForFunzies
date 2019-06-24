using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCforFunzies.Models
{
    public class ListProduct: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int listProductId { get; set;}

        public int productId { get; set;}

        public Product product { get; set;}

        public int productListId { get; set;}

        public ProductList ProductList { get; set;}
    }
}