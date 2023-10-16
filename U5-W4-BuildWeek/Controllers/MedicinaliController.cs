using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using U5_W4_BuildWeek.Models.DbModels;

namespace U5_W4_BuildWeek.Controllers
{
    public class MedicinaliController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        // GET: Medicinali
        public ActionResult Index()
        {
            var medicinali = db.Medicinali.ToList();
            return View(medicinali);
        }


        //Create Medicinali
        public ActionResult Create()
        {
            var ditte = db.Ditte.ToList();
            ViewBag.DitteList = new SelectList(ditte, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medicinali m)
        {
            if (ModelState.IsValid)
            {
           
                if (db.Medicinali.Any(existing => existing.Posizione == m.Posizione))
                {
                    ModelState.AddModelError("Posizione", "La Posizione inserita è già occupata.");
                    return View();
                }

                db.Medicinali.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


        //Modifica Medicinali
        public ActionResult Modifica(int id) 
        {
            var ditte = db.Ditte.ToList();
            ViewBag.DitteList = new SelectList(ditte, "Id", "Nome");
            var m =  db.Medicinali.Find(id);
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modifica(Medicinali m)
        {
            if (ModelState.IsValid)
            {
                
                var medicinale = db.Medicinali.Find(m.Id);
                if (medicinale != null)
                {
                  
                    medicinale.Nome = m.Nome;
                    medicinale.Costo = m.Costo;
                    medicinale.Descrizione = m.Descrizione;
                    medicinale.Medicinale = m.Medicinale;
                    medicinale.Posizione = m.Posizione;

                    if (db.Medicinali.Any(existing => existing.Posizione == m.Posizione))
                    {
                        ModelState.AddModelError("Posizione", "La Posizione inserita è già occupata.");
                        return View();
                    }
                    db.Entry(medicinale).State = EntityState.Modified;

          
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    
                }
            }
            return View(m);
        }


        //Delete Medicinali
        public ActionResult Delete (int id)
        {
            Medicinali m = db.Medicinali.Find(id);
            db.Medicinali.Remove(m); 
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Creazione Ditta
        public ActionResult CreaDitta() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreaDitta(Ditte d)
        {
            if (ModelState.IsValid)
            {

              

                db.Ditte.Add(d);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}