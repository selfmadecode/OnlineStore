using SmartStore.Models;
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
        public ActionResult Products()
        {
            //var searchDb = TempData["searchDb"] as List<Item>;

            List<Item> items;

            // if (searchDb == null)
            if (!(TempData["searchDb"] is List<Item> searchDb))
                items = _shopService.GetAllProducts();
            else
                items = searchDb;
            
            //If the Admin is logged in, show the admin dashboard(stockAdmin)
            if (User.IsInRole(RoleName.Admin))
                return View("StockAdmin", items);
            else
            {
                if (User.IsInRole(RoleName.StoreManager))
                    return View("StockList", items);
            }
            // no user is loged in
            return View("Products", items);
        }


        [HttpGet]
        [Authorize(Roles = RoleName.Admin + "," + RoleName.StoreManager)]
        public ActionResult AddNewItem()
        {
            var itemViewModel = _shopService.ItemViewModel();

            return View("ItemForm", itemViewModel);
        }

        // saves the item into the DB
        [HttpPost]
        [Authorize(Roles = RoleName.Admin + "," + RoleName.StoreManager)]
        public ActionResult Save(Item item)
        {
            if(!ModelState.IsValid)
            {
                var itemViewModel = _shopService.ItemViewModel(item);
                
                return View("ItemForm", itemViewModel);
            }

            if (item.Id == 0)
                _shopService.AddItemToDb(item);
            else
            {
                _shopService.UpdateItemInDb(item);
            }
 
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
        [Authorize(Roles = RoleName.StoreManager)]
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

            //return View(items);
            TempData["searchDb"] = items;

            return RedirectToAction("Products");
        }
        public ActionResult GetAllOrder()
        {
            var pendinOrders = _shopService.GetAllOrder();
            return View(pendinOrders);
        }
        public ActionResult GetOrder(string id)
        {
            var orders = _shopService.GetOrder(id);
            return View(orders);
        }
    }
}