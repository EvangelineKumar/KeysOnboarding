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
    public class ProductSoldsController : Controller
    {
        private KeysContext db = new KeysContext();

        // GET: ProductSolds
        public ActionResult Index()
        {
            var model = new SalesViewModel()
            {
                Customers = db.Customers.ToList(),
                Products = db.Products.ToList(),
                Stores = db.Stores.ToList(),
                SalesList = db.ProductSolds.Include(p => p.Customer).Include(p => p.Product).Include(p => p.Store).ToList()
            };
            return View(model);
            //var productSolds = db.ProductSolds.Include(p => p.Customer).Include(p => p.Product).Include(p => p.Store);
            //return View(productSolds.ToList());
        }

        /// <summary>
        /// Connects to the database
        /// add and update sales
        /// </summary>
        /// <param name="model">
        /// contains all the data for saving
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(SalesViewModel model)
        {
            if(model.ProductSold.DateSold > DateTime.Now)
            {
                return Json(false);
            }
            if (model.ProductSold.Id > 0)
            {
                ProductSold ps = db.ProductSolds.Where(c => c.Id == model.ProductSold.Id).SingleOrDefault();
                ps.CustomerId = model.ProductSold.CustomerId;
                ps.ProductId = model.ProductSold.ProductId;
                ps.StoreId = model.ProductSold.StoreId;
                ps.DateSold = model.ProductSold.DateSold;
                db.SaveChanges();
            }
            else
            {
                ProductSold ps = new ProductSold()
                {
                    CustomerId = model.ProductSold.CustomerId,
                    ProductId = model.ProductSold.ProductId,
                    StoreId = model.ProductSold.StoreId,
                    DateSold = model.ProductSold.DateSold
                };

                db.ProductSolds.Add(ps);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// deletes the sale from the database
        /// </summary>
        /// <param name="saleId">
        /// contains the id of the sale to be deleted
        /// </param>
        /// <returns></returns>
        public ActionResult DeleteSale(int saleId)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                ProductSold sale = db.ProductSolds.SingleOrDefault(x => x.Id == saleId);
                if (sale != null)
                {
                    db.ProductSolds.Remove(sale);
                    db.SaveChanges();
                    result = true;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// gets the sales from database
        /// and shows the sale details in partial view
        /// </summary>
        /// <param name="saleId">
        /// contains the id of the sale item to be updated
        /// </param>
        /// <returns></returns>
        public ActionResult EditSale(int saleId)
        {
            ProductSold ps = db.ProductSolds.Where(c => c.Id == saleId).SingleOrDefault();
            List<Customer> list = db.Customers.ToList();
            ViewBag.CustomerList = new SelectList(list, "Id", "Name");
            List<Product> list2 = db.Products.ToList();
            ViewBag.ProductList = new SelectList(list2, "Id", "Name");
            List<Store> list3 = db.Stores.ToList();
            ViewBag.StoreList = new SelectList(list3, "Id", "Name");
            SalesViewModel modelname = new SalesViewModel()
            {
                ProductSold = ps
            };
            return PartialView("SalePartialView", modelname);
        }

        
    }
}
