namespace U5_W4_BuildWeek.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AggiuntoRicoveroAttivo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animali", "RicoveroAttivo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Animali", "RicoveroAttivo");
        }
    }
}
