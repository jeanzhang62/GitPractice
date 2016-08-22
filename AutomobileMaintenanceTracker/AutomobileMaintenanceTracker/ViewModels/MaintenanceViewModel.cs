using AutomobileMaintenanceTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Net.Http;
using AutomobileMaintenanceTracker.Helpers;
using PagedList;
using AutomobileMaintenanceTracker.Repositories;
using System.Data.Entity;

namespace AutomobileMaintenanceTracker.ViewModels
{
    public class MaintenanceViewModel
    {
        #region properties
        public int? Id { get; set; }

        [DisplayName("Service")]
        [UIHint("Service")]
        public int ServiceId { get; set; }

        [UIHint("Technician")]
        [DisplayName("Technician")]
        public int TechnicianId { get; set; }

        [DisplayName("Car")]
        public int CarId { get; set; }

        [DisplayName("Maintenance Date")]
        [DataType(DataType.DateTime)]
        [UIHint("DateTime")]
        public DateTime? MaintenanceDate { get; set; }

        public string Comments { get; set; }
        public int Rating { get; set; }

        [DisplayName("Number Of Service")]
        public int NumberOfService { get; set; }

        public List<ServiceViewModel> Services { get; set; }

        public string TheCarType { get; set; }

        public string CarDescription { get; set; }


        #endregion
        static Repository<Maintenance> repository;
        static UnitOfWork unitOfWork = new UnitOfWork();
     
        public static bool Create(MaintenanceViewModel maintenancevm)
        {
            try
            {
                Maintenance maintenance = MaintenanceViewModel.ModelToEntity(maintenancevm);                           
                unitOfWork.MaintenaceRepository.Add(maintenance);
                unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool Edit(MaintenanceViewModel maintenancevm)
        {
            try
            {
                Maintenance maintenance = MaintenanceViewModel.ModelToEntity(maintenancevm);
                var db = new AMTDb();
                db.Entry(maintenance).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static IEnumerable<MaintenanceViewModel> RetrieveAll()
        {
           
            var maintenances = new AMTDb().Maintenances.OrderByDescending( m => m.MaintenanceDate);
            List<MaintenanceViewModel> listMaintenance = new List<MaintenanceViewModel>();

            foreach (var item in maintenances)
            {
                List<ServiceViewModel> services = RetrieveServicesByMaintenanceId(item.Id);
                listMaintenance.Add( EntityToModel(item, services));
            }
            return listMaintenance;
        }

        public static void  RetrieveCarType(int typeId)
        {
            var TheCarType = new AMTDb().CarTypes.Where(ct => ct.Id == typeId).First().CarTypeName;

            if (TheCarType == ResponseMessage.ElectricCar)
            {
                LookupValues.ModifyService(TheCarType);
            }
            else
            {
                LookupValues.ModifyService(null);
            }

        }

        public  static CarViewModel RetrieveByCarId(int carId)
        {

            var car =  new AMTDb().Cars.Where(c => c.Id == carId).SingleOrDefault();
            RetrieveCarType(car.TypeId);
            List<MaintenanceViewModel> maintenanceList = RetrieveMaintenancesByCar(car);
            return new CarViewModel()
            {
                Id = car.Id,
                Maintenances = maintenanceList,
                MakeId = car.MakeId,
                ModelId = car.ModelId,
                Odometer = car.Odometer,
                TypeId = car.TypeId,
                Year= car.Year
            };
        }

        public static List<MaintenanceViewModel> RetrieveMaintenancesByCar(Car car)
        {
            var maintenanceList = new List<MaintenanceViewModel>();

            if(car.Maintenances != null)
            foreach (var maintenance in car.Maintenances.OrderByDescending( c => c.MaintenanceDate) )
            {
                List<ServiceViewModel> services = RetrieveServicesByMaintenanceId(maintenance.Id);
                maintenanceList.Add(EntityToModel(maintenance, services));
            }

            return maintenanceList;
        }

        public MaintenanceViewModel RetrieveByMaintenanceId(int maintenanceId)
        {
            var result =unitOfWork.MaintenaceRepository.GetById(maintenanceId);
            List<ServiceViewModel> services = RetrieveServicesByMaintenanceId(maintenanceId);
            return EntityToModel(result, services);
        }

        private static List<ServiceViewModel> RetrieveServicesByMaintenanceId(int maintenanceId)
        {
            List<int> serviceIds = unitOfWork.MaintenanceServiceRepository.Get(m => m.MaintenanceId == maintenanceId).Select(ms => ms.Service).ToList();
            List<ServiceViewModel> services = new List<ServiceViewModel>();
            if(serviceIds != null)
            foreach (var item in serviceIds)
            {
                var service = unitOfWork.ServiceRepository.Get().Where(x => x.Id == item)
                            .Select(s => new ServiceViewModel { ServiceId = s.Id, Price = s.Price, ServiceCode = s.ServiceCode }).First();
                services.Add(service);
            }
          

            return services;
        }

        public static ServiceViewModel EntityToModel(Service service)
        {
            return new ServiceViewModel()
            {
                ServiceId = service.Id,
                Price = service.Price,
                ServiceCode = service.ServiceCode
            };
        }

        public static MaintenanceViewModel EntityToModel(Maintenance maintenance, List<ServiceViewModel> services)
        {
            string carDesc =CarViewModel.GetCarDescription(unitOfWork.CarRepository.Get(c => c.Id == maintenance.CarId).First());
            return new MaintenanceViewModel()                
            {
                Id = maintenance.Id,
                CarId = maintenance.CarId,
                MaintenanceDate = maintenance.MaintenanceDate,
                Services = services,
                Rating = maintenance.Rating,
                Comments = maintenance.Comments,
                CarDescription = carDesc
            };

        }

        public static Maintenance ModelToEntity(MaintenanceViewModel maintenancevm)
        {
            return new Maintenance()
            {
                Id = maintenancevm.Id.HasValue? (int)maintenancevm.Id : 0,
                MaintenanceDate = maintenancevm.MaintenanceDate.HasValue ? maintenancevm.MaintenanceDate.Value:DateTime.MinValue,
                Comments = maintenancevm.Comments,
                Rating = maintenancevm.Rating,
                CarId = maintenancevm.CarId
                
            };
        }

        public static bool AddServiceToMaintenance(int serviceId, int maintenanceId)
        {
            try {
                var ms = new MaintenanceService()
                {
                    Service = serviceId,
                    MaintenanceId = maintenanceId
                };

                unitOfWork.MaintenanceServiceRepository.Add(ms);
                unitOfWork.Save();
                return true;

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public static bool DeleteServiceFromMaintenance(int serviceId, int maintenanceId)
        {
            try
            {
                var ms =unitOfWork.MaintenanceServiceRepository.Get(x => x.Service == serviceId && x.MaintenanceId == maintenanceId).First();
                unitOfWork.MaintenanceServiceRepository.Delete(ms);
                unitOfWork.Save();
                return true;

                return true;
            }

            catch(Exception ex)
            {
                return false;
            }

        }
    }
}