using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MichellesWebsite.Models;

namespace MichellesWebsite.Controllers
{
    public class ViewProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ViewProduct
        public ActionResult Index()
        {
            ProductStore ps = new ProductStore();
            ps.products = new List<ProductModel>();
            ps.prices = new List<string>();
            List<ProductModel> pl = db.ProductModels.ToList();
            List<ProductPrice> prices = db.ProductPrices.Where(x => x.dateTo == null).ToList();
            foreach( ProductModel product in pl )
            {
                ps.products.Add(product);
                if (prices.Any(x => x.productID == product.ID))
                {
                    ps.prices.Add(prices.Single(x => x.productID == product.ID).price.ToString());
                        } else { ps.prices.Add("None");
                }
            }
            return View(ps);
        }

        // GET: ViewProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.ProductModels.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // GET: ViewProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViewProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,description,ts,picture")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                db.ProductModels.Add(productModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productModel);
        }

        // GET: ViewProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.ProductModels.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // POST: ViewProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,description,ts,picture")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productModel);
        }

        // GET: ViewProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.ProductModels.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // POST: ViewProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductModel productModel = db.ProductModels.Find(id);
            db.ProductModels.Remove(productModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
