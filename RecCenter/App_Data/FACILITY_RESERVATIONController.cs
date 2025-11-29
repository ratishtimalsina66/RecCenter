using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RecCenter;

namespace RecCenter.App_Data
{
    public class FACILITY_RESERVATIONController : Controller
    {
        private group8Entities db = new group8Entities();

        // 👇 TODO: replace "YOUR_ALLOWED_STATUS" with one of the values
        // you saw in the CHECK constraint / DISTINCT Status query.
        private const string DefaultReservationStatus = "YOUR_ALLOWED_STATUS";

        // GET: FACILITY_RESERVATION
        public ActionResult Index()
        {
            var reservations = db.FACILITY_RESERVATION
                                 .Include(f => f.FACILITY)
                                 .Include(f => f.MEMBER);
            return View(reservations.ToList());
        }

        // GET: FACILITY_RESERVATION/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var reservation = db.FACILITY_RESERVATION.Find(id);
            if (reservation == null)
                return HttpNotFound();

            return View(reservation);
        }

        // GET: FACILITY_RESERVATION/Create
        public ActionResult Create()
        {
            ViewBag.Facility_ID = new SelectList(db.FACILITies, "Facility_ID", "Facility_Name");
            ViewBag.Member_ID = new SelectList(db.MEMBERs, "Member_ID", "First_Name");
            return View();
        }

        // POST: FACILITY_RESERVATION/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Facility_ID,Member_ID,Reservation_Date,Start_Time,End_Time,Purpose,Status,Created_Date")]
            FACILITY_RESERVATION reservation)
        {
            // repopulate dropdowns if we need to redisplay the form
            ViewBag.Facility_ID = new SelectList(db.FACILITies, "Facility_ID", "Facility_Name", reservation.Facility_ID);
            ViewBag.Member_ID = new SelectList(db.MEMBERs, "Member_ID", "First_Name", reservation.Member_ID);

            // sensible defaults
            if (reservation.Reservation_Date == default(DateTime))
                reservation.Reservation_Date = DateTime.Today;

            if (reservation.Created_Date == default(DateTime))
                reservation.Created_Date = DateTime.Now;

            // ⭐ THIS IS THE CRITICAL PART ⭐
            // Always set Status to a value that is allowed by the CHECK constraint.
            if (string.IsNullOrWhiteSpace(reservation.Status))
                reservation.Status = DefaultReservationStatus;

            if (!ModelState.IsValid)
                return View(reservation);

            try
            {
                db.FACILITY_RESERVATION.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityErrors in ex.EntityValidationErrors)
                    foreach (var ve in entityErrors.ValidationErrors)
                        ModelState.AddModelError(ve.PropertyName, ve.ErrorMessage);

                ModelState.AddModelError("", "There was a validation problem saving this facility reservation.");
                return View(reservation);
            }
            catch (Exception)
            {
                // this will catch the SQL CHECK error if Status still isn't valid
                ModelState.AddModelError("", "The database rejected the Status value. Make sure DefaultReservationStatus matches the allowed values in the table.");
                return View(reservation);
            }
        }

        // GET: FACILITY_RESERVATION/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var reservation = db.FACILITY_RESERVATION.Find(id);
            if (reservation == null)
                return HttpNotFound();

            ViewBag.Facility_ID = new SelectList(db.FACILITies, "Facility_ID", "Facility_Name", reservation.Facility_ID);
            ViewBag.Member_ID = new SelectList(db.MEMBERs, "Member_ID", "First_Name", reservation.Member_ID);

            return View(reservation);
        }

        // POST: FACILITY_RESERVATION/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Reservation_ID,Facility_ID,Member_ID,Reservation_Date,Start_Time,End_Time,Purpose,Status,Created_Date")]
            FACILITY_RESERVATION reservation)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Facility_ID = new SelectList(db.FACILITies, "Facility_ID", "Facility_Name", reservation.Facility_ID);
                ViewBag.Member_ID = new SelectList(db.MEMBERs, "Member_ID", "First_Name", reservation.Member_ID);
                return View(reservation);
            }

            // Ensure Status is still one of the allowed values even on Edit
            if (string.IsNullOrWhiteSpace(reservation.Status))
                reservation.Status = DefaultReservationStatus;

            db.Entry(reservation).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: FACILITY_RESERVATION/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var reservation = db.FACILITY_RESERVATION.Find(id);
            if (reservation == null)
                return HttpNotFound();

            return View(reservation);
        }

        // POST: FACILITY_RESERVATION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var reservation = db.FACILITY_RESERVATION.Find(id);
            db.FACILITY_RESERVATION.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
