using KeysOnboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeysOnboarding.ViewModel
{
    public class CustomerViewModel
    {
        public Customer CreateCustomer { get; set; }
        public List<Customer> Customers { get; set; }
    }
}