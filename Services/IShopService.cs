using SmartStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Services
{
    public interface IShopService
    {
        List<Item> GetAllProducts();

        Item GetItemById(int id);

        List<Category> GetAllCategories();

        List<Supplier> GetAllSuppliers();

        void AddItemToDb(Item item);

        Item DeleteItem(int id);

        List<Item> SearchDb(string search);

        Item GetItemDetails(int id);
    }
}