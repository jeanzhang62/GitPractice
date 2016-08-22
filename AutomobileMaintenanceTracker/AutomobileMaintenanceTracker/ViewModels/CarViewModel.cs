using AutomobileMaintenanceTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PagedList;
using AutomobileMaintenanceTracker.Repositories;
using System.Data.Entity;

namespace AutomobileMaintenanceTracker.ViewModels
{
    public class CarViewModel
    {
        #region properties
        public int? Id { get; set; }

        [UIHint("StringField")]
        public string Year { get; set; }

        [DisplayName("Type")]
        [UIHint("CarType")]
        public int? TypeId { get; set; }

        [DisplayName("Make")]
        [UIHint("Make")]
        public int? MakeId { get; set; }


        [DisplayName("Model")]
        [UIHint("Model")]
        public int? ModelId { get; set; }

        public int Odometer { get; set; }

        [DisplayName("Number Of Maintenance")]
        public int NumberOfMaintenance { get; set; }

        public string CarDescription { get; set; }

        public ICollection<MaintenanceViewModel> Maintenances { get; set; }
        #endregion

        static UnitOfWork unitOfWork = new UnitOfWork();

        public static object AutoComplete(string term)
        {
            return  unitOfWork.CarRepository.Get(c => c.Year.StartsWith(term))
                       .Take(10)
                        .Select(c => new
                        {
                            label = c.Year
                        });
        }

        public static IEnumerable<CarViewModel> Retrieve(string searchTerm)
        {
            //var cars = unitOfWork.CarRepository.Get(c => searchTerm == null || c.Year.StartsWith(searchTerm)).
            //    OrderByDescending(c => c.Year).Take(3).ToList();
            var cars = new AMTDb().Cars.Where(c => searchTerm == null || c.Year.StartsWith(searchTerm)).
                OrderByDescending(c =>c.Maintenances.Count()).ToList();

            List < CarViewModel> carList = new List<CarViewModel>();

            foreach (var item in cars)
            {
                carList.Add(EntityToModel(item, MaintenanceViewModel.RetrieveMaintenancesByCar(item)));
            }

            return carList;
        }

        public static CarViewModel RetrieveByCarId(int? carId)
        {
            var car = unitOfWork.CarRepository.Get(c => c.Id == carId).SingleOrDefault();

            return EntityToModel(car,  MaintenanceViewModel.RetrieveMaintenancesByCar(car));
        }

        public static CarViewModel EntityToModel(Car car, List<MaintenanceViewModel> maintenanceList)
        {
            return new CarViewModel()
            {
                Id = car.Id,
                Year = car.Year,
                Maintenances = maintenanceList,
                MakeId = car.MakeId,
                ModelId = car.ModelId,
                Odometer = car.Odometer,
                TypeId = car.TypeId
            };
        }

        public static string GetCarDescription(Car car)
        {
            var make = unitOfWork.MakeRepository.Get(c => c.Id == car.MakeId).First().CarMake;
            var model = unitOfWork.ModelRepository.Get(c => c.Id == car.ModelId).First().CarModel;
            //var carType = unitOfWork.CarTypeRepository.Get(c => c.Id == car.TypeId).First().CarTypeName;

            return car.Year + " " + make + " " + model;
        }

        public static Car ModelToEntity(CarViewModel car)
        {
            return new Car()
            {
                Id = car.Id.HasValue ? (int)car.Id: 0,
                MakeId = (int)car.MakeId,
                Year = car.Year,
                ModelId = (int)car.ModelId,
                Odometer = car.Odometer,
                TypeId =(int) car.TypeId
            };
        }

        public  List<Model> GetModelByMakeId(int? makeId)
        {
            if (makeId == null)
                return null;

            return unitOfWork.ModelRepository.Get(l => l.MakeId == makeId).ToList();          
        }

        public static bool Create(CarViewModel car)
        {
            Car aCar = CarViewModel.ModelToEntity(car);

            try
            {
               unitOfWork.CarRepository.Add(aCar);
                unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool Edit(CarViewModel car)
        {
            try
            {
                Car aCar = CarViewModel.ModelToEntity(car);
                var db = new AMTDb();
                db.Entry(aCar).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool DeleteConfirmed(int id)
        {
            try
            {
                Car car = unitOfWork.CarRepository.GetById(id);
                unitOfWork.CarRepository.Delete(car);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


    }
}