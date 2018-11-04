using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeysOnboarding.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Customer Name")]
        [Required(ErrorMessage = "Customer Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Customer Address is required")]
        [StringLength(300)]
        public string Address { get; set; }

        public ICollection<ProductSold> ProductSold { get; set; }
    }
}