using AutomobileMaintenanceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomobileMaintenanceTracker.Helpers
{
    public class LookupValues
    {
        private static AMTDb db = new AMTDb();

        public LookupValues()
        {
            db = new AMTDb();
        }

        private static Dictionary<int, SelectListItem> _year;
        private static Dictionary<int, SelectListItem> _make;
        private static Dictionary<int, SelectListItem> _model;
        private static Dictionary<int, SelectListItem> _service;
        private static Dictionary<int, SelectListItem> _technician;
        private static Dictionary<int, SelectListItem> _carType;


        public static void Refresh(LookupTables type)
        {
            switch (type)
            {
                case LookupTables.Year:
                    _year = null;
                    break;
                case LookupTables.Make:
                    _make = null;
                    break;
                case LookupTables.Model:
                    _model = null;
                    break;
                case LookupTables.Service:
                    _service = null;
                    break;
                case LookupTables.Technician:
                    _technician = null;
                    break;
                case LookupTables.CarType:
                    _carType = null;
                    break;
            }


        }

        public static Dictionary<int, SelectListItem> Year
        {
            get { return _year ?? PopulateYear(); }
        }

        public static Dictionary<int, SelectListItem> Make
        {
            get { return _make ?? PopulateMake(); }
        }

        internal static void ModifyService(string theCarType)
        {
            _service = null;
            PopulateService(theCarType);
        }

        public static Dictionary<int, SelectListItem> Model
        {
            get { return _model ?? PopulateModel(); }
        }

        public static Dictionary<int, SelectListItem> Service
        {
            get { return _service ?? PopulateService(); }
        }

        public static Dictionary<int, SelectListItem> Technician
        {
            get { return _technician ?? PopulateTechnician(); }
        }

        public static Dictionary<int, SelectListItem> CarType
        {
            get { return _carType ?? CarTypeList(); }
        }

        private static Dictionary<int, SelectListItem> PopulateYear()
        {
            _year = db.Years.ToDictionary(
                y => y.Id,
                    t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.CarYear
                    });
            return _year;
        }

        private static Dictionary<int, SelectListItem> PopulateMake()
        {
            _make = db.Makes.ToDictionary(
                y => y.Id,
                    t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.CarMake
                    });

            return _make;
        }

        private static Dictionary<int, SelectListItem> PopulateModel()
        {
            _model = db.Models.ToDictionary(
                y => y.Id,
                    t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.CarModel
                    });
            return _model;
        }

        private static Dictionary<int, SelectListItem> PopulateService(string cartype = null )
        {
            _service = db.Services
                  .Where(c => cartype == null || !c.ServiceCode.StartsWith("Oil Change"))
                .ToDictionary(
                y => y.Id,
                    t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.ServiceCode
                    });
            return _service;
        }

        private static Dictionary<int, SelectListItem> PopulateTechnician()
        {
            _technician = db.Technician.ToDictionary(
                y => y.Id,
                    t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.FirstName +"," + t.LastName
                    });
            return _technician;
        }

        private static Dictionary<int, SelectListItem> CarTypeList()
        {
            var _carType = db.CarTypes.ToDictionary(
                y => y.Id,
                    t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.CarTypeName
                    });

            return _carType;
        }
    }

}
