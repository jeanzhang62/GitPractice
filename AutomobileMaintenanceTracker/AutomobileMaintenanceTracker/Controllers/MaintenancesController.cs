using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomobileMaintenanceTracker.Models;
using AutomobileMaintenanceTracker.ViewModels;
using AutomobileMaintenanceTracker.Helpers;
using PagedList;

namespace AutomobileMaintenanceTracker.Controllers
{
    public class MaintenancesController : Controller
    {
        private AMTDb db = new AMTDb();

        public ActionResult List(int page = 1)
        {
             var model = MaintenanceViewModel.RetrieveAll().ToPagedList(page, 10);

            if (Request.IsAjaxRequest())
            {
                return PartialView("__MaintenanceList", model);
            }

            return View(model);
        }

        // GET: Maintenances
        public ActionResult Index([Bind(Prefix ="id")]int carId)
        {
            CarViewModel carViewModel = null;
            int? currentCarId = 0;

            if (carId == 0)
            {
                carViewModel = Session["CurrentUrl"] as CarViewModel;
                currentCarId = carViewModel.Id;
            }
           
             carViewModel = MaintenanceViewModel.RetrieveByCarId(carId);

            if (carViewModel != null)
            {
               Session["TheCarId"] = carId;
               Session["CurrentUrl"] = carViewModel;
               Session["NumberOfMaintenances"] = carViewModel.Maintenances.Count();

                return View(carViewModel);

            }
            return HttpNotFound();
        }

        // GET: Maintenances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintenance maintenance = db.Maintenances.Find(id);
            if (maintenance == null)
            {
                return HttpNotFound();
            }
            return View(maintenance);
        }

        // GET: Maintenances/Create
        public ActionResult Create(int carId, string carMakeModel)
        {
            return View("Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MaintenanceViewModel maintenancevm)
        {
            if(MaintenanceViewModel.Create(maintenancevm))
            {
                TempData.Clear();
                TempData.Add(ResponseMessage.SucceededMessage, ResponseMessage.CreateActionMessage);
                return RedirectToAction("Index", "Maintenances/Index/" + (int)Session["TheCarId"]);
            }
            return View();       
        }    

        // GET: Maintenances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceViewModel maintenance = new MaintenanceViewModel().RetrieveByMaintenanceId((int)id);
            if (maintenance == null)
            {
                return HttpNotFound();
            }
            return View(maintenance);
        }

        // POST: Maintenances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( MaintenanceViewModel maintenancevm)
        {
            if (MaintenanceViewModel.Edit(maintenancevm))
            {
                TempData.Clear();
                TempData.Add(ResponseMessage.SucceededMessage, ResponseMessage.EditActionMessage);
                return RedirectToAction("Index", "Maintenances/Index/" + (int)Session["TheCarId"]);
            }
            return View();
        }

        // GET: Maintenances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintenance maintenance = db.Maintenances.Find(id);
            if (maintenance == null)
            {
                return HttpNotFound();
            }

            TempData.Clear();
            TempData.Add(ResponseMessage.SucceededMessage, ResponseMessage.DeleteActionMessage);

            return View(maintenance);
        }

        // POST: Maintenances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Maintenance maintenance = db.Maintenances.Find(id);
            TempData.Clear();

            db.Maintenances.Remove(maintenance);
            db.SaveChanges();
            TempData.Add(ResponseMessage.SucceededMessage, ResponseMessage.DeleteActionMessage);

            return RedirectToAction("Index", "Maintenances/Index/" + (int)Session["TheCarId"]);
        }

        public ActionResult AddServiceToMaintenance(int maintenanceId, int serviceId = 0)
        {
            if (serviceId != 0 && MaintenanceViewModel.AddServiceToMaintenance(serviceId, maintenanceId))
            {
                TempData.Clear();
                TempData.Add(ResponseMessage.SucceededMessage, ResponseMessage.CreateActionMessage);
            };

            return View();
        }

        public ActionResult DeleteServiceFromMaintenance(int serviceId, int maintenanceId)
        {
            if (MaintenanceViewModel.DeleteServiceFromMaintenance(serviceId, maintenanceId))
            {
                TempData.Clear();
                TempData.Add(ResponseMessage.SucceededMessage, ResponseMessage.DeleteActionMessage);
                return RedirectToAction("Index", "Maintenances/Index/" + (int)Session["TheCarId"]);

            };
            return null;

        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
