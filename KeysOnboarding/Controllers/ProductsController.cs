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
    public class ProductsController : Controller
    {
        private KeysContext db = new KeysContext();

        // GET: Products
        public ActionResult Index()
        {
            //ModelState.Clear();
            ProductViewModel product = new ProductViewModel()
            {
                Products = db.Products.ToList()
            };
            return View(product);
        }

        /// <summary>
        /// Connects to the database
        /// add and update products
        /// </summary>
        /// <param name="prod">
        /// contains all the data for saving
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(ProductViewModel prod)
        {
            var p_Name = prod.CreateProduct.Name;
            Product product = db.Products.Where(p => p.Name == p_Name).SingleOrDefault();

            if (product != null && product.Id != prod.CreateProduct.Id)
            {
                return Json(false);
            }

            if (prod.CreateProduct.Id > 0)
            {
                Product pro = db.Products.Where(p => p.Id == prod.CreateProduct.Id).SingleOrDefault();
                pro.Name = prod.CreateProduct.Name;
                pro.Price = prod.CreateProduct.Price;
                db.SaveChanges();
            }
            else
            {
                Product pr = new Product()
                {
                    Name = prod.CreateProduct.Name,
                    Price = prod.CreateProduct.Price
                };
                db.Products.Add(pr);
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }

        /// <summary>
        /// deletes the product from the database
        /// </summary>
        /// <param name="productId">
        /// contains the id of the product to be deleted
        /// </param>
        /// <returns></returns>
        public ActionResult DeleteProduct(int productId)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                Product prod = db.Products.SingleOrDefault(x => x.Id == productId);
                if (prod != null)
                {
                    db.Products.Remove(prod);
                    db.SaveChanges();
                    result = true;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// shows the details of a particular product
        /// </summary>
        /// <param name="productId">
        /// contains the id of the product
        /// </param>
        /// <returns></returns>
        public ActionResult ShowProduct(int productId)
        {
            //Product prod = db.Products.SingleOrDefault(x => x.Id == productId);
            Product prod = db.Products.Where(x => x.Id == productId).SingleOrDefault();

            ProductViewModel model = new ProductViewModel()
            {
                CreateProduct = prod
            };
            return PartialView("ProductPartialShow", model);

            //return View();
        }

        /// <summary>
        /// gets the product from database
        /// and shows the product details in partial view
        /// </summary>
        /// <param name="productId">
        /// contains the id of the product to be updated
        /// </param>
        /// <returns></returns>
        public ActionResult EditProduct(int productId)
        {

            Product prod = db.Products.Where(p => p.Id == productId).SingleOrDefault();
            ProductViewModel model = new ProductViewModel()
            {
                CreateProduct = prod
            };
            return PartialView("ProductPartialEdit", model);
        }
    }
}
