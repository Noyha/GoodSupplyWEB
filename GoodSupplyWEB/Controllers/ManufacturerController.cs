using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoodSupplyWEB.Models.DB;

namespace GoodSupplyWEB.Controllers
{
    public class ManufacturerController : Controller
    {
        // GET: Manufacturer
        public ActionResult Index()
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                return View(db.Manufacturers.ToList());
            }
        }


        // GET: Details
        public ActionResult Details(int? id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Manufacturers manufacturers = db.Manufacturers.Find(id);

                if (manufacturers == null)
                {
                    return HttpNotFound();
                }

                return View(manufacturers);
            }
        }


        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Manufacturers manufacturers)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Manufacturers.Add(manufacturers);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(manufacturers);
            }
        }


        // GET: Edit
        public ActionResult Edit(int? id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Manufacturers manufacturers = db.Manufacturers.Find(id);

                if (manufacturers == null)
                {
                    return HttpNotFound();
                }
                return View(manufacturers);
            }
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Manufacturers manufacturers)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(manufacturers).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(manufacturers);
            }
        }


        // GET: Delete
        public ActionResult Delete(int? id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Manufacturers manufacturers = db.Manufacturers.Find(id);

                if (manufacturers == null)
                {
                    return HttpNotFound();
                }
                return View(manufacturers);
            }
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                Manufacturers manufacturers = db.Manufacturers.Find(id);
                db.Manufacturers.Remove(manufacturers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
