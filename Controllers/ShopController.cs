using SmartStore.Models;
using SmartStore.ViewModel;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStore.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext _dbContext;
        public ShopController()
        {
            _dbContext = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
        // GET: Items in stock
        //[Authorize]
        public ActionResult Products(string search = "")
        {
            var items = _dbContext.Items.Include(c => c.Category).Include(s => s.Supplier).ToList();
            
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
            var getCategoryFromDb = _dbContext.Categories.ToList();
            var getSupplierFromDb = _dbContext.Suppliers.ToList();

            var itemViewModel = new ItemViewModel
            {
                Categories = getCategoryFromDb,
                Supplier = getSupplierFromDb,
                Item = new Item() // to intialize default properties like ID for validification summary
            };
            return View("ItemForm", itemViewModel);
        }

        // saves the item into the DB
        [HttpPost]
        [Authorize(Roles = RoleName.Admin+ "," + RoleName.StoreManager)] //test 3
        public ActionResult Save(Item item)
        {
            if(!ModelState.IsValid)
            {
                var itemViewModel = new ItemViewModel
                {
                    Item = item,
                    Categories = _dbContext.Categories.ToList(),
                    Supplier = _dbContext.Suppliers.ToList()
                };
                return View("ItemForm", itemViewModel);
            }

            if(item.Id == 0) // new item will be added
                _dbContext.Items.Add(item);
            else
            {
                // the item that will be edited
                var UpdateItem = _dbContext.Items.Single(i => i.Id == item.Id);

                UpdateItem.Name = item.Name;
                UpdateItem.Amount = item.Amount;
                UpdateItem.ExpiringDate = item.ExpiringDate;
                UpdateItem.Quantity = item.Quantity;
                UpdateItem.SupplierId = item.SupplierId;
                UpdateItem.CategoryId = item.CategoryId;

            }
            _dbContext.SaveChanges();

            //AddThisFeature (Keep the form open incase you want to add more items) 
            return RedirectToAction("Products");
        }
        //[Authorize(Roles = RoleName.StoreManager)] //test 1
        [Authorize(Roles = RoleName.Admin + "," + RoleName.StoreManager)]
        public ActionResult Edit(int id)
        {
            //gets the id that matches else return notfound error
            var itemInDb = _dbContext.Items.SingleOrDefault(i => i.Id == id);
            
            if (itemInDb == null)
                return HttpNotFound();

            // holds the details of the item that matches the id
            var item = new ItemViewModel
            {
                Item = itemInDb,
                Categories = _dbContext.Categories.ToList(),
                Supplier = _dbContext.Suppliers.ToList()
            };

            return View("ItemForm", item);
        }
        //Delete an Item
        [Authorize(Roles = RoleName.Admin)] //test 2
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Items.Include(i => i.Supplier).Include(i => i.Category).Single(i => i.Id == id);
            
            if (item == null)
                return HttpNotFound();
            else
                _dbContext.Items.Remove(item);

            _dbContext.SaveChanges();
            return RedirectToAction("Products");
        }
        public ActionResult Details (int Id)
        {
            var productDetails = _dbContext.Items.Include(i => i.Category).Include(i => i.Supplier).SingleOrDefault(i => i.Id == Id);
            return View(productDetails);
        }
        public ActionResult Search(string search)
        {
            var items = _dbContext.Items.Include(c => c.Category).Include(s => s.Supplier).ToList();

            if (string.IsNullOrEmpty(search))
                return RedirectToAction("Products");

            switch (search)
            {
                case "Food":
                    return View(items.Where(c => c.Category.Name == search).ToList());
                case "House Hold":
                    return View(items.Where(c => c.Category.Name == search).ToList());
                case "Accessories":
                    return View(items.Where(c => c.Category.Name == search).ToList());
                case "Skin Care":
                    return View(items.Where(c => c.Category.Name == search).ToList());
                case "Others":
                    return View(items.Where(c => c.Category.Name == search || search == null).ToList());
                default:
                    return View(items);
            }
        }


    }
}