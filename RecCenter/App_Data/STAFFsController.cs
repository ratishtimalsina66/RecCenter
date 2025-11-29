using System.Data.Entity.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecCenter;

namespace RecCenter.App_Data
{
    public class STAFFsController : Controller
    {
        private group8Entities db = new group8Entities();

        // GET: STAFFs
        public ActionResult Index()
        {
            return View(db.STAFFs.ToList());
        }

        // GET: STAFFs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STAFF sTAFF = db.STAFFs.Find(id);
            if (sTAFF == null)
            {
                return HttpNotFound();
            }
            return View(sTAFF);
        }

        // GET: STAFFs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: STAFFs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(STAFF sTAFF)
        {
            // If you have dropdowns for Role, Department, etc., rebuild them here
            // ViewBag.Role_ID = new SelectList(db.ROLES, "Role_ID", "Role_Name", sTAFF.Role_ID);

            // Default required dates / status
            if (sTAFF.Hire_Date == default(DateTime))
                sTAFF.Hire_Date = DateTime.Now;

            if (sTAFF.Created_Date == default(DateTime))
                sTAFF.Created_Date = DateTime.Now;

            if (string.IsNullOrWhiteSpace(sTAFF.Status))
                sTAFF.Status = "Active";

            if (!ModelState.IsValid)
                return View(sTAFF);

            try
            {
                db.STAFFs.Add(sTAFF);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var e in ex.EntityValidationErrors)
                    foreach (var ve in e.ValidationErrors)
                        ModelState.AddModelError(ve.PropertyName, ve.ErrorMessage);

                ModelState.AddModelError("", "There was a validation problem saving this staff member.");
                return View(sTAFF);
            }
        }


        // GET: STAFFs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STAFF sTAFF = db.STAFFs.Find(id);
            if (sTAFF == null)
            {
                return HttpNotFound();
            }
            return View(sTAFF);
        }

        // POST: STAFFs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Staff_ID,First_Name,Last_Name,Email,Phone,Position,Hire_Date,Certification,Hourly_Rate,Status,Created_Date")] STAFF sTAFF)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTAFF).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sTAFF);
        }

        // GET: STAFFs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STAFF sTAFF = db.STAFFs.Find(id);
            if (sTAFF == null)
            {
                return HttpNotFound();
            }
            return View(sTAFF);
        }

        // POST: STAFFs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STAFF sTAFF = db.STAFFs.Find(id);
            db.STAFFs.Remove(sTAFF);
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
