using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U5_W4_BuildWeek.Models.DbModels;

namespace U5_W4_BuildWeek.Controllers
{
    public class HomeController : Controller
    {

        ModelDbContext db = new ModelDbContext();

        public ActionResult Index()
        {
            db.AnimaliTipologia.Add(new AnimaliTipologia
            {
                Tipologia = "Cane"
            });


            db.Animali.Add(new Animali
            {
                DataRegistrazione = DateTime.Now,
                DataInizioRicovero = DateTime.Now,
                Nome = "",
                Foto = "0",
                Colore = "0",
                DataNascita = DateTime.Now,
                Microchip = "0",
                FkTipologia = 1,

            });
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}