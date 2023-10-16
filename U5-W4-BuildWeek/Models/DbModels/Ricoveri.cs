namespace U5_W4_BuildWeek.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ricoveri")]
    public partial class Ricoveri
    {
        public int Id { get; set; }

        public DateTime Data { get; set; }

        [Column(TypeName = "money")]
        public decimal Costo { get; set; }

        public bool Attivo { get; set; }

        public int FkAnimale { get; set; }

        public virtual Animali Animali { get; set; }
    }
}
