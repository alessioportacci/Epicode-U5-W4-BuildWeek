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
            
                    ViewBag.DitteList = new SelectList(db.Ditte.ToList(), "Id", "Nome");
                    return View(m); 
                }

                db.Medicinali.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

      
            ViewBag.DitteList = new SelectList(db.Ditte.ToList(), "Id", "Nome");
            return View(m);
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

                        ViewBag.DitteList = new SelectList(db.Ditte.ToList(), "Id", "Nome");
                        return View(m);
                    }
                    db.Entry(medicinale).State = EntityState.Modified;

          
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    
                }
            }
            ViewBag.DitteList = new SelectList(db.Ditte.ToList(), "Id", "Nome");
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


        //Lista delle ditte presenti
        public ActionResult ListaDitte()
        {
            return View(db.Ditte.ToList());
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



        //Modifica Ditta
        public ActionResult ModificaDitta(int id )
        {
            var d = db.Ditte.Find(id);
            return View(d);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ModificaDitta (Ditte d)
        {

            if (ModelState.IsValid)
            {

                var ditta = db.Ditte.Find(d.Id);
                if (ditta != null)
                {
                    ditta.Nome = d.Nome;
                    ditta.Recapito = d.Recapito;
                    ditta.Indirizzo = d.Indirizzo;
                    

                    db.Entry(ditta).State = EntityState.Modified;


                    db.SaveChanges();

                    return RedirectToAction("ListaDitte");
                }
               
            }
            return View();
        }



        //Eliminazione ditta
        public ActionResult EliminaDitta(int id)
        {
            Ditte d = db.Ditte.Find(id);

            if (d != null)
            {
               
                var medicinaliAssociati = db.Medicinali.Where(m => m.FkDitta == d.Id).ToList();

             
                foreach (var m in medicinaliAssociati)
                {
                    db.Medicinali.Remove(m);
                }

                db.Ditte.Remove(d);
                db.SaveChanges();
            }

            return RedirectToAction("ListaDitte");
        }

        public ActionResult Cerca()
        {
            return View();
        }


        public JsonResult Armadietto(string nome)
        {
            var localizzazione = db.Medicinali.Where(m => m.Nome == nome).FirstOrDefault();

            if (localizzazione != null)
            {
           
                var result = new
                {
                    Nome = localizzazione.Nome,
                    Posizione = localizzazione.Posizione
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
               
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Vendite() 
        {
            return View();
        }

        public JsonResult Ricerca(DateTime data)
        {
            var vendite = db.MedicinaliVendite.Where(m => DbFunctions.TruncateTime(m.Data) == data.Date).ToList();

            var risultati = new List<object>();

            foreach (var vendita in vendite)
            {
                var medicina = db.Medicinali.Find(vendita.FkProdotto);

                var result = new
                {
                    Data = vendita.Data.ToString("dd-MM-yyyy"),
                    Medicina =  medicina.Nome 
                };

                risultati.Add(result);
            }

            return Json(risultati, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VenditePerCliente()
        {
            return View();
        }

        public JsonResult RicercaCF(string Cf)
        {
            try
            {
                var user = db.Utenti.Where(u => u.CF == Cf).FirstOrDefault();

                if (user != null)
                {
                    var vendite = db.MedicinaliVendite.Where(m => m.FkUtente == user.Id).ToList();

                    var risultati = new List<object>();

                    foreach (var vendita in vendite)
                    {
                        var medicina = db.Medicinali.Find(vendita.FkProdotto);

                        if (medicina != null)
                        {
                            var result = new
                            {
                                Data = vendita.Data.ToString("dd-MM-yyyy"),
                                Medicina = medicina.Nome
                            };

                            risultati.Add(result);
                        }
                    }

                    return Json(risultati, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { error = "Utente non trovato" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = "Errore interno del server" }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}