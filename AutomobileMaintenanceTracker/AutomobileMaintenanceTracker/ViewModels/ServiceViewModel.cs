using AutomobileMaintenanceTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomobileMaintenanceTracker.ViewModels
{
    public class ServiceViewModel
    {
        #region properties

        [DisplayName("Service")]
        [UIHint("Service")]
        public int ServiceId { get; set; }

        public decimal Price { get; set; }

        [DisplayName("Service Code")]
        public string ServiceCode { get; set; }
        #endregion
    }
}