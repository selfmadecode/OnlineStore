using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartStore.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Services
{
    public class AdminService: Controller
    {
        public static bool CreateStoreManager(ApplicationDbContext context, StoreManager storeManager)
        {
            // if the username does not exist
            if (!context.Users.Any(u => u.UserName == storeManager.EmailAddress))
            {
                var user = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(user);

                var passwordHasher = new PasswordHasher();
                var admin = new ApplicationUser
                {
                    UserName = storeManager.EmailAddress,
                    Email = storeManager.EmailAddress,
                    PasswordHash = passwordHasher.HashPassword(storeManager.Password)
                };
                userManager.Create(admin);
                userManager.AddToRole(admin.Id, RoleName.StoreManager);
                //ViewBag.NewManager = "Store Manager Added succesffully";
                return true;
            }
            else
            {// if the user is already in db
                // ViewBag.NewManager = "User already exist";
                return false;
            }
        }
    }
}