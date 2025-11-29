using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecCenter;

namespace RecCenter.App_Data
{
    public class EQUIPMENTsController : Controller
    {
        private group8Entities db = new group8Entities();

        // GET: EQUIPMENTs
        public ActionResult Index()
        {
            var eQUIPMENTs = db.EQUIPMENTs.Include(e => e.FACILITY);
            return View(eQUIPMENTs.ToList());
        }

        // GET: EQUIPMENTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQUIPMENT eQUIPMENT = db.EQUIPMENTs.Find(id);
            if (eQUIPMENT == null)
            {
                return HttpNotFound();
            }
            return View(eQUIPMENT);
        }

        // GET: EQUIPMENTs/Create
        public ActionResult Create()
        {
            ViewBag.Facility_ID = new SelectList(db.FACILITies, "Facility_ID", "Facility_Name");
            return View();
        }

        // POST: EQUIPMENTs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EQUIPMENT equipment)
        {
            // FK dropdown: Facility
            ViewBag.Facility_ID = new SelectList(db.FACILITies, "Facility_ID", "Facility_Name", equipment.Facility_ID);

            // Default dates / status
            if (equipment.Purchase_Date == default(DateTime))
                equipment.Purchase_Date = DateTime.Now;

            if (equipment.Created_Date == default(DateTime))
                equipment.Created_Date = DateTime.Now;

            if (string.IsNullOrWhiteSpace(equipment.Status))
                equipment.Status = "Active";

            if (!ModelState.IsValid)
                return View(equipment);

            try
            {
                db.EQUIPMENTs.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var e in ex.EntityValidationErrors)
                    foreach (var ve in e.ValidationErrors)
                        ModelState.AddModelError(ve.PropertyName, ve.ErrorMessage);

                ModelState.AddModelError("", "There was a validation problem saving this equipment record.");
                return View(equipment);
            }
        }


        // GET: EQUIPMENTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQUIPMENT eQUIPMENT = db.EQUIPMENTs.Find(id);
            if (eQUIPMENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.Facility_ID = new SelectList(db.FACILITies, "Facility_ID", "Facility_Name", eQUIPMENT.Facility_ID);
            return View(eQUIPMENT);
        }

        // POST: EQUIPMENTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Equipment_ID,Equipment_Name,Equipment_Type,Facility_ID,Purchase_Date,Last_Maintenance,Next_Maintenance,Status,Serial_Number,Created_Date")] EQUIPMENT eQUIPMENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eQUIPMENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Facility_ID = new SelectList(db.FACILITies, "Facility_ID", "Facility_Name", eQUIPMENT.Facility_ID);
            return View(eQUIPMENT);
        }

        // GET: EQUIPMENTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQUIPMENT eQUIPMENT = db.EQUIPMENTs.Find(id);
            if (eQUIPMENT == null)
            {
                return HttpNotFound();
            }
            return View(eQUIPMENT);
        }

        // POST: EQUIPMENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EQUIPMENT eQUIPMENT = db.EQUIPMENTs.Find(id);
            db.EQUIPMENTs.Remove(eQUIPMENT);
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
