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
    public class ShowPeliculasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShowPeliculas
        public ActionResult Index()
        {
            var showPeliculas = db.ShowPeliculas.Include(s => s.Cines).Include(s => s.Peliculas);
            return View(showPeliculas.ToList());
        }

        // GET: ShowPeliculas/Details/5
 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowPelicula showPelicula = db.ShowPeliculas.Find(id);
            if (showPelicula == null)
            {
                return HttpNotFound();
            }
            return View(showPelicula);
        }
        [Authorize(Roles = "Admin")]
        // GET: ShowPeliculas/Create
        public ActionResult Create()
        {
            ViewBag.CinesID = new SelectList(db.Cines, "CinesID", "Nombre");
            ViewBag.PeliculasID = new SelectList(db.Peliculas, "PeliculasID", "Nombre");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: ShowPeliculas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShowPeliculaID,CinesID,PeliculasID,ShowInicio,ShowFin")] ShowPelicula showPelicula)
        {
            if (ModelState.IsValid)
            {
                db.ShowPeliculas.Add(showPelicula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CinesID = new SelectList(db.Cines, "CinesID", "Nombre", showPelicula.CinesID);
            ViewBag.PeliculasID = new SelectList(db.Peliculas, "PeliculasID", "Nombre", showPelicula.PeliculasID);
            return View(showPelicula);
        }
        [Authorize(Roles = "Admin")]
        // GET: ShowPeliculas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowPelicula showPelicula = db.ShowPeliculas.Find(id);
            if (showPelicula == null)
            {
                return HttpNotFound();
            }
            ViewBag.CinesID = new SelectList(db.Cines, "CinesID", "Nombre", showPelicula.CinesID);
            ViewBag.PeliculasID = new SelectList(db.Peliculas, "PeliculasID", "Nombre", showPelicula.PeliculasID);
            return View(showPelicula);
        }
        [Authorize(Roles = "Admin")]
        // POST: ShowPeliculas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShowPeliculaID,CinesID,PeliculasID,ShowInicio,ShowFin")] ShowPelicula showPelicula)
        {
            if (ModelState.IsValid)
            {
                db.Entry(showPelicula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CinesID = new SelectList(db.Cines, "CinesID", "Nombre", showPelicula.CinesID);
            ViewBag.PeliculasID = new SelectList(db.Peliculas, "PeliculasID", "Nombre", showPelicula.PeliculasID);
            return View(showPelicula);
        }
        [Authorize(Roles = "Admin")]
        // GET: ShowPeliculas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowPelicula showPelicula = db.ShowPeliculas.Find(id);
            if (showPelicula == null)
            {
                return HttpNotFound();
            }
            return View(showPelicula);
        }
        [Authorize(Roles = "Admin")]
        // POST: ShowPeliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShowPelicula showPelicula = db.ShowPeliculas.Find(id);
            db.ShowPeliculas.Remove(showPelicula);
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
