namespace U5_W4_BuildWeek.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MedicinaliVendite")]
    public partial class MedicinaliVendite
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Ricetta { get; set; }

        public DateTime Data { get; set; }

        public int FkUtente { get; set; }

        public int FkProdotto { get; set; }

        public virtual Medicinali Medicinali { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
