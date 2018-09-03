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
using GoodSupplyWEB.Models;
using GoodSupplyWEB.Extensions;

namespace GoodSupplyWEB.Controllers
{
    public class SupplierProductsController : Controller
    {

        public ActionResult ShowItems(int? Id, string option = null, string search = null)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                List<ThumbnailProductsModel> Thumbnail = (from m in db.ManufacturerProducts
                                                          select new ThumbnailProductsModel
                                                          {
                                                              ProductId = m.Id,
                                                              ProductName = m.Products.Name,
                                                              ManufacturerName = m.Manufacturers.Name,
                                                              CommerciallName = m.Name,
                                                              CatalogNumber = m.CatalogNumber,
                                                              ImageUrl = m.ImageUrl,
                                                              PackageQuantity = m.PackageQuantity,
                                                              Link = "/ProductDetail/Index/" + m.Id
                                                          }).ToList();
                if (search != null)
                {

                    if (option == "ProductName" && search.Length > 0)
                    {
                        return View(Thumbnail.Where(t => t.ProductName.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName));
                    }
                    if (option == "CatalogNumber" && search.Length > 0)
                    {
                        return View(Thumbnail.Where(t => t.CatalogNumber.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName));
                    }

                    return View(Thumbnail.Where(t => t.ProductName.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName));

                }

                return View(Thumbnail.OrderBy(p => p.ProductName));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetSupplierProducts(SupplierSetProductsViewModel supplierSetProductsVM)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var prices = new Prices
                {
                    ProductPrice = supplierSetProductsVM.ProductPrice,
                    Quantity = supplierSetProductsVM.Quantity,
                    ManufacturerPrice = supplierSetProductsVM.ManufacturerPrice,
                    Suppliers = db.Suppliers.FirstOrDefault(s => s.Id.Equals(supplierSetProductsVM.SupplierId)),
                    SupplierId = supplierSetProductsVM.SupplierId,
                    ManufacturerProducts = db.ManufacturerProducts.FirstOrDefault(m => m.Id.Equals(supplierSetProductsVM.ManufacturerProductId)),
                    ManufacturerProductId = supplierSetProductsVM.ManufacturerProductId
                };

