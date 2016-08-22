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
using PagedList;
using AutomobileMaintenanceTracker.Helpers;

namespace AutomobileMaintenanceTracker.Controllers
{
    public class CarsController : Controller
    {

        List<SelectListItem> makes;
        List<SelectListItem> models;

        public CarsController()
        {
            makes = new List<SelectListItem>();
            models = new List<SelectListItem>();

        }

        public ActionResult AutoComplete(string term)
        {
            var model = CarViewModel.AutoComplete(term);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        // GET: Cars
        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            var model = CarViewModel.Retrieve(searchTerm).ToPagedList(page, 5);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Cars", model);
            }
                  
            return View(model);
        }


        // GET: Cars/Create
        public ActionResult Create()
        {
            return View("Edit");
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarViewModel car)
        {
            if(CarViewModel.Create(car))
            {
                TempData.Clear();
                TempData.Add(ResponseMessage.SucceededMessage, ResponseMessage.CreateActionMessage);

                return RedirectToAction("Index");
            }
             return View();
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarViewModel car =CarViewModel.RetrieveByCarId(id);

            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarViewModel car)
        {

            if(CarViewModel.Edit(car))
            {
                TempData.Clear();
                TempData.Add(ResponseMessage.SucceededMessage, ResponseMessage.EditActionMessage);
                return RedirectToAction("Index");
            }
          else 
            {
                return View(car);
            }
        }      

        public ActionResult DeleteConfirmed(int id)
        {
            if(CarViewModel.DeleteConfirmed(id))
            {
                TempData.Clear();
                TempData.Add(ResponseMessage.SucceededMessage, ResponseMessage.DeleteActionMessage);

                return RedirectToAction("Index");
            }
            return View();
        }      

        public JsonResult GetModelByMakeId(int? makeId)
        {
            var carmodel = new CarViewModel();
            var model = carmodel.GetModelByMakeId(makeId);
            var modelData = model.Select(m => new SelectListItem()
            {
                Text = m.CarModel,
                Value = m.Id.ToString(),
            });
            return Json(modelData, JsonRequestBehavior.AllowGet);
        }

       

      

      
    }
}
