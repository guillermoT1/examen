using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoCine.Models;

namespace ProyectoCine.Controllers
{
    public class FilaAsientosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FilaAsientos
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var filaAsientos = db.FilaAsientos.Include(f => f.Cines);
            return View(filaAsientos.ToList());
        }

        // GET: FilaAsientos/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilaAsientos filaAsientos = db.FilaAsientos.Find(id);
            if (filaAsientos == null)
            {
                return HttpNotFound();
            }
            return View(filaAsientos);
        }
        [Authorize(Roles = "Admin")]
        // GET: FilaAsientos/Create
        public ActionResult Create()
        {
            ViewBag.CinesID = new SelectList(db.Cines, "CinesID", "Nombre");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: FilaAsientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FilaAsientosID,CinesID,Contador_Asientos")] FilaAsientos filaAsientos)
        {
            if (ModelState.IsValid)
            {
                db.FilaAsientos.Add(filaAsientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CinesID = new SelectList(db.Cines, "CinesID", "Nombre", filaAsientos.CinesID);
            return View(filaAsientos);
        }

        // GET: FilaAsientos/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilaAsientos filaAsientos = db.FilaAsientos.Find(id);
            if (filaAsientos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CinesID = new SelectList(db.Cines, "CinesID", "Nombre", filaAsientos.CinesID);
            return View(filaAsientos);
        }
        [Authorize(Roles = "Admin")]
        // POST: FilaAsientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FilaAsientosID,CinesID,Contador_Asientos")] FilaAsientos filaAsientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filaAsientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CinesID = new SelectList(db.Cines, "CinesID", "Nombre", filaAsientos.CinesID);
            return View(filaAsientos);
        }
        [Authorize(Roles = "Admin")]
        // GET: FilaAsientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilaAsientos filaAsientos = db.FilaAsientos.Find(id);
            if (filaAsientos == null)
            {
                return HttpNotFound();
            }
            return View(filaAsientos);
        }
        [Authorize(Roles = "Admin")]
        // POST: FilaAsientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FilaAsientos filaAsientos = db.FilaAsientos.Find(id);
            db.FilaAsientos.Remove(filaAsientos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
