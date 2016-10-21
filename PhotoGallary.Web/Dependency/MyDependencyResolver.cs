using Microsoft.Practices.Unity;
using PhotoGallary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dependency
{
    public class MyDependencyResolver : IDependencyResolver
    {
        readonly IUnityContainer _container;

        public MyDependencyResolver()
        {
            _container = new UnityContainer();
            _container.RegisterType<IPhotoService, PhotoService>();
          //  _container.RegisterType<IContext, Context>();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch
            {
                return null;
            }
        }
    }
}
