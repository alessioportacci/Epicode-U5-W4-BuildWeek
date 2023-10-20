using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U5_W4_BuildWeek.Models.DbModels;

namespace U5_W4_BuildWeek.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        ModelDbContext db = new ModelDbContext();


        public ActionResult Index()
        {
            return View(db.Utenti.ToList());
        }


        public ActionResult Edit(int id)
        {
            List<SelectListItem> Ruoli = new List<SelectListItem>();
            Ruoli.Add(new SelectListItem() { Text = "Utente", Value = "User" });
            Ruoli.Add(new SelectListItem() { Text = "Amministratore", Value = "Admin" });
            Ruoli.Add(new SelectListItem() { Text = "Veterinario", Value = "Veterinario" });
            Ruoli.Add(new SelectListItem() { Text = "Farmacista", Value = "Farmacista" });
            this.ViewBag.Ruoli = new SelectList(Ruoli, "Value", "Text");

            return View(db.Utenti.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Utenti utente)
        {
            db.Entry(utente).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("index");
        }


        public ActionResult Details(int id)
        {
            return View(db.Utenti.Find(id));
        }


       public ActionResult Delete(int id)
        {
            Utenti utenti = db.Utenti.Find(id);

            //Cancella vendite
            List<MedicinaliVendite> Vendite = db.MedicinaliVendite.Where(v => v.FkUtente == id).ToList();
            foreach (MedicinaliVendite ordine in Vendite)
                db.MedicinaliVendite.Remove(ordine);

            //Cancella animali
            List<Animali> Animali = db.Animali.Where(a => a.FkUtente == id).ToList();
            foreach (Animali animale in Animali)
            {
                List<Visite> Visite = db.Visite.Where(vi => vi.FkAnimale == animale.Id).ToList();
                //Cancella visita
                foreach (Visite visita in Visite)
                    db.Visite.Remove(visita);
                db.Animali.Remove(animale);
            }

            db.Utenti.Remove(utenti);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Reset(int id)
        {
            Utenti user = db.Utenti.Find(id);
            user.Password = "password123!";
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}