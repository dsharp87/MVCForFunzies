using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCforFunzies.Models
{
    public abstract class BaseEntity {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime dateCreated { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime dateUpdated { get; set; }
    }
}
