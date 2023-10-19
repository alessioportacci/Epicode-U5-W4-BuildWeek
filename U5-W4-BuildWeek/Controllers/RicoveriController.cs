using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U5_W4_BuildWeek.Models;
using U5_W4_BuildWeek.Models.DbModels;

namespace U5_W4_BuildWeek.Controllers
{
    public class RicoveriController : Controller
    {

        ModelDbContext db = new ModelDbContext();

        public ActionResult Index()
        {
            ViewBag.Tipologie = db.AnimaliTipologia.ToList();
            return View();
        }


        public JsonResult RicoveriFilter(List<int> Tipologie)
        {
            List<RicoveriModel> ricoveri = new List<RicoveriModel>();
            List<Animali> animaliList = new List<Animali>();

            //Nel caso restituisca una lista null
            try
            {
                animaliList = db.Animali.Where(a => a.RicoveroAttivo && Tipologie.Contains(a.FkTipologia)).ToList();
            }
            catch { return Json(""); }

            if (animaliList.Count > 0)
                foreach (Animali animale in animaliList)
                    ricoveri.Add(new RicoveriModel
                    {
                        Id = animale.Id,
                        DataInizioRicovero = animale.DataInizioRicovero.Value.ToString(),
                        Nome = animale.Nome,
                        Foto = animale.Foto,
                        Colore = animale.Colore,
                        DataNascita = animale.DataNascita.HasValue ? animale.DataNascita.Value.ToString() : "-",
                        Microchip = animale.Microchip,
                        Tipologia = animale.AnimaliTipologia.Tipologia,
                    });

            return Json(ricoveri);
        }

        [Authorize(Roles = "Admin,Veterinario")]
        public JsonResult EstraiRicoveri()
        {
            List<RicoveriModel> ricoveri = new List<RicoveriModel>();
            List<Animali> animaliList = db.Animali.Where(a => a.RicoveroAttivo).ToList();
            foreach (Animali animale in animaliList)
                ricoveri.Add(new RicoveriModel
                {
                    Id = animale.Id,
                    DataInizioRicovero = animale.DataInizioRicovero.Value.ToString(),
                    Nome = animale.Nome,
                    Foto = animale.Foto,
                    Colore = animale.Colore,
                    DataNascita = animale.DataNascita.HasValue ? animale.DataNascita.Value.ToString() : "-",
                    Microchip = animale.Microchip,
                    Tipologia = animale.AnimaliTipologia.Tipologia,
                });

            return Json(ricoveri, JsonRequestBehavior.AllowGet);
        }


    }
}
