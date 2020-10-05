using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartStore.Models;
using SmartStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStore.Controllers
{
    [Authorize(Roles = RoleName.Admin)]
    public class AdminController : Controller
    {
        // admin/addstoremanager
        public ActionResult AddStoreManager()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddManager(ApplicationDbContext context, StoreManager storeManager)
        {
            if (ModelState.IsValid)
            {

                var result = AdminService.CreateStoreManager(context, storeManager);

                if (result)
                {
                    ViewBag.NewManager = "Store Manager Added succesffully";
                    return View("~/Views/Shop/StockAdmin.cshtml");
                }
                else
                {
                    ViewBag.NewManager = "User already exist";
                    return View("~/Views/Shop/StockAdmin.cshtml");
                }
            }
            else
            {
                // model state is not valid
                return View("AddStoreManager", storeManager);
            }
        }
    }
}