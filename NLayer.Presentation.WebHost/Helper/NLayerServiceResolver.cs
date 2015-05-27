using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace NLayer.Presentation.WebHost.Helper
{
    public class NLayerServiceResolver : IServiceResolver
    {
        public T Resolve<T>()
        {
            return (T)DependencyResolver.Current.GetService(typeof(T));
            //return ServiceLocator.Current.GetInstance<T>();
        }

        public object Resolve(Type type)
        {
            var obj = DependencyResolver.Current.GetService(type);
            return obj;
            //var obj = ServiceLocator.Current.GetInstance(type);
            //return obj;
        }
    }
}