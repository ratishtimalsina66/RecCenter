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
    public class CLASS_SCHEDULEController : Controller
    {
        private group8Entities db = new group8Entities();

        // GET: CLASS_SCHEDULE
        public ActionResult Index()
        {
            var cLASS_SCHEDULE = db.CLASS_SCHEDULE.Include(c => c.CLASS).Include(c => c.FACILITY);
            return View(cLASS_SCHEDULE.ToList());
        }

        // GET: CLASS_SCHEDULE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLASS_SCHEDULE cLASS_SCHEDULE = db.CLASS_SCHEDULE.Find(id);
            if (cLASS_SCHEDULE == null)
            {
                return HttpNotFound();
            }
            return View(cLASS_SCHEDULE);
        }

        // GET: CLASS_SCHEDULE/Create
        public ActionResult Create()
        {
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "Class_Name");
            ViewBag.Facility_ID = new SelectList(db.FACILITies, "Facility_ID", "Facility_Name");
            return View();
        }

        // POST: CLASS_SCHEDULE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Schedule_ID,Class_ID,Facility_ID,Day_Of_Week,Start_Time,End_Time,Start_Date,End_Date,Created_Date")] CLASS_SCHEDULE cLASS_SCHEDULE)
        {
            if (ModelState.IsValid)
            {
                db.CLASS_SCHEDULE.Add(cLASS_SCHEDULE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "Class_Name", cLASS_SCHEDULE.Class_ID);
            ViewBag.Facility_ID = new SelectList(db.FACILITies, "Facility_ID", "Facility_Name", cLASS_SCHEDULE.Facility_ID);
            return View(cLASS_SCHEDULE);
        }

        // GET: CLASS_SCHEDULE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLASS_SCHEDULE cLASS_SCHEDULE = db.CLASS_SCHEDULE.Find(id);
            if (cLASS_SCHEDULE == null)
            {
                return HttpNotFound();
            }
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "Class_Name", cLASS_SCHEDULE.Class_ID);
            ViewBag.Facility_ID = new SelectList(db.FACILITies, "Facility_ID", "Facility_Name", cLASS_SCHEDULE.Facility_ID);
            return View(cLASS_SCHEDULE);
        }

        // POST: CLASS_SCHEDULE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Schedule_ID,Class_ID,Facility_ID,Day_Of_Week,Start_Time,End_Time,Start_Date,End_Date,Created_Date")] CLASS_SCHEDULE cLASS_SCHEDULE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLASS_SCHEDULE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "Class_Name", cLASS_SCHEDULE.Class_ID);
            ViewBag.Facility_ID = new SelectList(db.FACILITies, "Facility_ID", "Facility_Name", cLASS_SCHEDULE.Facility_ID);
            return View(cLASS_SCHEDULE);
        }

        // GET: CLASS_SCHEDULE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLASS_SCHEDULE cLASS_SCHEDULE = db.CLASS_SCHEDULE.Find(id);
            if (cLASS_SCHEDULE == null)
            {
                return HttpNotFound();
            }
            return View(cLASS_SCHEDULE);
        }

        // POST: CLASS_SCHEDULE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CLASS_SCHEDULE cLASS_SCHEDULE = db.CLASS_SCHEDULE.Find(id);
            db.CLASS_SCHEDULE.Remove(cLASS_SCHEDULE);
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
