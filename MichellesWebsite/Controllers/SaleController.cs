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
    public class SaleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SaleModels
        public ActionResult Index()
        {
            return View(db.SaleModels.ToList());
        }

        // GET: SaleModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleModel saleModel = db.SaleModels.Find(id);
            if (saleModel == null)
            {
                return HttpNotFound();
            }
            return View(saleModel);
        }

        // GET: SaleModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaleModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleId,CustomerId,ProductId,Quantity,PriceId,Amount,ts")] SaleModel saleModel)
        {
            if (ModelState.IsValid)
            {
                db.SaleModels.Add(saleModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(saleModel);
        }

        // GET: SaleModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleModel saleModel = db.SaleModels.Find(id);
            if (saleModel == null)
            {
                return HttpNotFound();
            }
            return View(saleModel);
        }

        // POST: SaleModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleId,CustomerId,ProductId,Quantity,PriceId,Amount,ts")] SaleModel saleModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saleModel);
        }

        // GET: SaleModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleModel saleModel = db.SaleModels.Find(id);
            if (saleModel == null)
            {
                return HttpNotFound();
            }
            return View(saleModel);
        }

        // POST: SaleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaleModel saleModel = db.SaleModels.Find(id);
            db.SaleModels.Remove(saleModel);
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
