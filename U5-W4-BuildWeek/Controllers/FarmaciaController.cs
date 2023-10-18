using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U5_W4_BuildWeek.Models.DbModels;

namespace U5_W4_BuildWeek.Controllers
{
    public class FarmaciaController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        // GET: Farmacia
        public ActionResult Index()
        {
            return View(db.Medicinali.ToList());
        }
        public ActionResult Acquista (int id )
        {
            
            return View();
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
            return View();
        }
    }
}