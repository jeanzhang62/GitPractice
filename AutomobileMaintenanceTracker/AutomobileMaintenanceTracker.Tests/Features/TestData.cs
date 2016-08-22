using AutomobileMaintenanceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileMaintenanceTracker.Tests.Features
{
    class TestData
    {
        public static IQueryable<Service> Services
        {
            get
            {
                var services = new List<Service>();
                for (int i = 0; i < 10; i++)
                {
                    var service = new Service();
                    services.Add(service);
                }

                return services.AsQueryable();
            }
        }
    }
}