                if (ModelState.IsValid)
                {
                    db.Prices.Add(prices);
                    db.SaveChanges();
                    return RedirectToAction("ShowItems");
                }
                return View();
            }
        }

        public ActionResult MyProducts(int? Id, string option = null, string search = null)
        {
            //Add a view for supplier to see all his products in a list, with search button.
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var myProducts = db.Prices.Include(p => p.ManufacturerProducts.Products).Include(p => p.ManufacturerProducts.Manufacturers).Include(p => p.Suppliers).Where(p => p.SupplierId == Id).ToList();

                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    myProducts = myProducts.Where(s => s.ManufacturerProducts.Products.Name.Contains(searchString)
                //                           || s.FirstMidName.Contains(searchString));
                //}

                if (search != null)
                {

                    if (option == "ProductName" && search.Length > 0)
                    {
                        return View(myProducts.Where(t => t.ManufacturerProducts.Products.Name.ToLower().Contains(search.ToLower())).OrderBy(t => t.ManufacturerProducts.Products.Name));
                    }
                    if (option == "ManufacturerName" && search.Length > 0)
                    {
                        return View(myProducts.Where(t => t.ManufacturerProducts.Manufacturers.Name.ToLower().Contains(search.ToLower())).OrderBy(t => t.ManufacturerProducts.Manufacturers.Name));
                    }

                    return View(myProducts.Where(t => t.ManufacturerProducts.Products.Name.ToLower().Contains(search.ToLower())).OrderBy(t => t.ManufacturerProducts.Products.Name));

                }

                return View(myProducts.OrderBy(p => p.ManufacturerProducts.Products.Name));

                //return View(myProducts.ToList());
            }
        }


        // GET: EditProduct
        public ActionResult EditProduct(int? id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Prices prices = db.Prices.Find(id);

                if (prices == null)
                {
                    return HttpNotFound();
                }

                var model = new PriceViewModel
                {
                    Prices = prices,
                    ManufacturerProducts = db.ManufacturerProducts.ToList(),
                    Suppliers = db.Suppliers.ToList()
                };
                return View(model);
            }
        }

        // POST: EditProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(PriceViewModel priceVM)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var prices = new Prices
                {
                    Id = priceVM.Prices.Id,
                    ManufacturerPrice = priceVM.Prices.ManufacturerPrice,
                    ProductPrice = priceVM.Prices.ProductPrice,
                    Quantity = priceVM.Prices.Quantity,
                    ManufacturerProductId = priceVM.Prices.ManufacturerProductId,
                    ManufacturerProducts = priceVM.Prices.ManufacturerProducts,
                    SupplierId = priceVM.Prices.SupplierId,
                    Suppliers = priceVM.Prices.Suppliers
                };

                if (ModelState.IsValid)
                {
                    db.Entry(prices).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("MyProducts", new { id = prices.SupplierId});
                }
                priceVM.ManufacturerProducts = db.ManufacturerProducts.ToList();
                priceVM.Suppliers = db.Suppliers.ToList();
                return View(priceVM);
            }
        }


        // GRT: DeleteProduct
        public ActionResult DeleteProduct(int? id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Prices prices = db.Prices.Find(id);

                if (prices == null)
                {
                    return HttpNotFound();
                }

                var model = new PriceViewModel
                {
                    Prices = prices,
                    ManufacturerProducts = db.ManufacturerProducts.ToList(),
                    Suppliers = db.Suppliers.ToList()
                };
                return View(model);
            }
        }

        // POST: DeleteProduct
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProductConfirmed(int id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                Prices prices = db.Prices.Find(id);
                db.Prices.Remove(prices);
                db.SaveChanges();
                return RedirectToAction("MyProducts", new { id = prices.SupplierId});
            }
        }


        // LOGIN PROPERTIES

        public ActionResult Index()
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var prices = db.Prices.Include(p => p.ManufacturerProducts).Include(p => p.Suppliers);
                return View(prices.ToList());
            }
        }

        // GET: SupplierProducts/Details/5
        public ActionResult Details(int? id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Prices prices = db.Prices.Find(id);

                if (prices == null)
                {
                    return HttpNotFound();
                }

                var model = new PriceViewModel
                {
                    Prices = prices,
                    ManufacturerProducts = db.ManufacturerProducts.ToList(),
                    Suppliers = db.Suppliers.ToList()
                };
                return View(model);
            }
        }

        //GET: Create
        public ActionResult Create()
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var suppliers = db.Suppliers.ToList();
                var manufacturerProducts = db.ManufacturerProducts.ToList();
                var model = new PriceViewModel
                {
                    Suppliers = suppliers,
                    ManufacturerProducts = manufacturerProducts                    
                };
                return View(model);
            }
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PriceViewModel priceVM)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var prices = new Prices
                {
                    ProductPrice = priceVM.Prices.ProductPrice,
                    Quantity = priceVM.Prices.Quantity,
                    ManufacturerPrice = priceVM.Prices.ManufacturerPrice,
                    Suppliers = priceVM.Prices.Suppliers,
                    SupplierId = priceVM.Prices.SupplierId,
                    ManufacturerProducts = priceVM.Prices.ManufacturerProducts,
                    ManufacturerProductId = priceVM.Prices.ManufacturerProductId
                };

                if (ModelState.IsValid)
                {
                    db.Prices.Add(prices);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                priceVM.Suppliers = db.Suppliers.ToList();
                priceVM.ManufacturerProducts = db.ManufacturerProducts.ToList();
                return View(priceVM);
            }
        }

        // GET: SupplierProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Prices prices = db.Prices.Find(id);

                if (prices == null)
                {
                    return HttpNotFound();
                }

                var model = new PriceViewModel
                {
                    Prices = prices,
                    ManufacturerProducts = db.ManufacturerProducts.ToList(),
                    Suppliers = db.Suppliers.ToList()
                };
                return View(model);
            }
        }

        // POST: SupplierProducts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PriceViewModel priceVM)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var prices = new Prices
                {
                    Id = priceVM.Prices.Id,
                    ManufacturerPrice = priceVM.Prices.ManufacturerPrice,
                    ProductPrice = priceVM.Prices.ProductPrice,
                    Quantity = priceVM.Prices.Quantity,
                    ManufacturerProductId = priceVM.Prices.ManufacturerProductId,
                    ManufacturerProducts = priceVM.Prices.ManufacturerProducts,
                    SupplierId = priceVM.Prices.SupplierId,
                    Suppliers = priceVM.Prices.Suppliers
                };

                if (ModelState.IsValid)
                {
                    db.Entry(prices).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                priceVM.ManufacturerProducts = db.ManufacturerProducts.ToList();
                priceVM.Suppliers = db.Suppliers.ToList();
                return View(priceVM);
            }
        }


        // GET: SupplierProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Prices prices = db.Prices.Find(id);

                if (prices == null)
                {
                    return HttpNotFound();
                }

                var model = new PriceViewModel
                {
                    Prices = prices,
                    ManufacturerProducts = db.ManufacturerProducts.ToList(),
                    Suppliers = db.Suppliers.ToList()
                };
                return View(model);
            }
        }

        // POST: SupplierProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                Prices prices = db.Prices.Find(id);
                db.Prices.Remove(prices);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
