using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomobileMaintenanceTracker.Helpers
{
    public static class ResponseMessage
    {
        public const string NoData = "No Data";
        public const string SucceededMessage = "Succeeded Message";
        public const string FailedMessage = "Failed Message";
        public const string CreateActionMessage = "Create has been succeeded.";
        public const string EditActionMessage = "Edit has been succeeded.";
        public const string DeleteActionMessage = "Delete has been succeeded.";
        public const string ErrorMessage = "There are errors";
        public const string ElectricCar = "Electric";
    }
}