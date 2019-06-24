using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCforFunzies.Models
{
    public class User: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set;}

        public string firstName { get; set;}

        public string lastName { get; set;}

        public string email { get; set;}

        public string password { get; set;}

        public List<ProductList> ProductLists { get; set;}

        public User()
        {
            ProductLists = new List<ProductList>();
        }
    }
}