using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartStore.Models;
using SmartStore.Services;
using SmartStore.Services.Interfaces;
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
        IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
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
                var result = _adminService.CreateStoreManager(context, storeManager);

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
                return View("AddStoreManager", storeManager);
            }
        }
    }
}