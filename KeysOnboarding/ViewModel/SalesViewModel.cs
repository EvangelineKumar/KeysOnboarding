using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KeysOnboarding.Models;

namespace KeysOnboarding.ViewModel
{
    public class SalesViewModel
    {
        public ProductSold ProductSold { get; set; }
        public List<ProductSold> SalesList { get; set; }

        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
        public List<Store> Stores { get; set; }
    }
}