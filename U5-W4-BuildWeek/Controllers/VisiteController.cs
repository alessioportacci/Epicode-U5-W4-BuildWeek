 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U5_W4_BuildWeek.Models.DbModels;

namespace U5_W4_BuildWeek.Controllers
{
    [Authorize(Roles = "Admin,Veterinario")]
    public class VisiteController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        // GET: Visite
        public ActionResult Index([Bind(Include = "Microchip,Nome")] Animali animale)
        {
            List<Visite> Visite = db.Visite.ToList();

            if (animale.Microchip != null)
                Visite = Visite.Where(v => v.Animali.Microchip == animale.Microchip.Trim()).ToList();

            if (animale.Nome != null)
                Visite = Visite.Where(v => v.Animali.Nome.ToLower().Contains(animale.Nome.ToLower())).ToList();

            return View(Visite);
        }

        public ActionResult Create(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Visite visite)
        {

            if (ModelState.IsValid)
            {
                db.Visite.Add(visite);
                db.SaveChanges();
                return RedirectToAction("DettaglioAnimale", "Animali", new{ id = visite.FkAnimale });
            }

            ViewBag.Id = visite.FkAnimale;
            return View(visite);      
        }

        public ActionResult Delete(int id) 
        {
            Visite visite = db.Visite.Find(id);
            db.Visite.Remove(visite);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}