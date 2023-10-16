using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace U5_W4_BuildWeek.Models
{
    public class ContattaciModel
    {
        [Required(ErrorMessage = "Recapito obbligatorio")]
        public string Recapito {  get; set; }

        [Required(ErrorMessage = "Oggetto obbligatorio")]
        public string Oggetto    { get; set; }

        [Required(ErrorMessage = "Descrizione obbligatoria")]
        [Display(Name = "Descrizione del problema")]
        public string Descrizione { get; set; }
    }
}