using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MichellesWebsite.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace MichellesWebsite.Controllers
{
    [Authorize]
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

        public ActionResult PurchaseDetails(ProductModel product, int quantity)
        {
            ProductPrice price = db.ProductPrices.Where(x => x.productID == product.ID).Where(x => x.dateTo == null).SingleOrDefault();
            ApplicationCartItem itemToPurchase = new ApplicationCartItem();
            itemToPurchase.ProductId = product.ID;
            itemToPurchase.Name = product.name;
            itemToPurchase.Quantity = quantity;
            itemToPurchase.Price = 2.05M;
            
            ApplicationCart cart = new ApplicationCart
            {
                Id = Guid.NewGuid(), // Unique purchase Id
                Currency = "GBP",
                PurchaseDescription = "Left Handed Screwdriver",
                Items = new List<ApplicationCartItem>()
            };
            cart.Items.Add(itemToPurchase);

            // Storing this in session, you might want to store in it a database
            Session["Cart"] = cart;

            return View(cart);
        }
        /*[HttpPost]
        public ActionResult PurchaseDetails(SaleModel sale)
        {
            sale.Amount = db.ProductPrices.Single(x => x.ID == sale.PriceId).price * sale.Quantity;
            sale.ts = DateTime.Now;
            sale.CustomerId = User.Identity.GetUserId();
            db.SaleModels.Add(sale);
            db.SaveChanges();
            string message = "You bought " + sale.Quantity + " " + db.ProductModels.Single(x => x.ID == sale.ProductId).name + " for a total of £" 
                + sale.Amount + ". The estimated delivery date is " + (DateTime.Now.AddDays(14));
            return RedirectToAction("PostPurchaseDetails", new { sale = sale });
        }*/

        public ActionResult PostPurchaseDetails(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        // GET: ViewProduct/Details/5
        public ActionResult Details(int? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.ProductModels.Find(productId);
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
