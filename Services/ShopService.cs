using SmartStore.Models;
using SmartStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace SmartStore.Services
{
    public class ShopService : IShopService
    {
        readonly Context Dbcontext;
        public ShopService(Context context)
        {
            Dbcontext = context;
        }

        //List of all products
        public List<Item> GetAllProducts()
        {
            // context._dbContext.Database.CommandTimeout = 300;
            var allItems = Dbcontext._dbContext.Items.Include(c => c.Category).Include(s => s.Supplier).ToList();
            return allItems;
        }

        //Get Items by id
        public Item GetItemById(int id)
        {
            var item = Dbcontext._dbContext.Items.Include(i => i.Supplier).Include(i => i.Category).Single(i => i.Id == id);
            return item;
        }

        //List of Category
        public List<Category> GetAllCategories()
        {
            var getCategoryFromDb = Dbcontext._dbContext.Categories.ToList();
            return getCategoryFromDb;
        }

        //List of Suppliers
        public List<Supplier> GetAllSuppliers()
        {
            var getSupplierFromDb = Dbcontext._dbContext.Suppliers.ToList();
            return getSupplierFromDb;
        }

        //Create the view Model
        public ItemViewModel ItemViewModel(Item item = null)
        {
            //The item parameter is null so that when the model state is not valid
            // the details in the param will be sent back to the user

            if (item == null)
            {
                var itemViewModel = new ItemViewModel
                {
                    Categories = GetAllCategories(),
                    Supplier = GetAllSuppliers(),
                    Item = new Item() // to intialize default properties like ID for validification summary
                };
                return itemViewModel;
            }
            else
            {
                var itemViewModel = new ItemViewModel
                {
                    Item = item,
                    Categories = GetAllCategories(),
                    Supplier = GetAllSuppliers()
                };
                return itemViewModel;
            }
        }

        public void SaveImage(Item item)
        {
            string fileName = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);
            string extension = Path.GetExtension(item.ImageFile.FileName);

            fileName += DateTime.Now.ToString("yymmssfff") + extension;
            item.ImagePath = "~/Content/Images/" + fileName;
            fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images/"), fileName);

            //fileName = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);

            item.ImageFile.SaveAs(fileName);
        }

        //Add to DB
        public void AddItemToDb(Item item)
        {
            SaveImage(item);

            Dbcontext._dbContext.Items.Add(item);
            Dbcontext._dbContext.SaveChanges();
        }

        // Update Item on DataBase
        public void UpdateItemInDb(Item item)
        {
            SaveImage(item);
            var ItemToUpdate = Dbcontext._dbContext.Items.Single(i => i.Id == item.Id);

            ItemToUpdate.Name = item.Name;
            ItemToUpdate.Amount = item.Amount;
            ItemToUpdate.ExpiringDate = item.ExpiringDate;
            ItemToUpdate.Quantity = item.Quantity;
            ItemToUpdate.SupplierId = item.SupplierId;
            ItemToUpdate.CategoryId = item.CategoryId;
            ItemToUpdate.ImagePath = item.ImagePath;

            Dbcontext._dbContext.SaveChanges();
        }

        //Edit Item
        public ItemViewModel EditItemInDb(int id)
        {
            var item = Dbcontext._dbContext.Items.SingleOrDefault(i => i.Id == id);

            if (item == null)
                return null;

            //item.ImagePath = Path.GetFileName(item.ImagePath);

            var itemViewModel = new ItemViewModel
            {
                Item = item,
                Categories = GetAllCategories(),
                Supplier = GetAllSuppliers()
            };
            return itemViewModel;
        }

        //Delete Item
        public Item DeleteItem(int id)
        {
            var item = GetItemById(id);
            
            if (item == null)
                return null;
            else
            {
                // context._dbContext.Entry(item).State = EntityState.Detached;
                Dbcontext._dbContext.Entry(item).State = EntityState.Deleted;
                //context._dbContext.Items.Remove(item);
                Dbcontext._dbContext.SaveChanges();
                return item;
            }
        }

        //Item details
        public Item GetItemDetails(int id)
        {
           var item = GetItemById(id);
            if (item == null)
                return null;

            return item;
        }

        public List<Item> SearchDb(string search)
        {
            var items = GetAllProducts();

            switch (search)
            {
                case "Food":
                    return items.Where(c => c.Category.Name == search).ToList();
                case "House Hold":
                    return items.Where(c => c.Category.Name == search).ToList();
                case "Accessories":
                    return items.Where(c => c.Category.Name == search).ToList();
                case "Skin Care":
                    return items.Where(c => c.Category.Name == search).ToList();
                case "Others":
                    return items.Where(c => c.Category.Name == search || search == null).ToList();
                default:
                    return items;
            }
        }

        public List<string> GetAllOrder() {
            //Get the ID from the usercart
            var allUser = Dbcontext._dbContext.UserCart.Where(p => p.Processing == true).Select(i => i.UserId).ToList();

            List<string> usersId = new List<string>();

            foreach (var id in allUser)
            {
                // if the list does not contains the id, add it
                if (!usersId.Contains(id))
                {
                    usersId.Add(id);
                }
            }
            return usersId;
        }

        public List<UserCart> GetOrder(string userId)
        {
            var orders = Dbcontext._dbContext.UserCart.Where(i => i.UserId == userId).ToList();
            orders.Select(p => p.Processing == true);
            return orders;
        }


    }
}