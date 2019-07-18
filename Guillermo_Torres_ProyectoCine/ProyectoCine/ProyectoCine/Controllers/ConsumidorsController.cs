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
    public class ConsumidorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Consumidors
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

            var consumidor = from s in db.Consumidors
                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                consumidor = consumidor.Where(s => s.Nombre.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    consumidor = consumidor.OrderByDescending(s => s.Nombre);

                    break;
                default:
                    consumidor = consumidor.OrderBy(s => s.Nombre);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(consumidor.ToPagedList(pageNumber, pageSize));
        }

        // GET: Consumidors/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumidor consumidor = db.Consumidors.Find(id);
            if (consumidor == null)
            {
                return HttpNotFound();
            }
            return View(consumidor);
        }

        // GET: Consumidors/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Consumidors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsumidorID,Nombre,Telefono,DetallesTarjeta,DetallesConsumidor")] Consumidor consumidor)
        {
            if (ModelState.IsValid)
            {
                db.Consumidors.Add(consumidor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consumidor);
        }

        // GET: Consumidors/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumidor consumidor = db.Consumidors.Find(id);
            if (consumidor == null)
            {
                return HttpNotFound();
            }
            return View(consumidor);
        }

        // POST: Consumidors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConsumidorID,Nombre,Telefono,DetallesTarjeta,DetallesConsumidor")] Consumidor consumidor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consumidor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consumidor);
        }

        // GET: Consumidors/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumidor consumidor = db.Consumidors.Find(id);
            if (consumidor == null)
            {
                return HttpNotFound();
            }
            return View(consumidor);
        }

        // POST: Consumidors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consumidor consumidor = db.Consumidors.Find(id);
            db.Consumidors.Remove(consumidor);
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
