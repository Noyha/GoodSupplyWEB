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
    public class ManufacturerProductController : Controller
    {
        // GET: ManufacturerProduct
        public ActionResult Index()
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var manufacturerProducts = db.ManufacturerProducts.Include(m => m.Manufacturers).Include(m => m.Products);
                return View(manufacturerProducts.ToList());
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

                ManufacturerProducts manufacturerProducts = db.ManufacturerProducts.Find(id);

                if (manufacturerProducts == null)
                {
                    return HttpNotFound();
                }

                var model = new ManufacturerProductViewModel
                {
                    ManufacturerProducts = manufacturerProducts,
                    Manufacturers = db.Manufacturers.ToList(),
                    Products = db.Products.ToList()
                };
                return View(model);
            }
        }


        // GET: Create
        public ActionResult Create()
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var manufacturers = db.Manufacturers.ToList();
                var products = db.Products.ToList();
                var model = new ManufacturerProductViewModel
                {
                    Manufacturers = manufacturers,
                    Products = products
                };
                return View(model);
            }
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ManufacturerProductViewModel manufacturerProductVM)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var manufacturerProducts = new ManufacturerProducts
                {
                    Name = manufacturerProductVM.ManufacturerProducts.Name,
                    CatalogNumber = manufacturerProductVM.ManufacturerProducts.CatalogNumber,
                    ImageUrl = manufacturerProductVM.ManufacturerProducts.ImageUrl,
                    PackageQuantity = manufacturerProductVM.ManufacturerProducts.PackageQuantity,
                    Width = manufacturerProductVM.ManufacturerProducts.Width,
                    Length = manufacturerProductVM.ManufacturerProducts.Length,
                    Height = manufacturerProductVM.ManufacturerProducts.Height,
                    Manufacturers = manufacturerProductVM.ManufacturerProducts.Manufacturers,
                    ManufacturerId = manufacturerProductVM.ManufacturerProducts.ManufacturerId,
                    Products = manufacturerProductVM.ManufacturerProducts.Products,
                    ProductId = manufacturerProductVM.ManufacturerProducts.ProductId
                };

                if (ModelState.IsValid)
                {
                    db.ManufacturerProducts.Add(manufacturerProducts);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                manufacturerProductVM.Manufacturers = db.Manufacturers.ToList();
                manufacturerProductVM.Products = db.Products.ToList();
                return View(manufacturerProductVM);
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

                ManufacturerProducts manufacturerProducts = db.ManufacturerProducts.Find(id);

                if (manufacturerProducts == null)
                {
                    return HttpNotFound();
                }

                var model = new ManufacturerProductViewModel
                {
                    ManufacturerProducts = manufacturerProducts,
                    Manufacturers = db.Manufacturers.ToList(),
                    Products = db.Products.ToList()
                };
                return View(model);
            }
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ManufacturerProductViewModel manufacturerProductVM)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var manufacturerProducts = new ManufacturerProducts
                {
                    Id = manufacturerProductVM.ManufacturerProducts.Id,
                    Name = manufacturerProductVM.ManufacturerProducts.Name,
                    CatalogNumber = manufacturerProductVM.ManufacturerProducts.CatalogNumber,
                    ImageUrl = manufacturerProductVM.ManufacturerProducts.ImageUrl,
                    PackageQuantity = manufacturerProductVM.ManufacturerProducts.PackageQuantity,
                    Width = manufacturerProductVM.ManufacturerProducts.Width,
                    Length = manufacturerProductVM.ManufacturerProducts.Length,
                    Height = manufacturerProductVM.ManufacturerProducts.Height,
                    Manufacturers = manufacturerProductVM.ManufacturerProducts.Manufacturers,
                    ManufacturerId = manufacturerProductVM.ManufacturerProducts.ManufacturerId,
                    Products = manufacturerProductVM.ManufacturerProducts.Products,
                    ProductId = manufacturerProductVM.ManufacturerProducts.ProductId
                };

                if (ModelState.IsValid)
                {
                    db.Entry(manufacturerProducts).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                manufacturerProductVM.Manufacturers = db.Manufacturers.ToList();
                manufacturerProductVM.Products = db.Products.ToList();
                return View(manufacturerProductVM);
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

                ManufacturerProducts manufacturerProducts = db.ManufacturerProducts.Find(id);

                if (manufacturerProducts == null)
                {
                    return HttpNotFound();
                }

                var model = new ManufacturerProductViewModel
                {
                    ManufacturerProducts = manufacturerProducts,
                    Manufacturers = db.Manufacturers.ToList(),
                    Products = db.Products.ToList()
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
                ManufacturerProducts manufacturerProducts = db.ManufacturerProducts.Find(id);
                db.ManufacturerProducts.Remove(manufacturerProducts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
