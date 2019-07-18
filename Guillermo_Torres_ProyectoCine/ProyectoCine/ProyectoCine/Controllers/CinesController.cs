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
    public class CinesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cines
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

            var cines = from s in db.Cines
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                cines = cines.Where(s => s.Nombre.Contains(searchString)
                                       || s.Administrador.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    cines = cines.OrderByDescending(s => s.Nombre);
                    cines = cines.OrderByDescending(s => s.Administrador);

                    break;
                default:
                    cines = cines.OrderBy(s => s.Nombre);
                    cines = cines.OrderBy(s => s.Administrador);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(cines.ToPagedList(pageNumber, pageSize));
        }
        // GET: Cines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cines cines = db.Cines.Find(id);
            if (cines == null)
            {
                return HttpNotFound();
            }
            return View(cines);
        }
        [Authorize(Roles = "Admin")]
        // GET: Cines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cines/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CinesID,Nombre,Administrador,Direccion,Telefono,Capcidad_Asientos,OtrosDetalles")] Cines cines)
        {
            if (ModelState.IsValid)
            {
                db.Cines.Add(cines);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cines);
        }
        [Authorize(Roles = "Admin")]
        // GET: Cines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cines cines = db.Cines.Find(id);
            if (cines == null)
            {
                return HttpNotFound();
            }
            return View(cines);
        }
        [Authorize(Roles = "Admin")]
        // POST: Cines/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CinesID,Nombre,Administrador,Direccion,Telefono,Capcidad_Asientos,OtrosDetalles")] Cines cines)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cines).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cines);
        }
        [Authorize(Roles = "Admin")]
        // GET: Cines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cines cines = db.Cines.Find(id);
            if (cines == null)
            {
                return HttpNotFound();
            }
            return View(cines);
        }
        [Authorize(Roles = "Admin")]
        // POST: Cines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cines cines = db.Cines.Find(id);
            db.Cines.Remove(cines);
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
