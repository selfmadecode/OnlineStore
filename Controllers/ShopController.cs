﻿using SmartStore.Models;
using SmartStore.ViewModel;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartStore.Services;

namespace SmartStore.Controllers
{
    public class ShopController : Controller
    {
        IShopService _shopService;
        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }
        // GET: Items in stock
        //[Authorize]
       // ShopService shopService = new ShopService();
        public ActionResult Products()
        {
            // var items = _dbContext.Items.Include(c => c.Category).Include(s => s.Supplier).ToList();
            var items = _shopService.GetAllProducts();
            
            //If the Admin is logged in, show the admin dashboard(stockAdmin)
            if (User.IsInRole(RoleName.Admin))
                return View("StockAdmin", items);
            else
            {
                // if the store manager is logged in, show the stocklist dashboard
                if (User.IsInRole(RoleName.StoreManager))
                    return View("StockList", items);
            }
            // no user is logged in
            return View("Products", items);
        }

        //Passes the membership type to the view
        [HttpGet]
        [Authorize(Roles = RoleName.Admin + "," + RoleName.StoreManager)] //test 4
        public ActionResult AddNewItem()
        {
            var itemViewModel = _shopService.ItemViewModel();

            return View("ItemForm", itemViewModel);
        }

        // saves the item into the DB
        [HttpPost]
        [Authorize(Roles = RoleName.Admin + "," + RoleName.StoreManager)] //test 3
        public ActionResult Save(Item item)
        {
            if(!ModelState.IsValid)
            {
                var itemViewModel = _shopService.ItemViewModel(item);
                
                return View("ItemForm", itemViewModel);
            }

            if (item.Id == 0) // new item will be added
                _shopService.AddItemToDb(item);
            // _dbContext.Items.Add(item);
            else
            {
                _shopService.UpdateItemInDb(item);
            }

            //AddThisFeature (Keep the form open incase you want to add more items) 
            return RedirectToAction("Products");
        }

        [Authorize(Roles = RoleName.Admin + "," + RoleName.StoreManager)]
        public ActionResult Edit(int id)
        {
            var itemInDb = _shopService.EditItemInDb(id);

            if (itemInDb == null)
                return HttpNotFound();

            return View("ItemForm", itemInDb);
        }

        //Delete an Item
        [Authorize(Roles = RoleName.StoreManager)] //test 2
        public ActionResult Delete(int id)
        {
            var item = _shopService.DeleteItem(id);

            if (item == null)
                return HttpNotFound();

            return RedirectToAction("Products");
        }
        //Get Item details
        public ActionResult Details (int Id)
        {
            var productDetails = _shopService.GetItemDetails(Id);
            if (productDetails == null)
                return HttpNotFound();

            return View(productDetails);
        }
        //Search DB
        public ActionResult Search(string search)
        {
            if (string.IsNullOrEmpty(search))
                return RedirectToAction("Products");

            var items = _shopService.SearchDb(search);
            return View(items);
        }
    }
}