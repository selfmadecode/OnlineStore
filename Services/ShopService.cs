using SmartStore.Models;
using SmartStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartStore.Services
{
    public class ShopService
    {
        public static Context context = new Context();
        //List of all products

        public static List<Item> GetAllProducts()
        {
            var allItems = context._dbContext.Items.Include(c => c.Category).Include(s => s.Supplier).ToList();
            return allItems;
        }

        //Get Items by id
        public static Item GetItemById(int id)
        {
            var item = context._dbContext.Items.Include(i => i.Supplier).Include(i => i.Category).Single(i => i.Id == id);
            return item;
        }
        //List of Category
        public static List<Category> GetAllCategories()
        {
            var getCategoryFromDb = context._dbContext.Categories.ToList();
            return getCategoryFromDb;
        }
        //List of Suppliers
        public static List<Supplier> GetAllSuppliers()
        {
            var getSupplierFromDb = context._dbContext.Suppliers.ToList();
            return getSupplierFromDb;
        }

        //Create the view Model
        public static ItemViewModel itemViewModel(Item item = null)
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

        //Add to DB
        public static void AddItemToDb(Item item)
        {
            context._dbContext.Items.Add(item);
            context._dbContext.SaveChanges();
        }

        // Update Item on DataBase
        public static void UpdateItemInDb(Item item)
        {
            var ItemToUpdate = context._dbContext.Items.Single(i => i.Id == item.Id);

            ItemToUpdate.Name = item.Name;
            ItemToUpdate.Amount = item.Amount;
            ItemToUpdate.ExpiringDate = item.ExpiringDate;
            ItemToUpdate.Quantity = item.Quantity;
            ItemToUpdate.SupplierId = item.SupplierId;
            ItemToUpdate.CategoryId = item.CategoryId;

            context._dbContext.SaveChanges();
        }

        //Edit Item
        public static ItemViewModel EditItemInDb(int id)
        {
            var item = context._dbContext.Items.SingleOrDefault(i => i.Id == id);

            if (item == null)
                return null;

            var itemViewModel = new ItemViewModel
            {
                Item = item,
                Categories = GetAllCategories(),
                Supplier = GetAllSuppliers()
            };

            return itemViewModel;
        }

        //Delete Item
        public static Item DeleteItem(int id)
        {
            var item = GetItemById(id);
            
            if (item == null)
                return null;
            else
            {
                // context._dbContext.Entry(item).State = EntityState.Detached;
                context._dbContext.Entry(item).State = EntityState.Deleted;
                //context._dbContext.Items.Remove(item);
                context._dbContext.SaveChanges();
                return item;
            }
            
        }

        //Item details
        public static Item GetItemDetails(int id)
        {
           var item = GetItemById(id);
            if (item == null)
                return null;

            return item;
        }
        public static List<Item> SearchDb(string search)
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
    }
}