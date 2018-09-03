using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GoodSupplyWEB.Models.DB;
using GoodSupplyWEB.ViewModels;

namespace GoodSupplyWEB.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: ProductDetail
        public ActionResult Index(int id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                if ((string)Session["UserType"] == "Client")
                {
                    int userId = Int32.Parse((string)Session["UserId"]);

                    var user = db.Clients.Where(u => u.Id == userId).FirstOrDefault();

                    var productModel = db.ManufacturerProducts.Include(m => m.Manufacturers).Include(m => m.Products).SingleOrDefault(m => m.Id == id);

                    ProductOrderViewModel model = new ProductOrderViewModel
                    {
                        ManufacturerProductId = productModel.Id,
                        ProductName = productModel.Products.Name,
                        ManufacturerName = productModel.Manufacturers.Name,
                        CommercialName = productModel.Name,
                        CatalogNumber = productModel.CatalogNumber,
                        ImageUrl = productModel.ImageUrl,
                        PackageQuantity = productModel.PackageQuantity,
                        Width = productModel.Width,
                        Length = productModel.Length,
                        Height = productModel.Height,
                        ManufacturerId = productModel.ManufacturerId,
                        Manufacturers = db.Manufacturers.FirstOrDefault(m => m.Id.Equals(productModel.ManufacturerId)),
                        ProductId = productModel.ProductId,
                        Products = db.Products.FirstOrDefault(p => p.Id.Equals(productModel.ProductId)),
                        ClientId = user.Id,
                        PricesList = db.Prices.Where(p => p.ManufacturerProductId.Equals(productModel.Id)).ToList(),
                        Suppliers = db.Suppliers.ToList()
                    };

                    return View(model);

                }
                else if ((string)Session["UserType"] == "Supplier")
                {
                    int userId = Int32.Parse((string)Session["UserId"]);

                    var user = db.Suppliers.Where(u => u.Id == userId).FirstOrDefault();

                    var productModel = db.ManufacturerProducts.Include(m => m.Manufacturers).Include(m => m.Products).SingleOrDefault(m => m.Id == id);

                    ProductOrderViewModel model = new ProductOrderViewModel
                    {
                        ManufacturerProductId = productModel.Id,
                        ProductName = productModel.Products.Name,
                        ManufacturerName = productModel.Manufacturers.Name,
                        CommercialName = productModel.Name,
                        CatalogNumber = productModel.CatalogNumber,
                        ImageUrl = productModel.ImageUrl,
                        PackageQuantity = productModel.PackageQuantity,
                        Width = productModel.Width,
                        Length = productModel.Length,
                        Height = productModel.Height,
                        ManufacturerId = productModel.ManufacturerId,
                        Manufacturers = db.Manufacturers.FirstOrDefault(m => m.Id.Equals(productModel.ManufacturerId)),
                        ProductId = productModel.ProductId,
                        Products = db.Products.FirstOrDefault(p => p.Id.Equals(productModel.ProductId)),
                        PricesList = db.Prices.Where(p => p.ManufacturerProductId.Equals(productModel.Id)).ToList(),
                        Suppliers = db.Suppliers.ToList()
                    };

                    return View(model);
                }
                else
                {
                    var productModel = db.ManufacturerProducts.Include(m => m.Manufacturers).Include(m => m.Products).SingleOrDefault(m => m.Id == id);

                    ProductOrderViewModel model = new ProductOrderViewModel
                    {
                        ManufacturerProductId = productModel.Id,
                        ProductName = productModel.Products.Name,
                        ManufacturerName = productModel.Manufacturers.Name,
                        CommercialName = productModel.Name,
                        CatalogNumber = productModel.CatalogNumber,
                        ImageUrl = productModel.ImageUrl,
                        PackageQuantity = productModel.PackageQuantity,
                        Width = productModel.Width,
                        Length = productModel.Length,
                        Height = productModel.Height,
                        ManufacturerId = productModel.ManufacturerId,
                        Manufacturers = db.Manufacturers.FirstOrDefault(m => m.Id.Equals(productModel.ManufacturerId)),
                        ProductId = productModel.ProductId,
                        Products = db.Products.FirstOrDefault(p => p.Id.Equals(productModel.ProductId)),
                        PricesList = db.Prices.Where(p => p.ManufacturerProductId.Equals(productModel.Id)).ToList(),
                        Suppliers = db.Suppliers.ToList()
                    };

                    return View(model);
                }
            }
        }
    }
}