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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var products = db.Products.Include(p => p.Categories);
                return View(products.ToList());
            }
        }


        // GET: Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                Products products = db.Products.Find(id);

                if (products == null)
                {
                    return HttpNotFound();
                }

                var model = new ProductViewModel
                {
                    Products = products,
                    Categories = db.Categories.ToList()
                };
                return View(model);
            }
        }

        // GET: Create
        public ActionResult Create()
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var categories = db.Categories.ToList();
                var model = new ProductViewModel
                {
                    Categories = categories
                };
                return View(model);
            }
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel productVM)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var products = new Products
                {
                    Name = productVM.Products.Name,
                    Categories = productVM.Products.Categories,
                    CategoryId = productVM.Products.CategoryId
                };

                if (ModelState.IsValid)
                {
                    db.Products.Add(products);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                productVM.Categories = db.Categories.ToList();
                return View(productVM);
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

                Products products = db.Products.Find(id);

                if (products == null)
                {
                    return HttpNotFound();
                }

                var model = new ProductViewModel
                {
                    Products = products,
                    Categories = db.Categories.ToList()
                };
                return View(model);
            }
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel productVM)
        {
            var products = new Products
            {
                Id = productVM.Products.Id,
                Name = productVM.Products.Name,
                Categories = productVM.Products.Categories,
                CategoryId = productVM.Products.CategoryId
            };

            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(products).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                productVM.Categories = db.Categories.ToList();
                return View(productVM);
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

                Products products = db.Products.Find(id);

                if (products == null)
                {
                    return HttpNotFound();
                }

                var model = new ProductViewModel
                {
                    Products = products,
                    Categories = db.Categories.ToList()
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
                Products products = db.Products.Find(id);
                db.Products.Remove(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
