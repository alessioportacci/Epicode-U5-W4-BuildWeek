using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace U5_W4_BuildWeek.Models
{
    public class RicoveriModel
    {
        public int Id { get; set; }

        public string DataInizioRicovero { get; set; }

        [Required]
        [StringLength(70)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Foto { get; set; }

        [Required]
        [StringLength(50)]
        public string Colore { get; set; }

        public string DataNascita { get; set; }

        [StringLength(50)]
        public string Microchip { get; set; }

        public string Tipologia { get; set; }
    }
}