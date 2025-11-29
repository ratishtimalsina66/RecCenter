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
    public class CLASSesController : Controller
    {
        private group8Entities db = new group8Entities();

        // GET: CLASSes
        public ActionResult Index()
        {
            var cLASSes = db.CLASSes.Include(c => c.STAFF);
            return View(cLASSes.ToList());
        }

        // GET: CLASSes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLASS cLASS = db.CLASSes.Find(id);
            if (cLASS == null)
            {
                return HttpNotFound();
            }
            return View(cLASS);
        }

        // GET: CLASSes/Create
        public ActionResult Create()
        {
            ViewBag.Instructor_ID = new SelectList(db.STAFFs, "Staff_ID", "First_Name");
            return View();
        }

        // POST: CLASSes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Class_ID,Class_Name,Description,Instructor_ID,Duration_Minutes,Max_Capacity,Difficulty_Level,Category,Created_Date")] CLASS cLASS)
        {
            if (ModelState.IsValid)
            {
                db.CLASSes.Add(cLASS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Instructor_ID = new SelectList(db.STAFFs, "Staff_ID", "First_Name", cLASS.Instructor_ID);
            return View(cLASS);
        }

        // GET: CLASSes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLASS cLASS = db.CLASSes.Find(id);
            if (cLASS == null)
            {
                return HttpNotFound();
            }
            ViewBag.Instructor_ID = new SelectList(db.STAFFs, "Staff_ID", "First_Name", cLASS.Instructor_ID);
            return View(cLASS);
        }

        // POST: CLASSes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Class_ID,Class_Name,Description,Instructor_ID,Duration_Minutes,Max_Capacity,Difficulty_Level,Category,Created_Date")] CLASS cLASS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLASS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Instructor_ID = new SelectList(db.STAFFs, "Staff_ID", "First_Name", cLASS.Instructor_ID);
            return View(cLASS);
        }

        // GET: CLASSes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLASS cLASS = db.CLASSes.Find(id);
            if (cLASS == null)
            {
                return HttpNotFound();
            }
            return View(cLASS);
        }

        // POST: CLASSes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CLASS cLASS = db.CLASSes.Find(id);
            db.CLASSes.Remove(cLASS);
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
