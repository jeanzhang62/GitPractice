namespace AutomobileMaintenanceTracker.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutomobileMaintenanceTracker.Models.AMTDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "AutomobileMaintenanceTracker.Models.AMTDb";
        }

        protected override void Seed(AutomobileMaintenanceTracker.Models.AMTDb context)
        {

            for (int i = 1; i < 100; i++)
            {
                context.MaintenanceServices.AddOrUpdate(y => y.Id,
                    new MaintenanceService { Id = i, MaintenanceId = new Random().Next(1, 3), Service = new Random().Next(1,3), TechnicianId = new Random().Next(1,3) });
            }

            for (int i = 1; i < 17; i++)
            {
                context.Years.AddOrUpdate(y => y.CarYear,
                    new Year { Id = i, CarYear = new Random().Next(2000, 2016).ToString() });
            }

            context.CarTypes.AddOrUpdate(m => m.CarTypeName,
                   new CarType { Id = 1, CarTypeName = "Gas" },
                   new CarType { Id = 2, CarTypeName = "Electric" },
                   new CarType { Id = 3, CarTypeName = "Diesel" }
                   );

            context.Makes.AddOrUpdate(m => m.CarMake,
                    new Make { Id = 1, CarMake = "Ford" },
                    new Make { Id = 2, CarMake = "BMW" },
                    new Make { Id = 3, CarMake = "Nissan" },
                    new Make { Id = 4, CarMake = "Mazda" },
                    new Make { Id = 5, CarMake = "Honda" }
                    );

            context.Models.AddOrUpdate(m => m.CarModel,
                  new AutomobileMaintenanceTracker.Models.Model { Id = 1, MakeId =1, CarModel = "Mustang" },
                  new Model { Id = 2, MakeId = 1, CarModel = "Focus" },
                  new Model { Id = 3, MakeId = 1, CarModel = "Exporer" }
                  );

            context.Models.AddOrUpdate(m => m.CarModel,
                new AutomobileMaintenanceTracker.Models.Model { Id = 4, MakeId = 2, CarModel = "M3" },
                new Model { Id = 5, MakeId = 2, CarModel = "M4" },
                new Model { Id = 6, MakeId = 2, CarModel = "M5" }
                );

            context.Models.AddOrUpdate(m => m.CarModel,
                new AutomobileMaintenanceTracker.Models.Model { Id = 7, MakeId = 3, CarModel = "Versa" },
                new Model { Id = 8, MakeId = 3, CarModel = "Micra" },
                new Model { Id = 9, MakeId = 3, CarModel = "Micra SV" }
                );

            context.Models.AddOrUpdate(m => m.CarModel,
                new AutomobileMaintenanceTracker.Models.Model { Id = 10, MakeId = 4, CarModel = "CX-5" },
                new Model { Id = 11, MakeId = 4, CarModel = "CX-9" },
                new Model { Id = 12, MakeId = 4, CarModel = "Mazda6" }
                );

            context.Models.AddOrUpdate(m => m.CarModel,
                new AutomobileMaintenanceTracker.Models.Model { Id = 13, MakeId = 5, CarModel = "Civic" },
                new Model { Id = 14, MakeId = 5, CarModel = "Accord" },
                new Model { Id = 15, MakeId = 5, CarModel = "CR-5" }
                );

            context.Services.AddOrUpdate(m => m.ServiceCode,
                new Service { Id = 1, Price = 1, ServiceCode = "Oil Change" },
                new Service { Id = 2, Price = 1, ServiceCode = "Air Filter" },
                new Service { Id = 3, Price = 1, ServiceCode = "Tire Rotation" }
                );
            context.Technician.AddOrUpdate(m => m.FirstName,
                new Technician { Id = 1, FirstName = "Mike", LastName = "Smith" },
                new Technician { Id = 2, FirstName = "Bob", LastName = "Blue" },
                new Technician { Id = 3, FirstName = "Joe", LastName = "Martin" }
                );

            //for (int i = 10; i < 200; i++)
            //{
            //    context.Cars.AddOrUpdate(c => c.MakeId,
            //        new Car
            //        {
            //            MakeId = i,
            //            Year = new Random().Next(2000, 2016).ToString(),
            //            ModelId = new Random().Next(1, 15),
            //            Odometer = new Random().Next(1000, 50000),
            //            Maintenances =
            //                    new List<Maintenance>
            //                    {
            //                        new Maintenance { MaintenanceDate = DateTime.Now,
            //                                          Comments ="Excellent" + i.ToString(),
            //                                          Rating = new Random().Next(1,10)
            //                                    }
            //                    }
            //        }

            //        );
            //}
        }
    }
}
