namespace AutomobileMaintenanceTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcartypetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarTypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cars", "TypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "TypeId");
            DropTable("dbo.CarTypes");
        }
    }
}
