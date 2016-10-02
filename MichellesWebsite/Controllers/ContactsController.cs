using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MichellesWebsite.Models;
using Microsoft.AspNet.Identity;

namespace MichellesWebsite.Controllers
{
    [Authorize]
    public class ContactsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contacts
        [Authorize(Roles ="Administrator")]
        public ActionResult Index()
        {
            List<Enquiry> enquiries = db.Enquiries.OrderByDescending(x => x.ts).ToList();
            List <ApplicationUser> users = db.Users.ToList();
            List<int> enquiryId = enquiries.Select(t => t.EnquiryId).ToList();
            List<Contact> contacts = db.Contacts.OrderByDescending(x => x.ts).Where(x => enquiryId.Contains(x.EnquiryId)).ToList();
            List<EnquiryListView> contactList = enquiries.Select(x => new EnquiryListView()
            {
                EnquiryId = x.EnquiryId,
                message = contacts.First(y => y.EnquiryId == y.EnquiryId).message.Substring(0, 15) + "...",
                queryType = x.queryType,
                ts = x.ts,
                user = users.Single(y => y.Id == x.CustomerId)
            }).ToList();
            return View(contactList);
        }

        public ActionResult EnquiryDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enquiry enq = db.Enquiries.Single(x => x.EnquiryId == id);
            if (enq.CustomerId == User.Identity.GetUserId())
            {
                ContactListView contactList = new ContactListView()
                {
                    messages = db.Contacts.Where(x => x.EnquiryId == id).ToList(),
                    queryType = enq.queryType
                };
                return View(contactList);
            }
            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult Enquiries()
        {
            string userId = User.Identity.GetUserId();
            List<Enquiry> enquiries = db.Enquiries.OrderByDescending(x => x.ts).Where(x => x.CustomerId == userId).ToList();
            List<int> enquiryId = enquiries.Select(t => t.EnquiryId).ToList();
            List<Contact> contacts = db.Contacts.OrderByDescending(x => x.ts).Where(x => enquiryId.Contains(x.EnquiryId)).ToList();
            List<ContactView> contactList = enquiries.Select(x => new ContactView()
            {
                EnquiryId = x.EnquiryId,
                queryType = x.queryType,
                ts = x.ts,
                message = contacts.First(y => y.EnquiryId == y.EnquiryId).message.Substring(0, 15) + "..."
            }).ToList();
            return View(contactList);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactListView contactList = new ContactListView()
            {
                messages = db.Contacts.Where(x => x.EnquiryId == id).ToList(),
                queryType = db.Enquiries.Single(x => x.EnquiryId == id).queryType
            };
            return View(contactList);
        }

        // GET: Contacts/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Contact", "Home", null);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "queryType,message")] ContactView contact)
        {
            if (ModelState.IsValid)
            {
                Contact con = new Contact
                {
                    Enquiry = new Enquiry
                    {
                        CustomerId = User.Identity.GetUserId(),
                        queryType = contact.queryType,
                        ts = DateTime.UtcNow
                    },
                    ts = DateTime.UtcNow,
                    FromCustomer = true,
                    message = contact.message
                };
                db.Contacts.Add(con);
                db.SaveChanges();
                ViewBag.Message = "Submitted successfully. Check in your account tickets for updates.";
                return View();

            }
            return View(contact);
        }

        public ActionResult Reply(int id)
        {
            Contact con = new Contact();
            con.EnquiryId = id;
            return View(con);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply([Bind(Include = "EnquiryId,message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.ts = DateTime.UtcNow;
                contact.FromCustomer = true;
                db.Contacts.Add(contact);
                db.SaveChanges();
                ViewBag.Message = "Submitted successfully. Check in your account tickets for updates.";
                return View(new Contact() { message = "" });

            }
            return View(contact);
        }

        public ActionResult ReplyAdmin(int id)
        {
            Contact con = new Contact();
            con.EnquiryId = id;
            return View(con);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReplyAdmin([Bind(Include = "EnquiryId,message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.ts = DateTime.UtcNow;
                contact.FromCustomer = false;
                db.Contacts.Add(contact);
                db.SaveChanges();
                ViewBag.Message = "Submitted successfully. Check in your account tickets for updates.";
                return View(new Contact() { message = "" });

            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
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
