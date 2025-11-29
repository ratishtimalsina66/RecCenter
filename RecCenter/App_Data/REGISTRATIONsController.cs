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
    public class REGISTRATIONsController : Controller
    {
        private group8Entities db = new group8Entities();

        // GET: REGISTRATIONs
        public ActionResult Index()
        {
            var rEGISTRATIONs = db.REGISTRATIONs.Include(r => r.CLASS_SCHEDULE).Include(r => r.MEMBER);
            return View(rEGISTRATIONs.ToList());
        }

        // GET: REGISTRATIONs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGISTRATION rEGISTRATION = db.REGISTRATIONs.Find(id);
            if (rEGISTRATION == null)
            {
                return HttpNotFound();
            }
            return View(rEGISTRATION);
        }

        // GET: REGISTRATIONs/Create
        public ActionResult Create()
        {
            ViewBag.Schedule_ID = new SelectList(db.CLASS_SCHEDULE, "Schedule_ID", "Day_Of_Week");
            ViewBag.Member_ID = new SelectList(db.MEMBERs, "Member_ID", "First_Name");
            return View();
        }

        // POST: REGISTRATIONs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Member_ID,Schedule_ID,Registration_Date,Status,Attendance,Created_Date")]
    REGISTRATION registration)
        {
            // Rebuild dropdowns so they are ready if we need to redisplay the form
            ViewBag.Schedule_ID = new SelectList(db.CLASS_SCHEDULE, "Schedule_ID", "Day_Of_Week", registration.Schedule_ID);
            ViewBag.Member_ID = new SelectList(db.MEMBERs, "Member_ID", "First_Name", registration.Member_ID);

            // ---- Server-side defaults for NOT NULL columns ----
            if (registration.Registration_Date == default(DateTime))
                registration.Registration_Date = DateTime.Now;

            if (registration.Created_Date == default(DateTime))
                registration.Created_Date = DateTime.Now;

            if (string.IsNullOrWhiteSpace(registration.Status))
                registration.Status = "Active";      // adjust if you use another default

            // If Attendance has to be non-null, you can set a default here too, e.g.:
            // if (string.IsNullOrWhiteSpace(registration.Attendance))
            //     registration.Attendance = "Pending";

            if (!ModelState.IsValid)
            {
                // show validation messages instead of crashing
                return View(registration);
            }

            try
            {
                db.REGISTRATIONs.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                // Push EF errors into ModelState so they show on the page
                foreach (var entityErrors in ex.EntityValidationErrors)
                {
                    foreach (var ve in entityErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(ve.PropertyName, ve.ErrorMessage);
                    }
                }

                ModelState.AddModelError(
                    "",
                    "There was a validation problem saving this registration. Please review the highlighted fields."
                );

                return View(registration);
            }
        }


        // GET: REGISTRATIONs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGISTRATION rEGISTRATION = db.REGISTRATIONs.Find(id);
            if (rEGISTRATION == null)
            {
                return HttpNotFound();
            }
            ViewBag.Schedule_ID = new SelectList(db.CLASS_SCHEDULE, "Schedule_ID", "Day_Of_Week", rEGISTRATION.Schedule_ID);
            ViewBag.Member_ID = new SelectList(db.MEMBERs, "Member_ID", "First_Name", rEGISTRATION.Member_ID);
            return View(rEGISTRATION);
        }

        // POST: REGISTRATIONs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Registration_ID,Member_ID,Schedule_ID,Registration_Date,Status,Attendance,Created_Date")] REGISTRATION rEGISTRATION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEGISTRATION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Schedule_ID = new SelectList(db.CLASS_SCHEDULE, "Schedule_ID", "Day_Of_Week", rEGISTRATION.Schedule_ID);
            ViewBag.Member_ID = new SelectList(db.MEMBERs, "Member_ID", "First_Name", rEGISTRATION.Member_ID);
            return View(rEGISTRATION);
        }

        // GET: REGISTRATIONs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REGISTRATION rEGISTRATION = db.REGISTRATIONs.Find(id);
            if (rEGISTRATION == null)
            {
                return HttpNotFound();
            }
            return View(rEGISTRATION);
        }

        // POST: REGISTRATIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REGISTRATION rEGISTRATION = db.REGISTRATIONs.Find(id);
            db.REGISTRATIONs.Remove(rEGISTRATION);
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
