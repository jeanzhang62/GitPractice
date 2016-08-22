namespace AutomobileMaintenanceTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.String(),
                        MakeId = c.Int(nullable: false),
                        ModelId = c.Int(nullable: false),
                        Odometer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Maintenances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        MaintenanceDate = c.DateTime(nullable: false),
                        Comments = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.MaintenanceServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaintenanceId = c.Int(nullable: false),
                        Service = c.Int(nullable: false),
                        TechnicianId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Makes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarMake = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarModel = c.String(),
                        MakeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Makes", t => t.MakeId, cascadeDelete: true)
                .Index(t => t.MakeId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceCode = c.String(),
                        ServiceDesc = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Technicians",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Years",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarYear = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Models", "MakeId", "dbo.Makes");
            DropForeignKey("dbo.Maintenances", "CarId", "dbo.Cars");
            DropIndex("dbo.Models", new[] { "MakeId" });
            DropIndex("dbo.Maintenances", new[] { "CarId" });
            DropTable("dbo.Years");
            DropTable("dbo.Technicians");
            DropTable("dbo.Services");
            DropTable("dbo.Models");
            DropTable("dbo.Makes");
            DropTable("dbo.MaintenanceServices");
            DropTable("dbo.Maintenances");
            DropTable("dbo.Cars");
        }
    }
}
