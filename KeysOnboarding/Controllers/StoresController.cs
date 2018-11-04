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
    public class StoresController : Controller
    {
        private KeysContext db = new KeysContext();

        // GET: Stores
        public ActionResult Index()
        {
            //return View(db.Stores.ToList());
            StoreViewModel i = new StoreViewModel()
            {
                Stores = db.Stores.ToList()
            };
            return View(i);
        }

        /// <summary>
        /// Connects to the database
        /// add and update stores
        /// </summary>
        /// <param name="stor">
        /// contains all the data for saving
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(StoreViewModel stor)
        {
            var s_Name = stor.CreateStore.Name;
            Store store = db.Stores.Where(s => s.Name == s_Name).SingleOrDefault();

            if (store != null && store.Id != stor.CreateStore.Id)
            {
                return Json(false);
            }

            if (stor.CreateStore.Id > 0)
            {
                Store str = db.Stores.Where(c => c.Id == stor.CreateStore.Id).SingleOrDefault();
                str.Name = stor.CreateStore.Name;
                str.Address = stor.CreateStore.Address;
                db.SaveChanges();
            }
            else
            {
                Store st = new Store()
                {
                    Name = stor.CreateStore.Name,
                    Address = stor.CreateStore.Address
                };
                db.Stores.Add(st);
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }

        /// <summary>
        /// deletes the store from the database
        /// </summary>
        /// <param name="storeId">
        /// contains the id of the store to be deleted
        /// </param>
        /// <returns></returns>
        public ActionResult DeleteStore(int storeId)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                Store stor = db.Stores.SingleOrDefault(x => x.Id == storeId);
                if (stor != null)
                {
                    db.Stores.Remove(stor);
                    db.SaveChanges();
                    result = true;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// shows the details of a particular store
        /// </summary>
        /// <param name="storeId">
        /// contains the id of the store
        /// </param>
        /// <returns></returns>
        public ActionResult ShowStore(int storeId)
        {
            //Product prod = db.Products.SingleOrDefault(x => x.Id == productId);
            Store stor = db.Stores.Where(x => x.Id == storeId).SingleOrDefault();

            StoreViewModel model = new StoreViewModel()
            {
                CreateStore = stor
            };
            return PartialView("StorePartialShow", model);

            //return View();
        }

        /// <summary>
        /// gets the store from database
        /// and shows the store details in partial view
        /// </summary>
        /// <param name="storeId">
        /// contains the id of the store to be updated
        /// </param>
        /// <returns></returns>
        public ActionResult EditStore(int storeId)
        {

            Store stor = db.Stores.Where(p => p.Id == storeId).SingleOrDefault();
            StoreViewModel model = new StoreViewModel()
            {
                CreateStore = stor
            };
            return PartialView("StorePartialEdit", model);
        }
    }
}
