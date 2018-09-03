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
    public class OrderDetailController : Controller
    {
        // GET: OrderDetail
        public ActionResult Index()
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var orderDetails = db.OrderDetails.Include(o => o.Orders).Include(o => o.ManufacturerProducts);
                return View(orderDetails.ToList());
            }

            //using (var db = new GoodSupplyEntities())
            //{
            //    var order1 = db.OrderDetails.Where(o => o.OrderId == 1).FirstOrDefault();
            //    var my_price = db.Prices.Where(p => p.SupplierId == order1.Orders.SupplierId && p.ManufacturerProductId == order1.ManufacturerProductId).FirstOrDefault();
            //    order1.QuantityPrice = my_price.Price * order1.Quantity;
            //}
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

                OrderDetails orderDetails = db.OrderDetails.Find(id);

                if (orderDetails == null)
                {
                    return HttpNotFound();
                }

                var model = new OrderDetailViewModel
                {
                    OrderDetails = orderDetails,
                    ManufacturerProducts = db.ManufacturerProducts.ToList(),
                    Orders = db.Orders.ToList()
                };
                return View(model);
            }
        }


        // GET: Create
        public ActionResult Create()
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var manufacturerProducts = db.ManufacturerProducts.ToList();
                var orders = db.Orders.ToList();
                var model = new OrderDetailViewModel
                {
                    ManufacturerProducts = manufacturerProducts,
                    Orders = orders
                };
                return View(model);
            }
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderDetailViewModel orderDetailVM)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var orderDetails = new OrderDetails
                {
                    Quantity = orderDetailVM.OrderDetails.Quantity,
                    QuantityPrice = orderDetailVM.OrderDetails.QuantityPrice,
                    ItemPrice = orderDetailVM.OrderDetails.ItemPrice,
                    TotalOrderPrice = orderDetailVM.OrderDetails.TotalOrderPrice,
                    OrderId = orderDetailVM.OrderDetails.OrderId,
                    Orders = orderDetailVM.OrderDetails.Orders,
                    ManufacturerProductId = orderDetailVM.OrderDetails.ManufacturerProductId,
                    ManufacturerProducts = orderDetailVM.OrderDetails.ManufacturerProducts
                };

                if (ModelState.IsValid)
                {
                    db.OrderDetails.Add(orderDetails);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                orderDetailVM.ManufacturerProducts = db.ManufacturerProducts.ToList();
                orderDetailVM.Orders = db.Orders.ToList();
                return View(orderDetailVM);
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

                OrderDetails orderDetails = db.OrderDetails.Find(id);

                if (orderDetails == null)
                {
                    return HttpNotFound();
                }

                var model = new OrderDetailViewModel
                {
                    OrderDetails = orderDetails,
                    ManufacturerProducts = db.ManufacturerProducts.ToList(),
                    Orders = db.Orders.ToList()
                };
                return View(model);
            }
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderDetailViewModel orderDetailVM)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var orderDetails = new OrderDetails
                {
                    Id = orderDetailVM.OrderDetails.Id,
                    Quantity = orderDetailVM.OrderDetails.Quantity,
                    QuantityPrice = orderDetailVM.OrderDetails.QuantityPrice,
                    ItemPrice = orderDetailVM.OrderDetails.ItemPrice,
                    TotalOrderPrice = orderDetailVM.OrderDetails.TotalOrderPrice,
                    OrderId = orderDetailVM.OrderDetails.OrderId,
                    Orders = orderDetailVM.OrderDetails.Orders,
                    ManufacturerProductId = orderDetailVM.OrderDetails.ManufacturerProductId,
                    ManufacturerProducts = orderDetailVM.OrderDetails.ManufacturerProducts
                };

                if (ModelState.IsValid)
                {
                    db.Entry(orderDetails).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                orderDetailVM.ManufacturerProducts = db.ManufacturerProducts.ToList();
                orderDetailVM.Orders = db.Orders.ToList();
                return View(orderDetailVM);
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

                OrderDetails orderDetails = db.OrderDetails.Find(id);

                if (orderDetails == null)
                {
                    return HttpNotFound();
                }

                var model = new OrderDetailViewModel
                {
                    OrderDetails = orderDetails,
                    ManufacturerProducts = db.ManufacturerProducts.ToList(),
                    Orders = db.Orders.ToList()
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
                OrderDetails orderDetails = db.OrderDetails.Find(id);
                db.OrderDetails.Remove(orderDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult OrderDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var order = db.Orders.Where(o => o.Id == id).FirstOrDefault();
                var myOrderDetails = db.OrderDetails.Where(o => o.OrderId == order.Id).Include(o => o.Orders).Include(o => o.ManufacturerProducts).Include(o => o.ManufacturerProducts.Products).Include(o => o.ManufacturerProducts.Manufacturers);

                return View(myOrderDetails.ToList());
            }
        }

        public ActionResult DeleteProduct(int Id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                OrderDetails orderDetails = db.OrderDetails.Find(Id);

                var order = db.Orders.Where(o => o.Id == orderDetails.OrderId).FirstOrDefault();
                var orderId = order.Id;
                if (order.ClientApproval == 0)
                {
                    db.OrderDetails.Remove(orderDetails);
                    db.SaveChanges();
                    return RedirectToAction("OrderDetails", new { id = orderId });
                }
                TempData["orderMessage"] = "לא ניתן למחוק מוצר זה, ההזמנה כבר אושרה על ידך";
                return RedirectToAction("OrderDetails", new { id = orderId });
            }
        }
    }
}
