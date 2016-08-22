using AutomobileMaintenanceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomobileMaintenanceTracker.Repositories
{
    public class UnitOfWork
    {
        private AMTDb context = new AMTDb();
        private Repository<Car> carRepository;
        private Repository<Maintenance> maintenaceRepository;
        private Repository<Service> serviceRepository;
        private Repository<MaintenanceService> maintenanceServiceRepository;
        private Repository<CarType> carTypeRepository;
        private Repository<Make> makeRepository;
        private Repository<Model> modelRepository;
        private Repository<Technician> technicianRepository;

        public Repository<Car> CarRepository
        {
            get
            {

                if (this.carRepository == null)
                {
                    this.carRepository = new Repository<Car>(context);
                }
                return carRepository;
            }
        }

        public Repository<Maintenance> MaintenaceRepository
        {
            get
            {

                if (this.maintenaceRepository == null)
                {
                    this.maintenaceRepository = new Repository<Maintenance>(context);
                }
                return maintenaceRepository;
            }
        }

        public Repository<Service> ServiceRepository
        {
            get
            {

                if (this.serviceRepository == null)
                {
                    this.serviceRepository = new Repository<Service>(context);
                }
                return serviceRepository;
            }
        }

        public Repository<MaintenanceService> MaintenanceServiceRepository
        {
            get
            {

                if (this.maintenanceServiceRepository == null)
                {
                    this.maintenanceServiceRepository = new Repository<MaintenanceService>(context);
                }
                return maintenanceServiceRepository;
            }
        }
        public Repository<CarType> CarTypeRepository
        {
            get
            {

                if (this.carTypeRepository == null)
                {
                    this.carTypeRepository = new Repository<CarType>(context);
                }
                return CarTypeRepository;
            }
        }


        public Repository<Make> MakeRepository
        {
            get
            {

                if (this.makeRepository == null)
                {
                    this.makeRepository = new Repository<Make>(context);
                }
                return makeRepository;
            }
        }

        public Repository<Model> ModelRepository
        {
            get
            {

                if (this.modelRepository == null)
                {
                    this.modelRepository = new Repository<Model>(context);
                }
                return modelRepository;
            }
        }

        public Repository<Technician> TechnicianRepository
        {
            get
            {

                if (this.technicianRepository == null)
                {
                    this.technicianRepository = new Repository<Technician>(context);
                }
                return technicianRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
 