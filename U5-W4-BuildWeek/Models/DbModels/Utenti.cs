namespace U5_W4_BuildWeek.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Utenti")]
    public partial class Utenti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utenti()
        {
            Animali = new HashSet<Animali>();
            MedicinaliVendite = new HashSet<MedicinaliVendite>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Username obbligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Inserire una password valida")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Nome obbligatorio")]
        [StringLength(200)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Codice fiscale obbligatorio")]
        [Display(Name = "Codice Fiscale")]
        [StringLength(16)]
        public string CF { get; set; }

        [StringLength(13)]
        [Display(Name = "Numero di telefono")]
        public string Telefono { get; set; }

        [StringLength(100)]
        [Display(Name = "Indirizzo Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Ruolo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Animali> Animali { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicinaliVendite> MedicinaliVendite { get; set; }
    }
}
