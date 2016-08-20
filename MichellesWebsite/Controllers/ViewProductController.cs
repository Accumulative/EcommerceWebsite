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
using System.Globalization;

namespace MichellesWebsite.Controllers
{
    
    public class ViewProductController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ViewProduct
        public ActionResult Index()
        {
            string culture = CultureInfo.CurrentCulture.Name;
            List<ProductModel> pl = db.ProductModels.ToList();
            List<ProductPrice> prices = db.ProductPrices.Where(x => x.dateTo == null && x.country == Country.UK).ToList();
            IEnumerable<ProductViewModel> products = pl.Select(x => new ProductViewModel
            {
                name = culture == "en" ? x.name : x.zhName,
                description = culture=="en"?x.description:x.zhDescription,
                picture = x.picture,
                ID = x.ID,
                price = prices.Single(y => y.productID == x.ID).price,
                quantity = 0
            });
            return View(products);
        }
        [HttpPost]
        [Authorize]
        public ActionResult PurchaseDetails(ProductViewModel product)
        {
            string culture = CultureInfo.CurrentCulture.Name;
            ProductModel productModel = db.ProductModels.Single(x => x.ID == product.ID);
            ApplicationCartItem itemToPurchase = new ApplicationCartItem();
            itemToPurchase.ProductId = product.ID;
            itemToPurchase.Name = culture == "en"?productModel.name : productModel.zhName;
            itemToPurchase.Quantity = product.quantity;
            itemToPurchase.Price = product.price;

                ApplicationCart cart = new ApplicationCart
                {
                    Id = Guid.NewGuid(), // Unique purchase Id
                    Currency = "GBP",
                    PurchaseDescription = culture == "en" ? productModel.name : productModel.zhName,
                    Items = new List<ApplicationCartItem>()
                };
                cart.Items.Add(itemToPurchase);

                // Storing this in session, you might want to store in it a database
                Session["Cart"] = cart;
            string userId = User.Identity.GetUserId();
            ViewBag.Addresses = db.Addresses.Where(x => x.userId == userId).Select(x => new SelectListItem
            {
                Text = x.firstLine + " " + x.secondLine + " " + x.city + " " + x.postcode + "...",
                Value = x.id.ToString()
            });
                return View(cart);
        }
        /*[HttpPost]
        public ActionResult PurchaseDetails(SaleModel sale)
        {
            sale.Amount = db.ProductPrices.Single(x => x.ID == sale.PriceId).price * sale.Quantity;
            sale.ts = DateTime.UtcNow;
            sale.CustomerId = User.Identity.GetUserId();
            db.SaleModels.Add(sale);
            db.SaveChanges();
            string message = "You bought " + sale.Quantity + " " + db.ProductModels.Single(x => x.ID == sale.ProductId).name + " for a total of £" 
                + sale.Amount + ". The estimated delivery date is " + (DateTime.UtcNow.AddDays(14));
            return RedirectToAction("PostPurchaseDetails", new { sale = sale });
        }*/
        [Authorize]
        public ActionResult PostPurchaseDetails(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        // GET: ViewProduct/Details/5

        public ActionResult Details(int productId)
        {
            string culture = CultureInfo.CurrentCulture.Name;
            ProductModel product = db.ProductModels.Single(x => x.ID == productId);
            ProductViewModel productView = new ProductViewModel
            {
                name = culture == "en" ? product.name : product.zhName,
                description = culture == "en" ? product.description : product.zhDescription,
                picture = product.picture,
                ID = product.ID,
                price = db.ProductPrices.Where(x => x.dateTo == null && x.country == Country.UK).Single(y => y.productID == product.ID).price,
                quantity = 0
            };
            return View(productView);
        }

        // GET: ViewProduct/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViewProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
