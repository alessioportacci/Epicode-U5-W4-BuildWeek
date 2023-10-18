using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U5_W4_BuildWeek.Models;
using U5_W4_BuildWeek.Models.DbModels;

namespace U5_W4_BuildWeek.Controllers
{
    [Authorize(Roles = "Admin,Veterinario")]
    public class RicoveriController : Controller
    {

        ModelDbContext db = new ModelDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Tipologie = db.AnimaliTipologia.ToList();
            return View();
        }

        [AllowAnonymous]
        public JsonResult RicoveriFilter(List<int> Tipologie)
        {
            List<RicoveriModel> ricoveri = new List<RicoveriModel>();
            List<Animali> animaliList = new List<Animali>();

            //Nel caso restituisca una lista null
            try
            {
                animaliList = db.Animali.Where(a => a.DataInizioRicovero != null && Tipologie.Contains(a.FkTipologia)).ToList();
            }
            catch { return Json(""); }

            if (animaliList.Count > 0)
                foreach (Animali animale in animaliList)
                    ricoveri.Add(new RicoveriModel
                    {
                        Id = animale.Id,
                        DataInizioRicovero = animale.DataInizioRicovero,
                        Nome = animale.Nome,
                        Foto = animale.Foto,
                        Colore = animale.Colore,
                        DataNascita = animale.DataNascita,
                        Microchip = animale.Microchip,
                        FkTipologia = animale.FkTipologia,
                    });

            return Json(ricoveri);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {

            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
