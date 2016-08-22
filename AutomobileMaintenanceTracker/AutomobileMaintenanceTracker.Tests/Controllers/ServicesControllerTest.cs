using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomobileMaintenanceTracker;
using AutomobileMaintenanceTracker.Controllers;
using AutomobileMaintenanceTracker.Tests.Features;
using System.Collections;
using AutomobileMaintenanceTracker.Models;
using System.Collections.Generic;

namespace AutomobileMaintenanceTracker.Tests.Controllers
{
    [TestClass]
    public class ServicesControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var db = new FakeAmtDb();
            db.AddSet(TestData.Services);
            ServicesController controller = new ServicesController(db);

            // Act
            ViewResult result = controller.Index() as ViewResult;
            IEnumerable<Service> model  = result.Model as IEnumerable<Service>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Service Page", result.ViewBag.Title);
        }

        [TestMethod]
        public void Create_Saves_Service_When_Valid()
        {
            // Arrange
            var db = new FakeAmtDb();
            db.AddSet(TestData.Services);
            ServicesController controller = new ServicesController(db);

            // Act
            controller.Create(new Service(), null);

            // Assert
            Assert.AreEqual(1, db.Added.Count);
            Assert.AreEqual(true, db.Saved);
        }

        [TestMethod]
        public void Create_Does_Not_Saves_Service_When_Invalid()
        {
            // Arrange
            var db = new FakeAmtDb();
            db.AddSet(TestData.Services);
            ServicesController controller = new ServicesController(db);

            // Act
            controller.ModelState.AddModelError("", "Invalid");
            controller.Create(new Service(), null);

            // Assert
            Assert.AreEqual(0, db.Added.Count);
        }
    }
}
