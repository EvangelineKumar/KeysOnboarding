using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KeysOnboarding.Models;

namespace KeysOnboarding.ViewModel
{
    public class StoreViewModel
    {
        public Store CreateStore { get; set; }
        public List<Store> Stores { get; set; }
    }
}