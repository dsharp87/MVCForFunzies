using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MVCforFunzies.Models
{
    public class ProductViewModel: BaseEntity
    {

        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters long")]
        [RegularExpression(@"^[a-zA-Z\s\d]+$", ErrorMessage = "Name can only contain letters, numbers, and spacess")]
        [Display(Name = "Product Name")]
        public string name { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Must be at least 10 characters long")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Description can only contain letters, numbers, and spacess")]
        [Display(Name = "Item Description")]
        public string description { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        public string itemUrl {get; set;}

        [Required]
        [Range(0.01,9999.99, ErrorMessage = "Value price must be between 0.01 and 9999.99")]
        [ValidPriceCheck(ErrorMessage = "Must be a valid price in the form of XX.XX")]
        public float price { get; set; }


        public class ValidPriceCheck : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                float num;
                string valToTry = value.ToString();
                bool isValid = float.TryParse(valToTry , 
                NumberStyles.Currency,
                CultureInfo.GetCultureInfo("en-US"), // cached
                out num);
                return isValid;
            }
        }
    }
}