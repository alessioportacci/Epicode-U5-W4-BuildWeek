using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U5_W4_BuildWeek.Models.DbModels;

namespace U5_W4_BuildWeek.Controllers
{
    public class AnimaliController : Controller
    {
        ModelDbContext db = new ModelDbContext();

        public ActionResult DettaglioAnimale(int id)
        {
            var animale = db.Animali.Find(id);
            return View(animale);
        }

        [HttpGet]
        public ActionResult RegistraAnimale()
        {
            ViewBag.TipologieAnimali = GetTipologiaAnimale;
            ViewBag.Clienti = GetListaClienti;
            return View();
        }

        [HttpPost]
        public ActionResult RegistraAnimale(Animali animale)
        {
            ViewBag.TipologieAnimali = GetTipologiaAnimale;
            ViewBag.Clienti = GetListaClienti;

            if (ModelState.IsValid)
            {
                if (animale.FotoFile != null && animale.FotoFile.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/Imgs/Animali/"), animale.FotoFile.FileName);
                    animale.FotoFile.SaveAs(path);

                    animale.Foto = animale.FotoFile.FileName;
                }
                animale.DataRegistrazione = DateTime.Today;
                db.Animali.Add(animale);
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult ModificaAnimale(int id)
        {
            ViewBag.TipologieAnimali = GetTipologiaAnimale;
            ViewBag.Clienti = GetListaClienti;
            var animale = db.Animali.Find(id);
            return View(animale);
            
        }

        [HttpPost]
        public ActionResult ModificaAnimale(Animali animale)
        {
            ViewBag.TipologieAnimali = GetTipologiaAnimale;
            ViewBag.Clienti = GetListaClienti;

            if (ModelState.IsValid)
            {
                if (animale.FotoFile != null && animale.FotoFile.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/Imgs/Animali/"), animale.FotoFile.FileName);
                    animale.FotoFile.SaveAs(path);

                    animale.Foto = animale.FotoFile.FileName;
                }
                db.Entry(animale).State = EntityState.Modified;
                db.SaveChanges();
                return View(animale);
            }
            return View(animale);
        }

        public ActionResult EliminaAnimale(int id)
        {
            var animale = db.Animali.Find(id);

            if (animale != null)
            {
                db.Animali.Remove(animale);
                db.SaveChanges();
            }
            return View("Index");
        }

        #region Get Clienti / Tipologie

        public List<SelectListItem> GetListaClienti
        {
            get
            {
                List<SelectListItem> Utenti = new List<SelectListItem>();
                List<Utenti> ListaUtenti = db.Utenti.ToList();

                foreach (Utenti utente in ListaUtenti)
                {
                    SelectListItem item = new SelectListItem { Text = utente.Nome, Value = utente.Id.ToString() };
                    Utenti.Add(item);
                }
                return Utenti;
            }
        }

        public List<SelectListItem> GetTipologiaAnimale
        {
            get
            {
                List<SelectListItem> TipologiaAnimali = new List<SelectListItem>();
                List<AnimaliTipologia> ListaAnimali = db.AnimaliTipologia.ToList();

                foreach (AnimaliTipologia animale in ListaAnimali)
                {
                    SelectListItem item = new SelectListItem { Text = animale.Tipologia, Value = animale.Id.ToString() };
                    TipologiaAnimali.Add(item);
                }
                return TipologiaAnimali;
            }
        }

        #endregion
    }
}