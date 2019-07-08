using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCforFunzies.Models
{
    public class Product: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productId { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string itemUrl {get; set;}

        public float price { get; set; }
    }
}