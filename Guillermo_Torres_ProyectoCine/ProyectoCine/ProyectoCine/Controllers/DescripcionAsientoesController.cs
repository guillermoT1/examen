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
    public class DescripcionAsientoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DescripcionAsientoes
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var descripcionAsientoes = db.DescripcionAsientoes.Include(d => d.Estado).Include(d => d.FilaAsientos).Include(d => d.Reservas);
            return View(descripcionAsientoes.ToList());
        }

        // GET: DescripcionAsientoes/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescripcionAsiento descripcionAsiento = db.DescripcionAsientoes.Find(id);
            if (descripcionAsiento == null)
            {
                return HttpNotFound();
            }
            return View(descripcionAsiento);
        }

        // GET: DescripcionAsientoes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.EstadoAsientosID = new SelectList(db.EstadoAsientos, "EstadoAsientosID", "DestallesAsiento");
            ViewBag.FilaAsientosID = new SelectList(db.FilaAsientos, "FilaAsientosID", "FilaAsientosID");
            ViewBag.ReservasID = new SelectList(db.Reservas, "ReservasID", "ReservasID");
            return View();
        }

        // POST: DescripcionAsientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DescripcionAsientoID,FilaAsientosID,ReservasID,EstadoAsientosID")] DescripcionAsiento descripcionAsiento)
        {
            if (ModelState.IsValid)
            {
                db.DescripcionAsientoes.Add(descripcionAsiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstadoAsientosID = new SelectList(db.EstadoAsientos, "EstadoAsientosID", "DestallesAsiento", descripcionAsiento.EstadoAsientosID);
            ViewBag.FilaAsientosID = new SelectList(db.FilaAsientos, "FilaAsientosID", "FilaAsientosID", descripcionAsiento.FilaAsientosID);
            ViewBag.ReservasID = new SelectList(db.Reservas, "ReservasID", "ReservasID", descripcionAsiento.ReservasID);
            return View(descripcionAsiento);
        }

        // GET: DescripcionAsientoes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescripcionAsiento descripcionAsiento = db.DescripcionAsientoes.Find(id);
            if (descripcionAsiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoAsientosID = new SelectList(db.EstadoAsientos, "EstadoAsientosID", "DestallesAsiento", descripcionAsiento.EstadoAsientosID);
            ViewBag.FilaAsientosID = new SelectList(db.FilaAsientos, "FilaAsientosID", "FilaAsientosID", descripcionAsiento.FilaAsientosID);
            ViewBag.ReservasID = new SelectList(db.Reservas, "ReservasID", "ReservasID", descripcionAsiento.ReservasID);
            return View(descripcionAsiento);
        }

        [Authorize(Roles = "Admin")]
        // POST: DescripcionAsientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DescripcionAsientoID,FilaAsientosID,ReservasID,EstadoAsientosID")] DescripcionAsiento descripcionAsiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(descripcionAsiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstadoAsientosID = new SelectList(db.EstadoAsientos, "EstadoAsientosID", "DestallesAsiento", descripcionAsiento.EstadoAsientosID);
            ViewBag.FilaAsientosID = new SelectList(db.FilaAsientos, "FilaAsientosID", "FilaAsientosID", descripcionAsiento.FilaAsientosID);
            ViewBag.ReservasID = new SelectList(db.Reservas, "ReservasID", "ReservasID", descripcionAsiento.ReservasID);
            return View(descripcionAsiento);
        }

        // GET: DescripcionAsientoes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DescripcionAsiento descripcionAsiento = db.DescripcionAsientoes.Find(id);
            if (descripcionAsiento == null)
            {
                return HttpNotFound();
            }
            return View(descripcionAsiento);
        }
        [Authorize(Roles = "Admin")]
        // POST: DescripcionAsientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DescripcionAsiento descripcionAsiento = db.DescripcionAsientoes.Find(id);
            db.DescripcionAsientoes.Remove(descripcionAsiento);
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
