using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U5_W4_BuildWeek.Models.DbModels;

namespace U5_W4_BuildWeek.Controllers
{
    [Authorize(Roles = "Admin,Veterinario")]
    public class TipologiaAnimaleController : Controller
    {
        ModelDbContext db = new ModelDbContext();

        public ActionResult Tipologie()
        {
            var tipologie = db.AnimaliTipologia.ToList();
            return View(tipologie);
        }

        [HttpGet]
        public ActionResult AggiungiTipologia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AggiungiTipologia(AnimaliTipologia nuovaTipologia)
        {
            if (ModelState.IsValid)
            {
                db.AnimaliTipologia.Add(nuovaTipologia);
                db.SaveChanges();
                return RedirectToAction("Tipologie");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ModificaTipologia(int id)
        {
            var tipologia = db.AnimaliTipologia.Find(id);
            return View(tipologia);
        }

        [HttpPost]
        public ActionResult ModificaTipologia(AnimaliTipologia nuovaTipologia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nuovaTipologia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Tipologie");
            }
            return View();
        }

        public ActionResult EliminaTipologia(int id)
        {
            var tipologia = db.AnimaliTipologia.Find(id);

            if (tipologia != null)
            {
                db.AnimaliTipologia.Remove(tipologia);
                db.SaveChanges();
            }
            return RedirectToAction("Tipologie");
        }
    }
}