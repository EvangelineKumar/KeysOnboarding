using KeysOnboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeysOnboarding.ViewModel
{
    public class ProductViewModel
    {
        public Product CreateProduct { get; set; }
        public List<Product> Products { get; set; }
    }
}