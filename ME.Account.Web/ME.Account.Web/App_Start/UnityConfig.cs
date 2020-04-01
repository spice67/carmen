using Core.Common.Contracts;
using ME.Account.Web.Core.Business;
using ME.Account.Web.Core.Contracts;
using ME.Account.Web.Core.Data;
using ME.Account.Web.Models.api;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ME.Account.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<ICustomerInfoService, CustomerInfoService>();
            container.RegisterType<ITransactionInfoService, TransactionInfoService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}