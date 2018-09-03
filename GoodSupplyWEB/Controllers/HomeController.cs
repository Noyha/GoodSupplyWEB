using GoodSupplyWEB.Models;
using GoodSupplyWEB.Models.DB;
using GoodSupplyWEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;
using GoodSupplyWEB.Extensions;

namespace GoodSupplyWEB.Controllers
{
    public class HomeController : Controller
    {

        private GoodSupplyEntities db = new GoodSupplyEntities();

        public ActionResult Index(string option = null, string search = null)
        {
            var thumbnails = new List<ThumbnailProductsModel>().GetProductsThumbnails(new GoodSupplyEntities(), option, search);
            var count = thumbnails.Count() / 5;

            var model = new List<ThumbnailProductsBoxViewModel>();

            for (int i = 0; i <= count; i++)
            {
                model.Add(new ThumbnailProductsBoxViewModel
                {
                    ProductsThumbnails = thumbnails.Skip(i * 5).Take(5)
                });
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ShowProducts(int? id, string option = null, string search = null)
        {
            List<ThumbnailProductsModel> Thumbnail = new List<ThumbnailProductsModel>();

            ViewBag.categories = db.Categories.Include(c => c.Childrens).Include(c => c.Parents).Where(c => !c.ParentId.HasValue).ToList();

            if (!id.HasValue)
            {
                //ViewBag.categories = db.Categories.Include(c => c.Childrens).Include(c => c.Parents).Where(c => !c.ParentId.HasValue).ToList();

                Thumbnail = (from m in db.ManufacturerProducts
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
                    if (option == "ManufacturerName" && search.Length > 0)
                    {
                        return View(Thumbnail.Where(t => t.ManufacturerName.ToLower().Contains(search.ToLower())).OrderBy(t => t.ManufacturerName));
                    }

                    return View(Thumbnail.Where(t => t.ProductName.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName));

                }

                return View(Thumbnail.OrderBy(p => p.ProductName));
            }

            Thumbnail = (from m in db.ManufacturerProducts
                        join p in db.Products on m.ProductId equals p.Id
                        where p.CategoryId == id
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
                if (option == "ManufacturerName" && search.Length > 0)
                {
                    return View(Thumbnail.Where(t => t.ManufacturerName.ToLower().Contains(search.ToLower())).OrderBy(t => t.ManufacturerName));
                }

                return View(Thumbnail.Where(t => t.ProductName.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName));

            }

            return View(Thumbnail.OrderBy(p => p.ProductName));

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}