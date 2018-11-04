using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KeysOnboarding.Models;
using KeysOnboarding.ViewModel;

namespace KeysOnboarding.Controllers
{
    public class CustomersController : Controller
    {
        private KeysContext db = new KeysContext();

        // GET: Customers
        public ActionResult Index()
        {
            //return View(db.Customers.ToList());
            CustomerViewModel customer = new CustomerViewModel()
            {
                Customers = db.Customers.ToList()
            };
            return View(customer);
        }

        /// <summary>
        /// Connects to the database
        /// add and update customers
        /// </summary>
        /// <param name="cust">
        /// contains all the data for saving
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(CustomerViewModel cust)
        {
            var c_Name = cust.CreateCustomer.Name;
            Customer customer = db.Customers.Where(c => c.Name == c_Name).SingleOrDefault();

            if (customer != null && customer.Id != cust.CreateCustomer.Id)
            {
                return Json(false);
            }

            if (cust.CreateCustomer.Id > 0)
            {
                Customer cus = db.Customers.Where(x => x.Id == cust.CreateCustomer.Id).SingleOrDefault();
                cus.Name = cust.CreateCustomer.Name;
                cus.Address = cust.CreateCustomer.Address;
                db.SaveChanges();
            }
            else
            {
                Customer cu = new Customer()
                {
                    Name = cust.CreateCustomer.Name,
                    Address = cust.CreateCustomer.Address
                };
                db.Customers.Add(cu);
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }

        /// <summary>
        /// deletes the customer from the database
        /// </summary>
        /// <param name="customerId">
        /// contains the id of the customer to be deleted
        /// </param>
        /// <returns></returns>
        public ActionResult DeleteCustomer(int customerId)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                Customer cust = db.Customers.SingleOrDefault(x => x.Id == customerId);
                if (cust != null)
                {
                    db.Customers.Remove(cust);
                    db.SaveChanges();
                    result = true;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// shows the details of a particular customer
        /// </summary>
        /// <param name="customerId">
        /// contains the id of the customer
        /// </param>
        /// <returns></returns>
        public ActionResult ShowCustomer(int customerId)
        {
            //Product prod = db.Products.SingleOrDefault(x => x.Id == productId);
            Customer cust = db.Customers.Where(x => x.Id == customerId).SingleOrDefault();

            CustomerViewModel model = new CustomerViewModel()
            {
                CreateCustomer = cust
            };
            return PartialView("CustomerPartialShow", model);

            //return View();
        }

        /// <summary>
        /// gets the customer from database
        /// and shows the customer details in partial view
        /// </summary>
        /// <param name="customerId">
        /// contains the id of the customer to be updated
        /// </param>
        /// <returns></returns>
        public ActionResult EditCustomer(int customerId)
        {

            Customer cust = db.Customers.Where(p =>p.Id == customerId).SingleOrDefault();
            CustomerViewModel model = new CustomerViewModel()
            {
                CreateCustomer = cust
            };
            return PartialView("CustomerPartialEdit", model);
        }
    }
}
