﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartStore.Models;
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
            // fix the error here
            
                if (!context.Users.Any(u => u.UserName == storeManager.EmailAddress))
                {
                    var user = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(user);

                    var passwordHasher = new PasswordHasher();
                    var admin = new ApplicationUser
                    {
                        UserName = storeManager.EmailAddress,
                        PasswordHash = passwordHasher.HashPassword(storeManager.Password)
                    };
                    userManager.Create(admin);
                    userManager.AddToRole(admin.Id, RoleName.StoreManager);
                    ViewBag.NewManager = "Store Manager Added succesffully";
                }
            
            return RedirectToAction("Products", "Shop");
            
        }
    }
}