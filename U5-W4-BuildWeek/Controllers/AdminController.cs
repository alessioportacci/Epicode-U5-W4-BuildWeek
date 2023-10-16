using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U5_W4_BuildWeek.Models.DbModels;

namespace U5_W4_BuildWeek.Controllers
{
    public class AdminController : Controller
    {

        ModelDbContext db = new ModelDbContext();
        // GET: Admin
        public ActionResult Index()
        {
           


            return View(db.Utenti.ToList());
        }

        public ActionResult Edit(int id)
        {


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
            db.Utenti.Remove(utenti);
            db.SaveChanges();






            return RedirectToAction("Index");



        }
       

    }
}