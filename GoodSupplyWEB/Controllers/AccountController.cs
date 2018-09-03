using GoodSupplyWEB.Models.DB;
using GoodSupplyWEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace GoodSupplyWEB.Controllers
{
    public class AccountController : Controller
    {
        //// GET: Client Account
        public ActionResult IndexSuppliers()
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                return View(db.Suppliers.ToList());
            }
        }

        public ActionResult IndexClients()
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                return View(db.Clients.ToList());
            }
        }

        //// GET: Client Account
        //public ActionResult ClientIndex()
        //{
        //    using (var db = new GoodSupplyEntities())
        //    {
        //        return View();
        //    }
        //}

        //// GET: Supplier Account
        //public ActionResult SupplierIndex(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    using (GoodSupplyEntities db = new GoodSupplyEntities())
        //    {
        //        Suppliers supplier = db.Suppliers.Find(id);

        //        if (supplier == null)
        //        {
        //            return HttpNotFound();
        //        }

        //        SupplierViewModel model = new SupplierViewModel
        //        {
        //            Id = supplier.Id,
        //            FirstName = supplier.FirstName,
        //            LastName = supplier.LastName,
        //            Email = supplier.Email,
        //            Phone = supplier.Phone,
        //            BusinessName = supplier.BusinessName,
        //            BusinessAddress = supplier.BusinessAddress,
        //            VATIdNumber = supplier.VATIdNumber
        //        };

        //        return View(model);
        //    }
        //}

        //GET: Select Register
        public ActionResult SelectRegister()
        {
            return View();
        }

        //POST: Select Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectRegister(String userType)
        {
            if (userType == "Client")
            {
                return RedirectToAction("ClientRegister");
            }
            else
            {
                return RedirectToAction("SupplierRegister");
            }
        }

        //GET: Client Register
        public ActionResult ClientRegister()
        {
            return View();
        }

        //POST: Client Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientRegister(ClientViewModel clientVM)
        {
            ClientViewModel model = new ClientViewModel
            {
                //Id = clientVM.Id,
                FirstName = clientVM.FirstName,
                LastName = clientVM.LastName,
                Email = clientVM.Email,
                Password = clientVM.Password,
                ConfirmPassword = clientVM.ConfirmPassword,
                Phone = clientVM.Phone,
                BusinessName = clientVM.BusinessName,
                BusinessAddress = clientVM.BusinessAddress,
                VATIdNumber = clientVM.VATIdNumber
            };

            if (!ModelState.IsValid)
            {
                return View("ClientRegister", model);
            }
            else
            {
                using (GoodSupplyEntities db = new GoodSupplyEntities())
                {
                    var user1 = db.Suppliers.Where(s => s.Email == model.Email).FirstOrDefault();
                    var user2 = db.Clients.Where(c => c.Email == model.Email).FirstOrDefault();

                    if (user1 != null || user2 != null)
                    {
                        ModelState.AddModelError("", "אימייל קיים כבר במערכת, נסה להרשם עם אימייל שונה.");
                        return View("ClientRegister", model);
                    }

                    var client = new Clients
                    {
                        //Id = clientVM.Id,
                        FirstName = clientVM.FirstName,
                        LastName = clientVM.LastName,
                        Email = clientVM.Email,
                        Password = clientVM.Password,
                        Phone = clientVM.Phone,
                        BusinessName = clientVM.BusinessName,
                        BusinessAddress = clientVM.BusinessAddress,
                        VATIdNumber = clientVM.VATIdNumber
                    };

                    db.Clients.Add(client);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = clientVM.FirstName + " " + clientVM.LastName + " הצלחת להרשם בהצלחה.";     

                return View();
            }
        }


        //GET: Supplier Register
        public ActionResult SupplierRegister()
        {
            return View();
        }

        //POST: Supplier Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SupplierRegister(SupplierViewModel supplierVM)
        {
            SupplierViewModel model = new SupplierViewModel
            {
                //Id = supplierVM.Id,
                FirstName = supplierVM.FirstName,
                LastName = supplierVM.LastName,
                Email = supplierVM.Email,
                Password = supplierVM.Password,
                ConfirmPassword = supplierVM.ConfirmPassword,
                Phone = supplierVM.Phone,
                BusinessName = supplierVM.BusinessName,
                BusinessAddress = supplierVM.BusinessAddress,
                VATIdNumber = supplierVM.VATIdNumber
            };

            if (!ModelState.IsValid)
            {
                return View("SupplierRegister", model);
            }
            else
            {
                using (GoodSupplyEntities db = new GoodSupplyEntities())
                {
                    var user1 = db.Suppliers.Where(s => s.Email == model.Email).FirstOrDefault();
                    var user2 = db.Clients.Where(c => c.Email == model.Email).FirstOrDefault();

                    if (user1 != null || user2 != null)
                    {
                        ModelState.AddModelError("", "אימייל קיים כבר במערכת, נסה להרשם עם אימייל שונה.");
                        return View("SupplierRegister", model);
                    }

                    var supplier = new Suppliers
                    {
                        //Id = supplierVM.Id,
                        FirstName = supplierVM.FirstName,
                        LastName = supplierVM.LastName,
                        Email = supplierVM.Email,
                        Password = supplierVM.Password,
                        Phone = supplierVM.Phone,
                        BusinessName = supplierVM.BusinessName,
                        BusinessAddress = supplierVM.BusinessAddress,
                        VATIdNumber = supplierVM.VATIdNumber
                    };

                    db.Suppliers.Add(supplier);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = supplierVM.FirstName + " " + supplierVM.LastName + " הצלחת להרשם בהצלחה.";

                return View();
            }
        }


        //GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                using (GoodSupplyEntities db = new GoodSupplyEntities())
                {
                    var supplier = db.Suppliers.Where(s => s.Email == loginVM.Email && s.Password == loginVM.Password).FirstOrDefault();
                    var client = db.Clients.Where(c => c.Email == loginVM.Email && c.Password == loginVM.Password).FirstOrDefault();

                    if (supplier != null)
                    {
                        Session["UserId"] = supplier.Id.ToString();
                        Session["FirstName"] = supplier.FirstName.ToString();
                        Session["LastName"] = supplier.LastName.ToString();
                        Session["loginSuccess"] = supplier;
                        Session["UserType"] = "Supplier";
                        return RedirectToAction("ShowProducts", "Home");
                    }
                    else if (supplier == null && client != null)
                    {
                        Session["UserId"] = client.Id.ToString();
                        Session["FirstName"] = client.FirstName.ToString();
                        Session["LastName"] = client.LastName.ToString();
                        Session["loginSuccess"] = client;
                        Session["UserType"] = "Client";
                        return RedirectToAction("ShowProducts", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "אימייל או סיסמא לא נכונים.");
                    }
                }
                return View();
            }

        }

        //LogOut
        public ActionResult Logout()
        {
            Session["loginSuccess"] = null;
            Session["UserId"] = null;
            Session["FirstName"] = null;
            Session["LastName"] = null;
            Session["UserType"] = null;
            Session.Clear();
            return RedirectToAction("ShowProducts", "Home");
        }

        //SupplierLoggedIn
        public ActionResult SupplierLoggedIn(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                Suppliers supplier = db.Suppliers.Find(id);

                if (supplier == null)
                {
                    return HttpNotFound();
                }

                SupplierViewModel model = new SupplierViewModel
                {
                    Id = supplier.Id,
                    FirstName = supplier.FirstName,
                    LastName = supplier.LastName,
                    Email = supplier.Email,
                    Phone = supplier.Phone,
                    BusinessName = supplier.BusinessName,
                    BusinessAddress = supplier.BusinessAddress,
                    VATIdNumber = supplier.VATIdNumber
                };

                return View(model);
            }
        }

        //ClientLoggedIn
        public ActionResult ClientLoggedIn(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                Clients client = db.Clients.Find(id);

                if (client == null)
                {
                    return HttpNotFound();
                }

                ClientViewModel model = new ClientViewModel
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Email = client.Email,
                    Phone = client.Phone,
                    BusinessName = client.BusinessName,
                    BusinessAddress = client.BusinessAddress,
                    VATIdNumber = client.VATIdNumber
                };

                return View(model);
            }
        }

        //GET: Client Change Password
        public ActionResult ClientChangePassword(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new ChangePasswordViewModel
            {
                Id = (int)id
            };

            return View(model);
        }


        //POST: Client Change Password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                using (GoodSupplyEntities db = new GoodSupplyEntities())
                {
                    var client = db.Clients.Where(c => c.Id == model.Id).FirstOrDefault();

                    if (client != null && client.Password == model.OldPassword)
                    {
                        client.Password = model.NewPassword;
                        db.SaveChanges();

                        Session["ChangePasswordSuccess"] = "הסיסמא שונתה בהצלחה.";
                        return RedirectToAction("ClientLoggedIn", new { id = client.Id});
                    }
                    else
                    {
                        ModelState.AddModelError("", "סיסמא אינה נכונה.");
                    }
                }
                return View();
            }   
        }


        //GET: Supplier Change Password
        public ActionResult SupplierChangePassword(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new ChangePasswordViewModel
            {
                Id = (int)id
            };

            return View(model);
        }

        //POST: Client Change Password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SupplierChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                using (GoodSupplyEntities db = new GoodSupplyEntities())
                {
                    var supplier = db.Suppliers.Where(s => s.Id == model.Id).FirstOrDefault();

                    if (supplier != null && supplier.Password == model.OldPassword)
                    {
                        supplier.Password = model.NewPassword;
                        db.SaveChanges();

                        Session["ChangePasswordSuccess"] = "הסיסמא שונתה בהצלחה.";
                        return RedirectToAction("SupplierLoggedIn", new { id = supplier.Id });
                    }
                    else
                    {
                        ModelState.AddModelError("", "סיסמא אינה נכונה.");
                    }
                }
                return View();
            }
        }


        //GET: Client Edit
        public ActionResult ClientEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var client = db.Clients.Find(id);

                if (client == null)
                {
                    return HttpNotFound();
                }

                var model = new ClientViewModel
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Email = client.Email,
                    Password = client.Password,
                    ConfirmPassword = client.Password,
                    Phone = client.Phone,
                    BusinessName = client.BusinessName,
                    BusinessAddress = client.BusinessAddress,
                    VATIdNumber = client.VATIdNumber
                };

                return View(model);
            }
        }

        //POST: Client Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientEdit(ClientViewModel clientVM)
        {
            var model = new ClientViewModel
            {
                Id = clientVM.Id,
                FirstName = clientVM.FirstName,
                LastName = clientVM.LastName,
                Email = clientVM.Email,
                Phone = clientVM.Phone,
                BusinessName = clientVM.BusinessName,
                BusinessAddress = clientVM.BusinessAddress,
                VATIdNumber = clientVM.VATIdNumber
            };

            if (!ModelState.IsValid)
            {
                return View("ClientEdit", model);
            }
            else
            {

                using (GoodSupplyEntities db = new GoodSupplyEntities())
                {
                    var clientInDb = db.Clients.Single(c => c.Id == clientVM.Id);

                    if (clientInDb.Email != clientVM.Email)
                    {
                        var user1 = db.Suppliers.Where(s => s.Email == model.Email).FirstOrDefault();
                        var user2 = db.Clients.Where(c => c.Email == model.Email).FirstOrDefault();

                        if (user1 != null || user2 != null)
                        {
                            ModelState.AddModelError("", "אימייל קיים כבר במערכת, נסה להרשם עם אימייל שונה.");
                            return View("ClientEdit", model);
                        }
                    }

                    clientInDb.FirstName = clientVM.FirstName;
                    clientInDb.LastName = clientVM.LastName;
                    clientInDb.Email = clientVM.Email;
                    clientInDb.Phone = clientVM.Phone;
                    clientInDb.BusinessName = clientVM.BusinessName;
                    clientInDb.BusinessAddress = clientVM.BusinessAddress;
                    clientInDb.VATIdNumber = clientVM.VATIdNumber;

                    db.SaveChanges();
                    return RedirectToAction("ClientLoggedIn", new { id = clientVM.Id });

                }
                //return RedirectToAction("Index", "Home");
            }
        }

        //GET: Client Edit
        public ActionResult SupplierEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                var supplier = db.Suppliers.Find(id);

                if (supplier == null)
                {
                    return HttpNotFound();
                }

                var model = new SupplierViewModel
                {
                    Id = supplier.Id,
                    FirstName = supplier.FirstName,
                    LastName = supplier.LastName,
                    Email = supplier.Email,
                    Password = supplier.Password,
                    ConfirmPassword = supplier.Password,
                    Phone = supplier.Phone,
                    BusinessName = supplier.BusinessName,
                    BusinessAddress = supplier.BusinessAddress,
                    VATIdNumber = supplier.VATIdNumber
                };

                return View(model);
            }
        }

        //POST: Client Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SupplierEdit(SupplierViewModel supplierVM)
        {
            var model = new SupplierViewModel
            {
                Id = supplierVM.Id,
                FirstName = supplierVM.FirstName,
                LastName = supplierVM.LastName,
                Email = supplierVM.Email,
                Phone = supplierVM.Phone,
                BusinessName = supplierVM.BusinessName,
                BusinessAddress = supplierVM.BusinessAddress,
                VATIdNumber = supplierVM.VATIdNumber
            };

            if (!ModelState.IsValid)
            {
                return View("SupplierEdit", model);
            }
            else
            {

                using (GoodSupplyEntities db = new GoodSupplyEntities())
                {
                    var supplierInDb = db.Suppliers.Single(s => s.Id == supplierVM.Id);

                    if (supplierInDb.Email != supplierVM.Email)
                    {
                        var user1 = db.Suppliers.Where(s => s.Email == model.Email).FirstOrDefault();
                        var user2 = db.Clients.Where(c => c.Email == model.Email).FirstOrDefault();

                        if (user1 != null || user2 != null)
                        {
                            ModelState.AddModelError("", "אימייל קיים כבר במערכת, נסה להרשם עם אימייל שונה.");
                            return View("SupplierEdit", model);
                        }
                    }

                    supplierInDb.FirstName = supplierVM.FirstName;
                    supplierInDb.LastName = supplierVM.LastName;
                    supplierInDb.Email = supplierVM.Email;
                    supplierInDb.Phone = supplierVM.Phone;
                    supplierInDb.BusinessName = supplierVM.BusinessName;
                    supplierInDb.BusinessAddress = supplierVM.BusinessAddress;
                    supplierInDb.VATIdNumber = supplierVM.VATIdNumber;

                    db.SaveChanges();
                    return RedirectToAction("SupplierLoggedIn", new { id = supplierVM.Id });

                }
                //return RedirectToAction("Index", "Home");
            }
        }

        // GET: Client Delete
        public ActionResult ClientDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {

                Clients client = db.Clients.Find(id);

                if (client == null)
                {
                    return HttpNotFound();
                }

                ClientViewModel model = new ClientViewModel
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Email = client.Email,
                    Phone = client.Phone,
                    BusinessName = client.BusinessName,
                    BusinessAddress = client.BusinessAddress,
                    VATIdNumber = client.VATIdNumber
                };

                return View(model);
            }
        }

        // POST: Client Delete
        [HttpPost, ActionName("ClientDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ClientDeleteConfirmed(int id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                Clients client = db.Clients.Find(id);
                db.Clients.Remove(client);
                db.SaveChanges();
            }

            return RedirectToAction("Logout");
        }

        // GET: Supplier Delete
        public ActionResult SupplierDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {

                Suppliers supplier = db.Suppliers.Find(id);

                if (supplier == null)
                {
                    return HttpNotFound();
                }

                SupplierViewModel model = new SupplierViewModel
                {
                    Id = supplier.Id,
                    FirstName = supplier.FirstName,
                    LastName = supplier.LastName,
                    Email = supplier.Email,
                    Phone = supplier.Phone,
                    BusinessName = supplier.BusinessName,
                    BusinessAddress = supplier.BusinessAddress,
                    VATIdNumber = supplier.VATIdNumber
                };

                return View(model);
            }
        }

        // POST: Supplier Delete
        [HttpPost, ActionName("SupplierDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult SupplierDeleteConfirmed(int id)
        {
            using (GoodSupplyEntities db = new GoodSupplyEntities())
            {
                Suppliers supplier = db.Suppliers.Find(id);
                db.Suppliers.Remove(supplier);
                db.SaveChanges();
            }

            return RedirectToAction("Logout");
        }
    }
}