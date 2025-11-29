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
    public class FACILITiesController : Controller
    {
        private group8Entities db = new group8Entities();

        // GET: FACILITies
        public ActionResult Index()
        {
            return View(db.FACILITies.ToList());
        }

        // GET: FACILITies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FACILITY fACILITY = db.FACILITies.Find(id);
            if (fACILITY == null)
            {
                return HttpNotFound();
            }
            return View(fACILITY);
        }

        // GET: FACILITies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FACILITies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FACILITY facility)
        {
            // If Facility has any FKs with dropdowns, rebuild them here

            // Default Created_Date / Status
            if (facility.Created_Date == default(DateTime))
                facility.Created_Date = DateTime.Now;

            if (string.IsNullOrWhiteSpace(facility.Status))
                facility.Status = "Active";

            if (!ModelState.IsValid)
                return View(facility);

            try
            {
                db.FACILITies.Add(facility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var e in ex.EntityValidationErrors)
                    foreach (var ve in e.ValidationErrors)
                        ModelState.AddModelError(ve.PropertyName, ve.ErrorMessage);

                ModelState.AddModelError("", "There was a validation problem saving this facility.");
                return View(facility);
            }
        }

        // GET: FACILITies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FACILITY fACILITY = db.FACILITies.Find(id);
            if (fACILITY == null)
            {
                return HttpNotFound();
            }
            return View(fACILITY);
        }

        // POST: FACILITies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Facility_ID,Facility_Name,Facility_Type,Capacity,Floor_Number,Equipment_Available,Hourly_Rate,Status,Created_Date")] FACILITY fACILITY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fACILITY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fACILITY);
        }

        // GET: FACILITies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FACILITY fACILITY = db.FACILITies.Find(id);
            if (fACILITY == null)
            {
                return HttpNotFound();
            }
            return View(fACILITY);
        }

        // POST: FACILITies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FACILITY fACILITY = db.FACILITies.Find(id);
            db.FACILITies.Remove(fACILITY);
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
