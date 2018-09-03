using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoodSupplyWEB.Models;
using GoodSupplyWEB.Models.DB;
using GoodSupplyWEB.ViewModels;

namespace GoodSupplyWEB.Controllers
{    
    public class OrderController : Controller
    {

        private GoodSupplyEntities db = new GoodSupplyEntities();

        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Clients).Include(o => o.Suppliers);
            return View(orders.ToList());            
        }


        // GET: Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Orders orders = db.Orders.Find(id);

            if (orders == null)
            {
                return HttpNotFound();
            }

            var model = new OrderViewModel
            {
                Orders = orders,
                Clients = db.Clients.ToList(),
                Suppliers = db.Suppliers.ToList()
            };
            return View(model);     
        }


        // GET: Create
        public ActionResult Create()
        {
            var suppliers = db.Suppliers.ToList();
            var clients = db.Clients.ToList();
            var model = new OrderViewModel
            {
                Clients = clients,
                Suppliers = suppliers
            };
            return View(model);            
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel orderVM)
        {
            var orders = new Orders
            {
                CreateDate = orderVM.Orders.CreateDate,
                PayDate = orderVM.Orders.PayDate,
                Discount = orderVM.Orders.Discount,
                TotalPrice = orderVM.Orders.TotalPrice,
                ClientApproval = orderVM.Orders.ClientApproval,
                SupplierApproval = orderVM.Orders.SupplierApproval,
                ClientId = orderVM.Orders.ClientId,
                Clients = orderVM.Orders.Clients,
                SupplierId = orderVM.Orders.SupplierId,
                Suppliers = orderVM.Orders.Suppliers
            };

            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            orderVM.Suppliers = db.Suppliers.ToList();
            orderVM.Clients = db.Clients.ToList();
            return View(orderVM);        
        }


        // GET: Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Orders orders = db.Orders.Find(id);

            if (orders == null)
            {
                return HttpNotFound();
            }

            var model = new OrderViewModel
            {
                Orders = orders,
                Clients = db.Clients.ToList(),
                Suppliers = db.Suppliers.ToList()
            };
            return View(model);            
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel orderVM)
        {
            var orders = new Orders
            {
                Id = orderVM.Orders.Id,
                CreateDate = orderVM.Orders.CreateDate,
                PayDate = orderVM.Orders.PayDate,
                Discount = orderVM.Orders.Discount,
                TotalPrice = orderVM.Orders.TotalPrice,
                ClientApproval = orderVM.Orders.ClientApproval,
                SupplierApproval = orderVM.Orders.SupplierApproval,
                ClientId = orderVM.Orders.ClientId,
                Clients = orderVM.Orders.Clients,
                SupplierId = orderVM.Orders.SupplierId,
                Suppliers = orderVM.Orders.Suppliers
            };

            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            orderVM.Suppliers = db.Suppliers.ToList();
            orderVM.Clients = db.Clients.ToList();
            return View(orderVM);            
        }


        // GET: Delete
        public ActionResult Delete(int? id)
        {         
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Orders orders = db.Orders.Find(id);

            if (orders == null)
            {
                return HttpNotFound();
            }

            var model = new OrderViewModel
            {
                Orders = orders,
                Clients = db.Clients.ToList(),
                Suppliers = db.Suppliers.ToList()
            };
            return View(model);           
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            var orderDetail = db.OrderDetails.Where(o => o.OrderId == orders.Id).ToList();
            foreach(var item in orderDetail)
            {
                db.OrderDetails.Remove(item);
                db.SaveChanges();
            }
            db.Orders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("Index");            
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToOrder(ProductOrderViewModel productOrderVM)
        {
            var countOrders = db.Orders.Where(o => o.ClientId == productOrderVM.ClientId && o.SupplierId == productOrderVM.SupplierId).Count();

            if (countOrders > 1)
            {
                var openOrder = db.Orders.Where(o => o.ClientId == productOrderVM.ClientId && o.SupplierId == productOrderVM.SupplierId && o.ClientApproval == 0).FirstOrDefault();

                if (openOrder != null)
                {
                    ProductOrderViewModel model = new ProductOrderViewModel
                    {
                        OrderId = openOrder.Id,
                        ManufacturerProductId = productOrderVM.ManufacturerProductId,
                        Quantity = productOrderVM.Quantity,
                        ProductPrice = productOrderVM.ProductPrice,
                        PackageQuantity = productOrderVM.PackageQuantity,
                        ClientId = productOrderVM.ClientId
                    };
                    return RedirectToAction("ProcessOrderDetails", model);
                }

                var order = new Orders
                {
                    ClientApproval = 0,
                    SupplierApproval = 0,
                    CreateDate = DateTime.Now,
                    //TotalPrice = productOrderVM.ProductPrice * productOrderVM.Quantity,
                    ClientId = productOrderVM.ClientId,
                    Clients = db.Clients.FirstOrDefault(c => c.Id.Equals(productOrderVM.ClientId)),
                    SupplierId = productOrderVM.SupplierId,
                    Suppliers = db.Suppliers.FirstOrDefault(c => c.Id.Equals(productOrderVM.SupplierId))
                };

                if (ModelState.IsValid)
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return RedirectToAction("ProcessOrderDetails", productOrderVM);
                }
            }
            else if (countOrders == 1)
            {
                var openOrder = db.Orders.Where(o => o.ClientId == productOrderVM.ClientId && o.SupplierId == productOrderVM.SupplierId && o.ClientApproval == 0).FirstOrDefault();

                if (openOrder != null)
                {
                    ProductOrderViewModel model = new ProductOrderViewModel
                    {
                        OrderId = openOrder.Id,
                        ManufacturerProductId = productOrderVM.ManufacturerProductId,
                        Quantity = productOrderVM.Quantity,
                        ProductPrice = productOrderVM.ProductPrice,
                        PackageQuantity = productOrderVM.PackageQuantity,
                        ClientId = productOrderVM.ClientId
                    };
                    return RedirectToAction("ProcessOrderDetails", model);
                }

                var order = new Orders
                {
                    ClientApproval = 0,
                    SupplierApproval = 0,
                    CreateDate = DateTime.Now,
                    ClientId = productOrderVM.ClientId,
                    Clients = db.Clients.FirstOrDefault(c => c.Id.Equals(productOrderVM.ClientId)),
                    SupplierId = productOrderVM.SupplierId,
                    Suppliers = db.Suppliers.FirstOrDefault(c => c.Id.Equals(productOrderVM.SupplierId))
                };

                if (ModelState.IsValid)
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return RedirectToAction("ProcessOrderDetails", productOrderVM);
                }
            }

            var newOrder = new Orders
            {
                ClientApproval = 0,
                SupplierApproval = 0,
                CreateDate = DateTime.Now,
                ClientId = productOrderVM.ClientId,
                Clients = db.Clients.FirstOrDefault(c => c.Id.Equals(productOrderVM.ClientId)),
                SupplierId = productOrderVM.SupplierId,
                Suppliers = db.Suppliers.FirstOrDefault(c => c.Id.Equals(productOrderVM.SupplierId))
            };

            if (ModelState.IsValid)
            {
                db.Orders.Add(newOrder);
                db.SaveChanges();
                return RedirectToAction("ProcessOrderDetails", productOrderVM);
            }
            productOrderVM.Suppliers = db.Suppliers.ToList();
            productOrderVM.Clients = db.Clients.ToList();
            //return View("MyOrders", productOrderVM);
            return View("ShowProducts", "Home");            
        }


        public ActionResult ProcessOrderDetails(ProductOrderViewModel processProductOrderVM)
        {
            var orderNumber = db.Orders.Where(o => o.ClientId == processProductOrderVM.ClientId && o.SupplierId == processProductOrderVM.SupplierId && o.ClientApproval == 0).FirstOrDefault();

            int myOrderId = 0;

            if (processProductOrderVM.OrderId > 0)
            {
                myOrderId = processProductOrderVM.OrderId;
            }
            else
            {                    
                myOrderId = orderNumber.Id;
            }

            var itemExistsInOrder = db.OrderDetails.Where(o => o.OrderId == myOrderId && o.ManufacturerProductId == processProductOrderVM.ManufacturerProductId).FirstOrDefault();

            if (itemExistsInOrder != null) //Checks if the item already exists in the order
            { //if so, change the Quantity and the QuantityPrice, then change the TotalPrice of the order
                itemExistsInOrder.Quantity = itemExistsInOrder.Quantity + processProductOrderVM.Quantity;
                itemExistsInOrder.QuantityPrice = itemExistsInOrder.QuantityPrice + (processProductOrderVM.Quantity * processProductOrderVM.ProductPrice);

                db.SaveChanges();

                var order = db.Orders.Find(myOrderId);
                decimal count = 0;
                foreach (var item in order.OrderDetails) //count the total price of the order
                {
                    count += (decimal)item.QuantityPrice;
                }

                order.TotalPrice = count;
                db.SaveChanges();

                return Redirect(Request.UrlReferrer.ToString()); //returns the previous view
            }
            else
            { //if the item does not exists, add a new record in the OrderDetails table.
                var orderDetails = new OrderDetails
                {
                    OrderId = myOrderId,
                    ManufacturerProductId = processProductOrderVM.ManufacturerProductId,
                    Quantity = processProductOrderVM.Quantity,
                    QuantityPrice = processProductOrderVM.Quantity * processProductOrderVM.ProductPrice,
                    ItemPrice = (double)processProductOrderVM.ProductPrice / processProductOrderVM.PackageQuantity,
                    TotalOrderPrice = null,
                    ManufacturerProducts = db.ManufacturerProducts.FirstOrDefault(m => m.Id.Equals(processProductOrderVM.ManufacturerProductId)),
                    Orders = db.Orders.FirstOrDefault(o => o.Id.Equals(myOrderId))
                };

                if (ModelState.IsValid)
                {
                    db.OrderDetails.Add(orderDetails);
                    db.SaveChanges();

                    var order = db.Orders.Find(myOrderId);
                    decimal count = 0;
                    foreach (var item in order.OrderDetails) //count the total price of the order
                    {
                        count += (decimal)item.QuantityPrice;
                    }

                    order.TotalPrice = count;
                    db.SaveChanges();

                    //return RedirectToAction("MyOrders", new { id = processProductOrderVM.ClientId });
                    return Redirect(Request.UrlReferrer.ToString()); //returns the previous view
                }
            }
           
            return View("ShowProducts", "Home");            
        }

        public ActionResult MyOrders(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var supplier = db.Suppliers.Where(s => s.Id == id).FirstOrDefault();
            var client = db.Clients.Where(c => c.Id == id).FirstOrDefault();
                
            if ((string)Session["UserType"] == "Supplier")
            {
                var mySupplierOrders = db.Orders.Where(o => o.SupplierId == supplier.Id).Include(o => o.Clients).Include(o => o.Suppliers);
                return View(mySupplierOrders.ToList());
            }
            else if ((string)Session["UserType"] == "Client")
            {
                var myClientOrders = db.Orders.Where(o => o.ClientId == client.Id).Include(o => o.Clients).Include(o => o.Suppliers);
                return View(myClientOrders.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }                
        }

        [HttpPost]
        public ActionResult ApproveOrderClient(int Id)
        {
            var order = db.Orders.Find(Id);

            if (order == null)
            {
                return HttpNotFound();
            }

            order.ClientApproval = 1;
            order.PayDate = DateTime.Now;
            db.SaveChanges();

            return RedirectToAction("MyOrders", new { id = order.ClientId });
        }

        [HttpPost]
        public ActionResult ApproveOrderSupplier(int Id, decimal Discount = 0)
        {
            var order = db.Orders.Find(Id);

            if (order == null)
            {
                return HttpNotFound();
            }

            order.SupplierApproval = 1;
            db.SaveChanges();

            if(Discount > 0)
            {
                order.Discount = Discount;
                db.SaveChanges();

                //Session["orderDiscount"] = "(לאחר הנחה)";

                ViewBag.discount = "(לאחר הנחה)";

                order.TotalPrice = order.TotalPrice - ((order.TotalPrice / 100) * order.Discount);
                db.SaveChanges();
                return RedirectToAction("MyOrders", new { id = order.SupplierId });
            }

            return RedirectToAction("MyOrders", new { id = order.SupplierId });
        }


        public ActionResult DeleteOrder(int id)
        {
            Orders orders = db.Orders.Find(id);
            var orderDetail = db.OrderDetails.Where(o => o.OrderId == orders.Id).ToList();
            foreach (var item in orderDetail)
            {
                db.OrderDetails.Remove(item);
                db.SaveChanges();
            }
            db.Orders.Remove(orders);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
