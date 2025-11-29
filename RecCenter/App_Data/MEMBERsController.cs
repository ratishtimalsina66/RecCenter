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
    public class MEMBERsController : Controller
    {
        private group8Entities db = new group8Entities();

        // GET: MEMBERs
        public ActionResult Index()
        {
            var mEMBERs = db.MEMBERs.Include(m => m.MEMBERSHIP_TYPE);
            return View(mEMBERs.ToList());
        }

        // GET: MEMBERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBER mEMBER = db.MEMBERs.Find(id);
            if (mEMBER == null)
            {
                return HttpNotFound();
            }
            return View(mEMBER);
        }

        // GET: MEMBERs/Create
        public ActionResult Create()
        {
            ViewBag.Membership_Type_ID = new SelectList(db.MEMBERSHIP_TYPE, "Type_ID", "Type_Name");
            return View();
        }

        // POST: MEMBERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "First_Name,Last_Name,Email,Phone,Date_Of_Birth,Join_Date,Membership_Type_ID,Status,Address,City,State,ZIP_Code,Created_Date")]
    MEMBER mEMBER)
        {
            // if we need to redisplay the form, repopulate the membership type dropdown
            ViewBag.Membership_Type_ID =
                new SelectList(db.MEMBERSHIP_TYPE, "Type_ID", "Type_Name", mEMBER.Membership_Type_ID);

            // ---- server-side defaults to satisfy NOT NULL columns ----
            if (mEMBER.Join_Date == default(DateTime))
                mEMBER.Join_Date = DateTime.Now;

            if (mEMBER.Created_Date == default(DateTime))
                mEMBER.Created_Date = DateTime.Now;

            if (string.IsNullOrWhiteSpace(mEMBER.Status))
                mEMBER.Status = "Active";   // or whatever makes sense

            if (!ModelState.IsValid)
            {
                // show validation messages on the form instead of crashing
                return View(mEMBER);
            }

            try
            {
                db.MEMBERs.Add(mEMBER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                // Push EF validation errors into ModelState so they show on the page
                foreach (var entityErrors in ex.EntityValidationErrors)
                {
                    foreach (var ve in entityErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(ve.PropertyName, ve.ErrorMessage);
                    }
                }

                ModelState.AddModelError(
                    "",
                    "There was a validation problem saving this member. Please review the highlighted fields."
                );

                return View(mEMBER);
            }
        }


        // GET: MEMBERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBER mEMBER = db.MEMBERs.Find(id);
            if (mEMBER == null)
            {
                return HttpNotFound();
            }
            ViewBag.Membership_Type_ID = new SelectList(db.MEMBERSHIP_TYPE, "Type_ID", "Type_Name", mEMBER.Membership_Type_ID);
            return View(mEMBER);
        }

        // POST: MEMBERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Member_ID,First_Name,Last_Name,Email,Phone,Date_Of_Birth,Join_Date,Membership_Type_ID,Status,Address,City,State,ZIP_Code,Created_Date")] MEMBER mEMBER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mEMBER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Membership_Type_ID = new SelectList(db.MEMBERSHIP_TYPE, "Type_ID", "Type_Name", mEMBER.Membership_Type_ID);
            return View(mEMBER);
        }

        // GET: MEMBERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MEMBER mEMBER = db.MEMBERs.Find(id);
            if (mEMBER == null)
            {
                return HttpNotFound();
            }
            return View(mEMBER);
        }

        // POST: MEMBERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MEMBER mEMBER = db.MEMBERs.Find(id);
            db.MEMBERs.Remove(mEMBER);
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
