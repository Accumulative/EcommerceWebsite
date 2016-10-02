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
    public class CouponModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CouponModels
        public ActionResult Index()
        {
            return View(db.CouponModels.ToList());
        }
        [WebMethod]
        public ActionResult CheckCoupon(string code)
        {
            List<CouponModel> coupons = db.CouponModels.Where(x => x.code == code && x.startDate <= DateTime.Now && x.endDate >= DateTime.Now).ToList();
            if (coupons.Count > 0)
            {
                CouponModel coupon = coupons.First();
                return Json(new
                {
                    message = "Successful",
                    discount = coupon.discount,
                    freeDel = coupon.freeDelivery
                });
            }
            return Json(new { message = "Unsuccessful", discount = 0, freeDel = false });
        }
        // GET: CouponModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CouponModel couponModel = db.CouponModels.Find(id);
            if (couponModel == null)
            {
                return HttpNotFound();
            }
            return View(couponModel);
        }

        // GET: CouponModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CouponModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,code,startDate,endDate,discount,freeDelivery")] CouponModel couponModel)
        {
            if (ModelState.IsValid)
            {
                List<CouponModel> coupons = db.CouponModels.Where(x => x.code == couponModel.code && ((couponModel.startDate <= x.startDate && x.startDate <= couponModel.endDate) || (x.startDate <= couponModel.startDate && couponModel.startDate <= x.endDate))).ToList();
                if (coupons.Count== 0)
                {
                    couponModel.ts = DateTime.Now;
                    db.CouponModels.Add(couponModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Message = "Coupon already exists for that period";
            }
            
            return View(couponModel);
        }

        // GET: CouponModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CouponModel couponModel = db.CouponModels.Find(id);
            if (couponModel == null)
            {
                return HttpNotFound();
            }
            return View(couponModel);
        }

        // POST: CouponModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,code,startDate,endDate,discount,freeDelivery")] CouponModel couponModel)
        {
            if (ModelState.IsValid)
            {
                List<CouponModel> coupons = db.CouponModels.Where(x => x.id != couponModel.id && x.code == couponModel.code && ((couponModel.startDate <= x.startDate && x.startDate <= couponModel.endDate) || (x.startDate <= couponModel.startDate && couponModel.startDate <= x.endDate))).ToList();
                if (coupons.Count == 0)
                {
                    couponModel.ts = DateTime.Now;
                    db.Entry(couponModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Message = "Coupon already exists for that period";
            }
            return View(couponModel);
        }

        // GET: CouponModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CouponModel couponModel = db.CouponModels.Find(id);
            if (couponModel == null)
            {
                return HttpNotFound();
            }
            return View(couponModel);
        }

        // POST: CouponModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CouponModel couponModel = db.CouponModels.Find(id);
            db.CouponModels.Remove(couponModel);
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
