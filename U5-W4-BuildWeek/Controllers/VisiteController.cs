using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U5_W4_BuildWeek.Models.DbModels;

namespace U5_W4_BuildWeek.Controllers
{
    public class VisiteController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        // GET: Visite
        public ActionResult Index()
        {



            return View(db.Visite.ToList());
        }

        public ActionResult create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult create(Visite visite)
        {
            if (ModelState.IsValid)
            {
                db.Visite.Add(visite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
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