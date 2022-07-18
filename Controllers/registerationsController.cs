using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class registerationsController : Controller
    {
        private lindaaEntities db = new lindaaEntities();

        // GET: registerations
        public ActionResult Index()
        {
            var registerations = db.registerations.Include(r => r.batch).Include(r => r.course);
            return View(registerations.ToList());
        }

        // GET: registerations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registeration registeration = db.registerations.Find(id);
            if (registeration == null)
            {
                return HttpNotFound();
            }
            return View(registeration);
        }

        // GET: registerations/Create
        public ActionResult Create()
        {
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1");
            ViewBag.course_id = new SelectList(db.courses, "id", "course1");
            return View();
        }

        // POST: registerations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id")] registeration registeration)
        {
            if (ModelState.IsValid)
            {
                db.registerations.Add(registeration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registeration.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course1", registeration.course_id);
            return View(registeration);
        }

        // GET: registerations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registeration registeration = db.registerations.Find(id);
            if (registeration == null)
            {
                return HttpNotFound();
            }
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registeration.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course1", registeration.course_id);
            return View(registeration);
        }

        // POST: registerations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id")] registeration registeration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registeration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", registeration.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course1", registeration.course_id);
            return View(registeration);
        }

        // GET: registerations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registeration registeration = db.registerations.Find(id);
            if (registeration == null)
            {
                return HttpNotFound();
            }
            return View(registeration);
        }

        // POST: registerations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            registeration registeration = db.registerations.Find(id);
            db.registerations.Remove(registeration);
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
