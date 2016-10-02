using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MichellesWebsite.Models;
using System.Web.Services;

namespace MichellesWebsite.Controllers
{
    public class DeliveryModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeliveryModels
        public ActionResult Index()
        {
            return View(db.DeliveryModels.ToList());
        }
        [WebMethod]
        public ActionResult CalculateDeliveryCost(string quantity, string addressId)
        {
            WebUILogging.LogMessage("Test1: " + addressId);
            Address ad = db.Addresses.Find(int.Parse(addressId));
            WebUILogging.LogMessage("Test2");
            DeliveryModel dm = db.DeliveryModels.Where(x => x.stateId == ad.stateId).First();
            WebUILogging.LogMessage("Test3");
            decimal cost = 0;
            if (dm != null && ad != null)
            {
                
                int qty = Int32.Parse(quantity);
                if (qty == 1) cost += dm.costWithin500;
                if (qty >= 2) cost += dm.costWithin1000;
                if (qty > 2) cost += dm.costPer1000 * (Math.Floor((decimal)(qty-3)/2)+1);
            }
            WebUILogging.LogMessage(cost.ToString());
            return Json(new { cost = cost, type = "£" }); 
        }

        // GET: DeliveryModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryModel deliveryModel = db.DeliveryModels.Find(id);
            if (deliveryModel == null)
            {
                return HttpNotFound();
            }
            return View(deliveryModel);
        }

        // GET: DeliveryModels/Create
        public ActionResult Create()
        {
            ViewBag.StateList = new SelectList(db.States.ToArray(), "id", "name");
            return View();
        }

        // POST: DeliveryModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,stateId,costWithin500,costWithin1000,costPer1000")] DeliveryModel deliveryModel)
        {
            if (ModelState.IsValid)
            {
                ViewBag.StateList = new SelectList(db.States.ToArray(), "id", "name");
                db.DeliveryModels.Add(deliveryModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deliveryModel);
        }

        // GET: DeliveryModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryModel deliveryModel = db.DeliveryModels.Find(id);
            if (deliveryModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateList = new SelectList(db.States.ToArray(), "id", "name",deliveryModel.stateId);
            return View(deliveryModel);
        }

        // POST: DeliveryModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,stateId,costWithin500,costWithin1000,costPer1000")] DeliveryModel deliveryModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliveryModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deliveryModel);
        }

        // GET: DeliveryModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryModel deliveryModel = db.DeliveryModels.Find(id);
            if (deliveryModel == null)
            {
                return HttpNotFound();
            }
            return View(deliveryModel);
        }

        // POST: DeliveryModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeliveryModel deliveryModel = db.DeliveryModels.Find(id);
            db.DeliveryModels.Remove(deliveryModel);
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
