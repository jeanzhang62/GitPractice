using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomobileMaintenanceTracker.Models
{

    public class CarType
    {
        public int Id { get; set; }
        public string CarTypeName { get; set; }

        public List<SelectListItem> GetListYear()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "Gas" },
                new SelectListItem {Value = "2", Text = "Electric" },
                new SelectListItem {Value = "3", Text = "Diesel" }
            };
        }
    }
    public class Year
    {
        public int Id { get; set; }
        public string CarYear { get; set; }

        public List<SelectListItem> GetListYear()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "2016" },
                new SelectListItem {Value = "2", Text = "2015" },
                new SelectListItem {Value = "3", Text = "2014" }
            };
        }
    }

    public class Make
    {
        public int Id { get; set; }
        public string CarMake { get; set; }

        public ICollection<Model> Models { get; set; }

        public List<SelectListItem> GetListMake()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "Ford" },
                new SelectListItem {Value = "2", Text = "BMW" },
                new SelectListItem {Value = "3", Text = "Mazda" }
            };
        }
    }

    public class Model
    {
        public int Id { get; set; }
        public string CarModel { get; set; }
        public int MakeId { get; set; }

        public List<SelectListItem> GetListModel()
        {
            var listModelOfFord = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "Mustang" },
                new SelectListItem {Value = "2", Text = "Explorer" },
                new SelectListItem {Value = "3", Text = "Focus" }
            };

            var listModelOfBMW = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "M3" },
                new SelectListItem {Value = "2", Text = "M4" },
                new SelectListItem {Value = "3", Text = "M5" }
            };

            var listModelOfMazda = new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "CX-5" },
                new SelectListItem {Value = "2", Text = "CX-9" },
                new SelectListItem {Value = "3", Text = "Mazda6" }
            };

            var listMake = new Make().GetListMake();
            var s = listMake[new Random().Next(listMake.Count())];

            return s.Value == "1" ? listModelOfFord : s.Value == "2" ? listModelOfBMW : listModelOfMazda;
        }
    }

    public class Car
    {
        public int Id { get; set; }       
        public string Year { get; set; }
        public int TypeId { get; set; }
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public int Odometer { get; set; }
        public virtual ICollection<Maintenance> Maintenances { get; set; }

        public List<Car> GetCars()
        {
            List<Car> listCar = new List<Car>();
            for (int i = 1; i <= 10; i++)
            {
                listCar.Add(new Car
                {
                    Id = i,
                    Year = new Random(DateTime.Now.Millisecond).Next(2014, 2016).ToString(),
                    MakeId = new Random(DateTime.Now.Millisecond).Next(1, 3),
                    ModelId = new Random(DateTime.Now.Millisecond).Next(1, 3),
                    Odometer = new Random(DateTime.Now.Millisecond).Next(1000, 5000),
                    Maintenances = new Maintenance().GetListMaintenance().Where(m => m.CarId == i).ToList()
                });
            }

            return listCar;
        }
      

    }

    public class Service
    {
        public int Id { get; set; }

        [DisplayName("Service Code")]
        public string ServiceCode { get; set; }

        [DisplayName("Service Description")]
        public string ServiceDesc { get; set; }
        public decimal Price { get; set; }

        public List<SelectListItem> GetListService()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Value = "1", Text = "Oil Change" },
                new SelectListItem {Value = "2", Text = "Tire Rotation" },
                new SelectListItem {Value = "3", Text = "Air Filter" }
            };
        }
    }

    public class Maintenance
    {
        public int Id { get; set; }

        [DisplayName("Car")]
        public int CarId { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Maintenance Date")]
        public DateTime MaintenanceDate { get; set; }
        public string Comments { get; set; }
        public int Rating { get; set; }

        //[DisplayName("Service")]
        //[UIHint("Service")]
        //public int ServiceId { get; set; }

        //[UIHint("Technician")]
        //[DisplayName("Technician")]
        //public int TechnicianId { get; set; }

        public List<Maintenance> GetListMaintenance()
        {
            List<Maintenance> listMaintenance = new List<Maintenance>();
            for (int i = 1; i <= 10; i++)
            {
                listMaintenance.Add(new Maintenance
                {
                    Id = i,
                    CarId = new Random().Next(1, 10),
                    MaintenanceDate = DateTime.Now,
                    Comments = "Excellent Service"
                });
            }

            return listMaintenance;
        }
    }

    public class MaintenanceService
    {
        public int Id { get; set; }
        public int MaintenanceId { get; set; }

        [DisplayName("Service")]
        [UIHint("Service")]
        public int Service { get; set; }

        [DisplayName("Technician")]
        [UIHint("Technician")]
        public int TechnicianId { get; set; }
    }

    public class Technician
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}