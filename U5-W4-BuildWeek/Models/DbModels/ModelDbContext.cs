using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace U5_W4_BuildWeek.Models.DbModels
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Animali> Animali { get; set; }
        public virtual DbSet<AnimaliTipologia> AnimaliTipologia { get; set; }
        public virtual DbSet<Ditte> Ditte { get; set; }
        public virtual DbSet<Medicinali> Medicinali { get; set; }
        public virtual DbSet<MedicinaliVendite> MedicinaliVendite { get; set; }
        public virtual DbSet<Ricoveri> Ricoveri { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Utenti> Utenti { get; set; }
        public virtual DbSet<Visite> Visite { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animali>()
                .HasMany(e => e.Ricoveri)
                .WithRequired(e => e.Animali)
                .HasForeignKey(e => e.FkAnimale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Animali>()
                .HasMany(e => e.Visite)
                .WithRequired(e => e.Animali)
                .HasForeignKey(e => e.FkAnimale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AnimaliTipologia>()
                .HasMany(e => e.Animali)
                .WithRequired(e => e.AnimaliTipologia)
                .HasForeignKey(e => e.FkTipologia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ditte>()
                .HasMany(e => e.Medicinali)
                .WithOptional(e => e.Ditte)
                .HasForeignKey(e => e.FkDitta);

            modelBuilder.Entity<Medicinali>()
                .Property(e => e.Costo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Medicinali>()
                .HasMany(e => e.MedicinaliVendite)
                .WithRequired(e => e.Medicinali)
                .HasForeignKey(e => e.FkProdotto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ricoveri>()
                .Property(e => e.Costo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.Animali)
                .WithOptional(e => e.Utenti)
                .HasForeignKey(e => e.FkUtente);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.MedicinaliVendite)
                .WithRequired(e => e.Utenti)
                .HasForeignKey(e => e.FkUtente)
                .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<U5_W4_BuildWeek.Models.RicoveriModel> RicoveriModels { get; set; }
    }
}
