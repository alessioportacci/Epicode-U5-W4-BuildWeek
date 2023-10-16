namespace U5_W4_BuildWeek.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AggiornamentoUtenti : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animali",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataRegistrazione = c.DateTime(nullable: false),
                        DataInizioRicovero = c.DateTime(),
                        Nome = c.String(nullable: false, maxLength: 70),
                        Foto = c.String(maxLength: 50),
                        Colore = c.String(nullable: false, maxLength: 50),
                        DataNascita = c.DateTime(),
                        Microchip = c.String(maxLength: 50),
                        FkTipologia = c.Int(nullable: false),
                        FkUtente = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnimaliTipologia", t => t.FkTipologia)
                .ForeignKey("dbo.Utenti", t => t.FkUtente)
                .Index(t => t.FkTipologia)
                .Index(t => t.FkUtente);
            
            CreateTable(
                "dbo.AnimaliTipologia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tipologia = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ricoveri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Costo = c.Decimal(nullable: false, storeType: "money"),
                        Attivo = c.Boolean(nullable: false),
                        FkAnimale = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animali", t => t.FkAnimale)
                .Index(t => t.FkAnimale);
            
            CreateTable(
                "dbo.Utenti",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 200),
                        CF = c.String(nullable: false, maxLength: 16),
                        Telefono = c.String(maxLength: 13),
                        Email = c.String(maxLength: 100),
                        Ruolo = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedicinaliVendite",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ricetta = c.String(maxLength: 50),
                        Data = c.DateTime(nullable: false),
                        FkUtente = c.Int(nullable: false),
                        FkProdotto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicinali", t => t.FkProdotto)
                .ForeignKey("dbo.Utenti", t => t.FkUtente)
                .Index(t => t.FkUtente)
                .Index(t => t.FkProdotto);
            
            CreateTable(
                "dbo.Medicinali",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Medicinale = c.Boolean(nullable: false),
                        Costo = c.Decimal(nullable: false, storeType: "money"),
                        Posizione = c.String(nullable: false, maxLength: 10),
                        Descrizione = c.String(maxLength: 200),
                        FkDitta = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ditte", t => t.FkDitta)
                .Index(t => t.FkDitta);
            
            CreateTable(
                "dbo.Ditte",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Recapito = c.String(maxLength: 13),
                        Indirizzo = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Visite",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataVisita = c.DateTime(nullable: false),
                        EsameObbiettivo = c.String(nullable: false, maxLength: 50),
                        DescrizioneCura = c.String(maxLength: 200),
                        FkAnimale = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animali", t => t.FkAnimale)
                .Index(t => t.FkAnimale);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visite", "FkAnimale", "dbo.Animali");
            DropForeignKey("dbo.MedicinaliVendite", "FkUtente", "dbo.Utenti");
            DropForeignKey("dbo.MedicinaliVendite", "FkProdotto", "dbo.Medicinali");
            DropForeignKey("dbo.Medicinali", "FkDitta", "dbo.Ditte");
            DropForeignKey("dbo.Animali", "FkUtente", "dbo.Utenti");
            DropForeignKey("dbo.Ricoveri", "FkAnimale", "dbo.Animali");
            DropForeignKey("dbo.Animali", "FkTipologia", "dbo.AnimaliTipologia");
            DropIndex("dbo.Visite", new[] { "FkAnimale" });
            DropIndex("dbo.Medicinali", new[] { "FkDitta" });
            DropIndex("dbo.MedicinaliVendite", new[] { "FkProdotto" });
            DropIndex("dbo.MedicinaliVendite", new[] { "FkUtente" });
            DropIndex("dbo.Ricoveri", new[] { "FkAnimale" });
            DropIndex("dbo.Animali", new[] { "FkUtente" });
            DropIndex("dbo.Animali", new[] { "FkTipologia" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Visite");
            DropTable("dbo.Ditte");
            DropTable("dbo.Medicinali");
            DropTable("dbo.MedicinaliVendite");
            DropTable("dbo.Utenti");
            DropTable("dbo.Ricoveri");
            DropTable("dbo.AnimaliTipologia");
            DropTable("dbo.Animali");
        }
    }
}
