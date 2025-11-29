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
    public class MEMBERSHIP_TYPEController : Controller
    {
        private group8Entities db = new group8Entities();

        // GET: MEMBERSHIP_TYPE
        public ActionResult Index()
        {
            return View(db.MEMBERSHIP_TYPE.ToList());
        }

        // GET: MEMBERSHIP_TYPE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBERSHIP_TYPE mEMBERSHIP_TYPE = db.MEMBERSHIP_TYPE.Find(id);
            if (mEMBERSHIP_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(mEMBERSHIP_TYPE);
        }

        // GET: MEMBERSHIP_TYPE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MEMBERSHIP_TYPE/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MEMBERSHIP_TYPE type)
        {
            if (type.Created_Date == default(DateTime))
                type.Created_Date = DateTime.Now;

            if (!ModelState.IsValid)
                return View(type);

            try
            {
                db.MEMBERSHIP_TYPE.Add(type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var e in ex.EntityValidationErrors)
                    foreach (var ve in e.ValidationErrors)
                        ModelState.AddModelError(ve.PropertyName, ve.ErrorMessage);

                ModelState.AddModelError("", "There was a validation problem saving this membership type.");
                return View(type);
            }
        }



        // GET: MEMBERSHIP_TYPE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBERSHIP_TYPE mEMBERSHIP_TYPE = db.MEMBERSHIP_TYPE.Find(id);
            if (mEMBERSHIP_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(mEMBERSHIP_TYPE);
        }

        // POST: MEMBERSHIP_TYPE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Type_ID,Type_Name,Description,Monthly_Fee,Duration_Months,Max_Classes,Facility_Access,Created_Date")] MEMBERSHIP_TYPE mEMBERSHIP_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mEMBERSHIP_TYPE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mEMBERSHIP_TYPE);
        }

        // GET: MEMBERSHIP_TYPE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBERSHIP_TYPE mEMBERSHIP_TYPE = db.MEMBERSHIP_TYPE.Find(id);
            if (mEMBERSHIP_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(mEMBERSHIP_TYPE);
        }

        // POST: MEMBERSHIP_TYPE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MEMBERSHIP_TYPE mEMBERSHIP_TYPE = db.MEMBERSHIP_TYPE.Find(id);
            db.MEMBERSHIP_TYPE.Remove(mEMBERSHIP_TYPE);
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
