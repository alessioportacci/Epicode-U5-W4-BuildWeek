namespace U5_W4_BuildWeek.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Animali")]
    public partial class Animali
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animali()
        {
            Ricoveri = new HashSet<Ricoveri>();
            Visite = new HashSet<Visite>();
        }

        public int Id { get; set; }

        public DateTime DataRegistrazione { get; set; }

        public DateTime? DataInizioRicovero { get; set; }

        [DefaultValue(false)]
        public bool RicoveroAttivo { get; set; }

        [Required]
        [StringLength(70)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Foto { get; set; }

        [NotMapped]
        public HttpPostedFileBase FotoFile { get; set; }

        [Required]
        [StringLength(50)]
        public string Colore { get; set; }

        public DateTime? DataNascita { get; set; }

        [StringLength(50)]
        public string Microchip { get; set; }

        public int FkTipologia { get; set; }

        public int? FkUtente { get; set; }

        public virtual AnimaliTipologia AnimaliTipologia { get; set; }

        public virtual Utenti Utenti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ricoveri> Ricoveri { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visite> Visite { get; set; }
    }
}
