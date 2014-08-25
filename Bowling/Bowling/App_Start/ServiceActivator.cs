using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Bowling
{
    public class ServiceActivator : IHttpControllerActivator
    {
        private readonly IContainer _container;

        public ServiceActivator(IContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            try
            {
                var scopedContainer = _container.GetNestedContainer();
                scopedContainer.Inject(typeof(HttpRequestMessage), request);
                request.RegisterForDispose(scopedContainer);
                return (IHttpController)scopedContainer.GetInstance(controllerType);
            }
            catch (Exception e)
            {
                // TODO : Logging
                throw e;
            }
        }
    }
}