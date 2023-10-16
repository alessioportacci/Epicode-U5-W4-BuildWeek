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
    public class HomeController : Controller
    {
        ModelDbContext db = new ModelDbContext();

        public List<SelectListItem> ListaClienti
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

        public List<SelectListItem> TipologiaAnimale
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

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistraAnimale()
        {
            ViewBag.TipologieAnimali = TipologiaAnimale;
            ViewBag.Clienti = ListaClienti;
            return View();
        }

        [HttpPost]
        public ActionResult RegistraAnimale(Animali animale)
        {
            ViewBag.TipologieAnimali = TipologiaAnimale;
            ViewBag.Clienti = ListaClienti;

            if (ModelState.IsValid)
            {
                if (animale.FotoFile != null && animale.FotoFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(animale.FotoFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/FotoAnimali/"), fileName);
                    animale.FotoFile.SaveAs(path);

                    animale.Foto = fileName;
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
            ViewBag.TipologieAnimali = TipologiaAnimale;
            ViewBag.Clienti = ListaClienti;
            var animale = db.Animali.Find(id);
            return View(animale);
            
        }

        [HttpPost]
        public ActionResult ModificaAnimale(Animali animale)
        {
            ViewBag.TipologieAnimali = TipologiaAnimale;
            ViewBag.Clienti = ListaClienti;

            if (ModelState.IsValid)
            {
                if (animale.FotoFile != null && animale.FotoFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(animale.FotoFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/"), fileName);
                    animale.FotoFile.SaveAs(path);

                    animale.Foto = fileName;
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