using GoodSupplyWEB.Models;
using GoodSupplyWEB.Models.DB;
using GoodSupplyWEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GoodSupplyWEB.Extensions;

namespace GoodSupplyWEB.Controllers
{
    public class OurSuppliersController : Controller
    {
        // GET: OurSuppliers
        public ActionResult Index(string option = null, string search = null)
        {
            var thumbnails = new List<ThumbnailSuppliersModel>().GetSuppliersThumbnails(new GoodSupplyEntities(), option, search);
            var count = thumbnails.Count() / 5;

            var model = new List<ThumbnailSuppliersBoxViewModel>();

            for (int i = 0; i <= count; i++)
            {
                model.Add(new ThumbnailSuppliersBoxViewModel
                {
                    SuppliersThumbnails = thumbnails.Skip(i * 5).Take(5)
                });
            }
            return View(model);
        }


        //public ActionResult SupplierDetails(int? id)
        //{
        //    return View();
        //}


        public ActionResult GetSupplierProducts(int? Id, string option = null, string search = null)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                int supplierId = 0;
                if (Id != null)
                {
                    supplierId = (int)Id;
                    ViewBag.SupplierIdForOrder = supplierId;
                }

                List<ThumbnailProductsModel> Thumbnail = (from m in db.ManufacturerProducts
                                                                   join p in db.Prices on m.Id equals p.ManufacturerProductId
                                                                   where p.SupplierId.Equals(supplierId)
                                                                   select new ThumbnailProductsModel
                                                                   {
                                                                       ProductId = m.Id,
                                                                       ProductName = m.Products.Name,
                                                                       ManufacturerName = m.Manufacturers.Name,
                                                                       CommerciallName = m.Name,
                                                                       CatalogNumber = m.CatalogNumber,
                                                                       ImageUrl = m.ImageUrl,
                                                                       PackageQuantity = m.PackageQuantity,
                                                                       //Link = "/ProductDetail/Index/" + m.Id,
                                                                       ProductPrice = p.ProductPrice
                                                                   }).ToList(); // a list of thumbnails models
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
    }
}