
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeysOnboarding.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        //[Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public double Price { get; set; }

        public ICollection<ProductSold> ProductSold { get; set; }
    }
}