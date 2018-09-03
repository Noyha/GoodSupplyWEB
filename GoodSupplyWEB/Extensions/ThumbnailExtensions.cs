using GoodSupplyWEB.Models;
using GoodSupplyWEB.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.Extensions
{
    public static class ThumbnailExtensions
    {
        public static IEnumerable<ThumbnailModel> SetSupplierProductsThumbnail(this List<ThumbnailModel> thumbnails, GoodSupplyEntities db = null)
        {
            try
            {
                if (db == null)
                {
                    db = new GoodSupplyEntities();
                }

                var suppliers = db.Suppliers.ToList();
                var manufacturerProducts = db.ManufacturerProducts.ToList();

                thumbnails = (from p in db.ManufacturerProducts
                              select new ThumbnailModel
                              {
                                  ManufacturerProductId = p.Id,
                                  ImageUrl = p.ImageUrl,
                                  ProductName = p.Products.Name,
                                  ManufacturerName = p.Manufacturers.Name,
                                  CatalogNumber = p.CatalogNumber,
                                  PackageQuantity = p.PackageQuantity,
                                  //ProductPrice = p.ProductPrice,
                                  //Quantity = p.Quantity,
                                  //ManufacturerPrice = p.ManufacturerPrice
                                  Suppliers = suppliers,
                                  ManufacturerProducts = manufacturerProducts
                              }).ToList();

                //thumbnails = (from p in db.Prices
                //              select new ThumbnailModel
                //              {
                //                  PriceId = p.Id,
                //                  ImageUrl = p.ManufacturerProducts.ImageUrl,
                //                  ProductName = p.ManufacturerProducts.Products.Name,
                //                  ManufacturerName = p.ManufacturerProducts.Manufacturers.Name,
                //                  CatalogNumber = p.ManufacturerProducts.CatalogNumber,
                //                  PackageQuantity = p.ManufacturerProducts.PackageQuantity,
                //                  ProductPrice = p.ProductPrice,
                //                  Quantity = p.Quantity,
                //                  ManufacturerPrice = p.ManufacturerPrice
                //              }).ToList();
            }
            catch (Exception ex)
            {

            }
            return thumbnails.OrderBy(t => t.ProductName);
        }


        public static IEnumerable<ThumbnailProductsModel> GetProductsThumbnails(this List<ThumbnailProductsModel> productsthumbnails, GoodSupplyEntities db = null, string option = null, string search = null)
        {
            try
            {

                if (db == null)
                {
                    db = new GoodSupplyEntities();
                }

                productsthumbnails = (from m in db.ManufacturerProducts
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
                                      }).ToList(); // a list of thumbnails models

                //if (search != null)
                //    return thumbnails.Where(t => t.Name.ToLower().Contains(search.ToLower())).OrderBy(t => t.Name);

                if (search != null)
                {

                    if (option == "ProductName" && search.Length > 0)
                    {
                        return productsthumbnails.Where(t => t.ProductName.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName);
                    }
                    if (option == "CatalogNumber" && search.Length > 0)
                    {
                        return productsthumbnails.Where(t => t.CatalogNumber.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName);
                    }

                    return productsthumbnails.Where(t => t.ProductName.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName);

                }
            }
            catch (Exception ex)
            {
            }

            return productsthumbnails.OrderBy(p => p.ProductName);
        }


        public static IEnumerable<ThumbnailSuppliersModel> GetSuppliersThumbnails(this List<ThumbnailSuppliersModel> suppliersthumbnails, GoodSupplyEntities db = null, string option = null, string search = null)
        {
            try
            {

                if (db == null)
                {
                    db = new GoodSupplyEntities();
                }

                suppliersthumbnails = (from s in db.Suppliers
                                      select new ThumbnailSuppliersModel
                                      {
                                          SupplierId = s.Id,
                                          FirstName = s.FirstName,
                                          LastName = s.LastName,
                                          Email = s.Email,
                                          Phone = s.Phone,
                                          BusinessName = s.BusinessName,
                                          BusinessAddress = s.BusinessAddress,
                                          VATIdNumber = s.VATIdNumber,
                                          Link1 = "/OurSuppliers/SupplierDetails/" + s.Id,
                                          Link2 = "/OurSuppliers/GetSupplierProducts/" + s.Id
                                      }).ToList(); // a list of thumbnails models

                if (search != null)
                {

                    if (option == "SupplierBusinessName" && search.Length > 0)
                    {
                        return suppliersthumbnails.Where(s => s.BusinessName.ToLower().Contains(search.ToLower())).OrderBy(s => s.BusinessName);
                    }
                    if (option == "SupplierBusinessAddress" && search.Length > 0)
                    {
                        return suppliersthumbnails.Where(s => s.BusinessAddress.ToLower().Contains(search.ToLower())).OrderBy(s => s.BusinessAddress);
                    }

                    return suppliersthumbnails.Where(s => s.BusinessName.ToLower().Contains(search.ToLower())).OrderBy(s => s.BusinessName);

                }
            }
            catch (Exception ex)
            {
            }

            return suppliersthumbnails.OrderBy(s => s.BusinessName);
        }


        public static IEnumerable<ThumbnailProductsModel> GetSupplierProductsThumbnails(this List<ThumbnailProductsModel> productsthumbnails, GoodSupplyEntities db = null, int id = 0, string option = null, string search = null)
        {
            try
            {

                if (db == null)
                {
                    db = new GoodSupplyEntities();
                }

                int supplierId = 0;
                if (id > 0)
                {
                    supplierId = id;
                }

                productsthumbnails = (from m in db.ManufacturerProducts
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

                //if (search != null)
                //    return thumbnails.Where(t => t.Name.ToLower().Contains(search.ToLower())).OrderBy(t => t.Name);

                if (search != null)
                {

                    if (option == "ProductName" && search.Length > 0)
                    {
                        return productsthumbnails.Where(t => t.ProductName.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName);
                    }
                    if (option == "CatalogNumber" && search.Length > 0)
                    {
                        return productsthumbnails.Where(t => t.CatalogNumber.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName);
                    }

                    return productsthumbnails.Where(t => t.ProductName.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName);

                }
            }
            catch (Exception ex)
            {
            }

            return productsthumbnails.OrderBy(p => p.ProductName);
        }


        public static IEnumerable<ThumbnailSupplierProductsModel> SetSupplierProductsThumbnails(this List<ThumbnailSupplierProductsModel> supplierProductsthumbnails, GoodSupplyEntities db = null, string option = null, string search = null)
        {
            try
            {

                if (db == null)
                {
                    db = new GoodSupplyEntities();
                }

                supplierProductsthumbnails = (from m in db.ManufacturerProducts
                                              select new ThumbnailSupplierProductsModel
                                              {
                                                  ProductId = m.Id,
                                                  ProductName = m.Products.Name,
                                                  ManufacturerName = m.Manufacturers.Name,
                                                  CommerciallName = m.Name,
                                                  CatalogNumber = m.CatalogNumber,
                                                  ImageUrl = m.ImageUrl,
                                                  PackageQuantity = m.PackageQuantity
                                              }).ToList(); // a list of thumbnails models

                //if (search != null)
                //    return thumbnails.Where(t => t.Name.ToLower().Contains(search.ToLower())).OrderBy(t => t.Name);

                if (search != null)
                {

                    if (option == "ProductName" && search.Length > 0)
                    {
                        return supplierProductsthumbnails.Where(t => t.ProductName.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName);
                    }
                    if (option == "CatalogNumber" && search.Length > 0)
                    {
                        return supplierProductsthumbnails.Where(t => t.CatalogNumber.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName);
                    }

                    return supplierProductsthumbnails.Where(t => t.ProductName.ToLower().Contains(search.ToLower())).OrderBy(t => t.ProductName);

                }
            }
            catch (Exception ex)
            {
            }

            return supplierProductsthumbnails.OrderBy(p => p.ProductName);
        }
    }
}