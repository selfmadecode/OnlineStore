using Ninject;
using SmartStore.Services;
using SmartStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStore.Infrastructure
{
    public class NinjectResolver : IDependencyResolver
    {
        private IKernel _kernel;
        public NinjectResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        private void AddBindings()
        {
            _kernel.Bind<IShopService>().To<ShopService>();
            _kernel.Bind<ICartService>().To<CartService>();
            _kernel.Bind<IAdminService>().To<AdminService>();
        }

        public object GetService(Type serviceType) => _kernel.TryGet(serviceType);

        public IEnumerable<object> GetServices(Type serviceType) => _kernel.GetAll(serviceType);
    }
}