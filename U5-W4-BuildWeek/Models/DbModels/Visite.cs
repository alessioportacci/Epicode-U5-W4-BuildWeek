namespace U5_W4_BuildWeek.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visite")]
    public partial class Visite
    {
        public int Id { get; set; }

        public DateTime DataVisita { get; set; }

        [Required]
        [StringLength(50)]
        public string EsameObbiettivo { get; set; }

        [StringLength(200)]
        public string DescrizioneCura { get; set; }

        public int FkAnimale { get; set; }

        public virtual Animali Animali { get; set; }
    }
}
