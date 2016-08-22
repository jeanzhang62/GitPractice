using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomobileMaintenanceTracker.Models;

namespace AutomobileMaintenanceTracker.Controllers
{
    public class ServicesController : Controller
    {
        private IAMTDb db;

        public ServicesController()
        {
            db = new AMTDb();
        }

        public ServicesController(IAMTDb _db)
        {
            db = _db;
        }
        // GET: Services
        public ActionResult Index()
        {
            ViewBag.Title = "Service Page";
            return View(db.Query<Service>().ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var model= db.Query<Service>.Find(id);
            Service service = db.Get<Service>((int)id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Service service, int? maintenanceId)
        {

            if (ModelState.IsValid)
            {
                if (maintenanceId.HasValue)
                {
                    var ms = new MaintenanceService()
                    {
                        MaintenanceId =(int) maintenanceId,
                        Service = service.Id
                    };

                    //db.MaintenanceServices.Add(ms);
                    db.Add<MaintenanceService>(ms);
                }

                db.Add<Service>(service);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }

        //GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Get<Service>((int)id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ServiceCode,ServiceDesc,Price")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Update<Service>(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

       // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Get<Service>((int)id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Get<Service>((int)id);
            db.Remove(service);
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
