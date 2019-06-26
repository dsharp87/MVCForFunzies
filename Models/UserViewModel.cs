using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCforFunzies.Models
{
    public class UserViewModel: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set;}

        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 letters long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only contain letters")]
        [Display(Name = "First Name")]
        public string firstName { get; set;}

        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 letters long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name can only contain letters")]
        [Display(Name = "Last Name")]
        public string lastName { get; set;}

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]  
        public string email { get; set;}

        [Required]
        [MinLength(8, ErrorMessage = "Must be at least 8 characters long")]
        [Display(Name = "Password")]  
        public string password { get; set;}

        [Required]
        [Compare("password", ErrorMessage = "Password and Password Confirmation must match")]
        [Display(Name = "Password Confirmation")]   
        public string passwordConfirmation { get; set;}

    }
}