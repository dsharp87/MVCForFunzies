using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCforFunzies.Models
{
    public class LogUserViewModel: BaseEntity
    {

        [EmailAddress]
        [Display(Name = "Email")]  
        public string email { get; set;}

        [Display(Name = "Password")]  
        public string password { get; set;}

    }
}