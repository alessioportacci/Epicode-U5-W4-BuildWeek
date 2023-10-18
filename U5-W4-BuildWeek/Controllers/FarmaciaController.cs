using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U5_W4_BuildWeek.Models.DbModels;

namespace U5_W4_BuildWeek.Controllers
{
    [Authorize]
    public class FarmaciaController : Controller
    {
        ModelDbContext db = new ModelDbContext();

        public ActionResult Index()
        {
            return View(db.Medicinali.ToList());
        }
        public ActionResult Acquista (int id = 5 )
        {
            Medicinali medicinale = db.Medicinali.Find(id);

          
            ViewBag.NomeMedicinale = medicinale.Nome;
            ViewBag.PrezzoMedicinale = medicinale.Costo;
            return PartialView(db.MedicinaliVendite.Find(id));
        }

        [HttpPost]
        public ActionResult Acquista( int id, string codiceFiscale, string ricetta)
        {
            Medicinali medicinale = db.Medicinali.Find(id);
            Utenti user = db.Utenti.FirstOrDefault(u => u.CF == codiceFiscale);
            MedicinaliVendite vendita = new MedicinaliVendite
            {
                FkProdotto = medicinale.Id,
                Ricetta = ricetta,
                Data = DateTime.Now, 
                FkUtente = user.Id
            };
            db.MedicinaliVendite.Add(vendita);
            db.SaveChanges();
            return RedirectToAction ("Index");
        }
    }
}