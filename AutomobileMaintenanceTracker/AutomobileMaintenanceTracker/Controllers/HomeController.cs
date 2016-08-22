using AutomobileMaintenanceTracker.Helpers;
using AutomobileMaintenanceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomobileMaintenanceTracker.Controllers
{
    public class HomeController : Controller
    {
        private CarsService carsService = new CarsService();

        public ActionResult Index()
        {
            ViewBag.Title = "Car List Retrieved From Web Api Service";
            return View(carsService.GetCars());
        }

        public ActionResult Edit(int id = 0)
        {
            Car car = carsService.GetCarById(id);

            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }


        public ActionResult Details(int id = 0)
        {
            Car car = carsService.GetCarById(id);

            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }
    }
}
