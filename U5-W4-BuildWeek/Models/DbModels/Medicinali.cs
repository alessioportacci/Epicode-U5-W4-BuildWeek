namespace U5_W4_BuildWeek.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Medicinali")]
    public partial class Medicinali
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medicinali()
        {
            MedicinaliVendite = new HashSet<MedicinaliVendite>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public bool Medicinale { get; set; }

        [Column(TypeName = "money")]
        public decimal Costo { get; set; }

        [Required]
        [StringLength(10)]
        public string Posizione { get; set; }

        [StringLength(200)]
        public string Descrizione { get; set; }

        public int? FkDitta { get; set; }

        public virtual Ditte Ditte { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicinaliVendite> MedicinaliVendite { get; set; }
    }
}
