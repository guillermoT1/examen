using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoCine.Models;
using PagedList;
using PagedList.Mvc;

namespace ProyectoCine.Controllers
{
    public class EstadoAsientosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EstadoAsientos
        [Authorize]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var estadoAsientos = from s in db.EstadoAsientos
                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                estadoAsientos = estadoAsientos.Where(s => s.Disponible.Contains(searchString));
                estadoAsientos = estadoAsientos.Where(s => s.NoDisponible.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    estadoAsientos = estadoAsientos.OrderByDescending(s => s.Disponible);
                    estadoAsientos = estadoAsientos.Where(s => s.NoDisponible.Contains(searchString));

                    break;
                default:
                    estadoAsientos = estadoAsientos.OrderBy(s => s.NoDisponible);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(estadoAsientos.ToPagedList(pageNumber, pageSize));
        }

        // GET: EstadoAsientos/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoAsientos estadoAsientos = db.EstadoAsientos.Find(id);
            if (estadoAsientos == null)
            {
                return HttpNotFound();
            }
            return View(estadoAsientos);
        }
        [Authorize(Roles = "Admin")]
        // GET: EstadoAsientos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoAsientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstadoAsientosID,DestallesAsiento,Disponible,NoDisponible")] EstadoAsientos estadoAsientos)
        {
            if (ModelState.IsValid)
            {
                db.EstadoAsientos.Add(estadoAsientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoAsientos);
        }
        [Authorize(Roles = "Admin")]
        // GET: EstadoAsientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoAsientos estadoAsientos = db.EstadoAsientos.Find(id);
            if (estadoAsientos == null)
            {
                return HttpNotFound();
            }
            return View(estadoAsientos);
        }
        [Authorize(Roles = "Admin")]
        // POST: EstadoAsientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstadoAsientosID,DestallesAsiento,Disponible,NoDisponible")] EstadoAsientos estadoAsientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoAsientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoAsientos);
        }
        [Authorize(Roles = "Admin")]
        // GET: EstadoAsientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoAsientos estadoAsientos = db.EstadoAsientos.Find(id);
            if (estadoAsientos == null)
            {
                return HttpNotFound();
            }
            return View(estadoAsientos);
        }
        [Authorize(Roles = "Admin")]
        // POST: EstadoAsientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoAsientos estadoAsientos = db.EstadoAsientos.Find(id);
            db.EstadoAsientos.Remove(estadoAsientos);
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
