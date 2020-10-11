using SmartStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Services.Interfaces
{
    public interface IAdminService
    {
        bool CreateStoreManager(ApplicationDbContext context, StoreManager storeManager);
    }
}
