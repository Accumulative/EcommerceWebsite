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
    [Authorize(Roles = "Administrator")]
    public class ProductController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductModels

        public ActionResult Index()
        {
            return View(db.ProductModels.ToList());
        }

        public ActionResult ListStockTransactions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Id = id;
            return View(db.StockTransactions.Where(x => x.ProductId == id).OrderByDescending(m => m.ts).ToList());
        }
        public ActionResult AddStock(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockTransaction stk = new StockTransaction();
            stk.ProductId = id??1;
            return View(stk);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStock(StockTransaction stockTransaction)
        {
            ProductModel pr = db.ProductModels.Single(x => x.ID == stockTransaction.ProductId);
            if (ModelState.IsValid && pr.stock + stockTransaction.Amount > 0)
            {
                stockTransaction.ts = DateTime.UtcNow;
                pr.stock = pr.stock + stockTransaction.Amount;
                db.StockTransactions.Add(stockTransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockTransaction);
        }

        // GET: ProductModels/Details/5
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

        // GET: ProductModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,description,zhName,zhDescription")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                productModel.ts = DateTime.UtcNow;
                productModel.stock = 0;
                db.ProductModels.Add(productModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productModel);
        }

        // GET: ProductModels/Edit/5
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

        // POST: ProductModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                productModel.ts = DateTime.UtcNow;
                db.Entry(productModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productModel);
        }

        public ActionResult UploadPicture(int? id)
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
        [HttpPost]
        public ActionResult UploadPicture(HttpPostedFileBase file, int productID)
        {
            BlobHandler bh = new BlobHandler("containername");
            bh.Upload(file);
            //var blobUris = bh.GetBlobs();

            ProductModel product = db.ProductModels.Single(x => x.ID == productID);
            product.picture = file.FileName;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult UpdatePrice(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<ProductPrice> productPrices = db.ProductPrices.Where(x => x.productID == id).OrderByDescending(x => x.dateFrom).Take(10).ToList();
            int productId = id ?? 0;
            return View(new ProductPriceView() { productPrices = productPrices, newProductPrice = new ProductPrice() { productID = productId } });
        }
        [HttpPost]
        public ActionResult UpdatePrice(ProductPriceView productPriceview)
        {
            List<ProductPrice> productPrices = db.ProductPrices.Where(x => x.productID == productPriceview.newProductPrice.productID).ToList();

            foreach(ProductPrice pp in productPrices)
            {
                if (pp.dateTo == null && pp.country == productPriceview.newProductPrice.country)
                {
                    ProductPrice oldProductPrice = pp;
                    oldProductPrice.dateTo = DateTime.UtcNow;
                    db.Entry(oldProductPrice).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            ProductPrice newProductPrice = new ProductPrice() { price = productPriceview.newProductPrice.price, dateFrom = DateTime.UtcNow, productID = productPriceview.newProductPrice.productID, country = productPriceview.newProductPrice.country};
            db.ProductPrices.Add(newProductPrice);
            db.SaveChanges();
            return RedirectToAction("UpdatePrice", new { id = productPriceview.newProductPrice.productID } );
        }

        // GET: ProductModels/Delete/5
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

        // POST: ProductModels/Delete/5
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
