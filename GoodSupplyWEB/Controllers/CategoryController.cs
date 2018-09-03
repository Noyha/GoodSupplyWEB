using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoodSupplyWEB.Models.DB;
using GoodSupplyWEB.ViewModels;

namespace GoodSupplyWEB.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var categories = db.Categories.Include(c => c.Parents);
                return View(categories.ToList());
            }
        }

        // GET: Details
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Categories categories = db.Categories.Find(id);
        //    if (categories == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(categories);
        //}


        // GET: Create
        public ActionResult Create()
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var categoriesList = db.Categories.ToList();
                var model = new CategoryViewModel
                {
                    CategoriesList = categoriesList
                };
                return View(model);
            }
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel categoryVM)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var categories = new Categories
                {
                    Name = categoryVM.Categories.Name,
                    Parents = categoryVM.Categories.Parents,
                    ParentId = categoryVM.Categories.ParentId
                };

                if (ModelState.IsValid)
                {
                    db.Categories.Add(categories);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                categoryVM.CategoriesList = db.Categories.ToList();
                return View(categoryVM);
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

                Categories categories = db.Categories.Find(id);

                if (categories == null)
                {
                    return HttpNotFound();
                }

                var model = new CategoryViewModel
                {
                    Categories = categories,
                    CategoriesList = db.Categories.ToList()
                };
                return View(model);
            }
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel categoryVM)
        {
            var categories = new Categories
            {
                Id = categoryVM.Categories.Id,
                Name = categoryVM.Categories.Name,
                Parents = categoryVM.Categories.Parents,
                ParentId = categoryVM.Categories.ParentId
            };

            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                //var categoryInDb = db.Categories.Single(c => c.Id == categoryVM.Categories.Id);

                if (ModelState.IsValid)
                {
                    db.Entry(categories).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                categoryVM.CategoriesList = db.Categories.ToList();
                return View(categoryVM);
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

                Categories categories = db.Categories.Find(id);

                if (categories == null)
                {
                    return HttpNotFound();
                }

                var model = new CategoryViewModel
                {
                    Categories = categories,
                    CategoriesList = db.Categories.ToList()
                };
                return View(model);
            }
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                Categories categories = db.Categories.Find(id);
                db.Categories.Remove(categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
