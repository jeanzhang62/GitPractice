using AutomobileMaintenanceTracker.Models;
using AutomobileMaintenanceTracker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace AutomobileMaintenanceTracker.Controllers
{
    

    public class CarsApiController : ApiController
    {
        IEnumerable<Car> listCar = new List<Car>();
        static UnitOfWork unitOfWork = new UnitOfWork();

        // GET api/Car
        public CarsApiController()
        {
           

            listCar = unitOfWork.CarRepository.Get();
                //new Car().GetCars();
        }

        public IEnumerable<Car> Get()
        {
            return listCar;
        }

        // GET api/Car/5
        public Car Get(int id)
        {


            return listCar.Where(c => c.Id == id).SingleOrDefault();
        }

        // POST api/values
        public void Post([FromBody]Car value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Car value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
