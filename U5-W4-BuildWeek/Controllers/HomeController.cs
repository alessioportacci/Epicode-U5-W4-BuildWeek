using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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


        public ActionResult Contattaci()
        {
            return View();
        }


        public ActionResult CercaAnimale()
        {
            return View();
        }

        public int CercaAnimaleByChip(string Chip)
        {
            Animali animale = db.Animali.Where(a => a.Microchip == Chip).FirstOrDefault();

            if (animale == null)
                return 0;

            return animale.Id;
        }    


        #region Login / Register

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Username,Password")]Utenti utente)
        {
            if( db.Utenti.Where(u => u.Username == utente.Username && u.Password == utente.Password).Count() > 0)
            {
                Session["IdUtente"] = utente.Id;
                FormsAuthentication.SetAuthCookie(utente.Username, true);
                return RedirectToAction("Index");
            }
            return View();
        }


        public ActionResult Logout() 
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Exclude = "Ruolo")]Utenti utente)
        {
            if (db.Utenti.Where(u => u.Username == utente.Username).Count() > 0)
            {
                ViewBag.Errore = "Username già in uso da un altro utente";
                return View();
            }

            db.Utenti.Add(new Utenti
            {
                Nome = utente.Nome,
                CF = utente.CF,
                Telefono = utente.Telefono,
                Email = utente.Email,
                Username = utente.Username,
                Password = utente.Password,
                Ruolo = "User",
            });
            db.SaveChanges();

            Login(utente);

            return RedirectToAction("Index");
        }

        #endregion

    }
}