using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Security;
using System.ServiceModel;
using Core.Common.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ME.AccntRedis.Controllers
{
    public class ApiControllerBase : ControllerBase, IServiceAwareController
    {
        List<IServiceContract> _DisposableServices;

        protected virtual void RegisterServices(List<IServiceContract> disposableServices)
        {
        }

        void IServiceAwareController.RegisterDisposableServices(List<IServiceContract> disposableServices)
        {
            RegisterServices(disposableServices);
        }

        List<IServiceContract> IServiceAwareController.DisposableServices
        {
            get
            {
                if (_DisposableServices == null)
                    _DisposableServices = new List<IServiceContract>();

                return _DisposableServices;
            }
        }

        //protected void ValidateAuthorizedUser(string userRequested)
        //{
        //    string userLoggedIn = User.Identity.Name;
        //    if (userLoggedIn != userRequested)
        //        throw new SecurityException("Attempting to access data for another user.");
        //}

        protected IActionResult GetHttpResponse(HttpRequestMessage request, Func<IActionResult> codeToExecute)
        {
            IActionResult response = null;

            try
            {
                response = codeToExecute.Invoke();
            }
            catch (SecurityException ex)
            {
                response = Unauthorized(ex.Message);
            }
            catch (FaultException<AuthorizationValidationException> ex)
            {
                response = Unauthorized(ex.Message);
            }
            catch (FaultException ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }
    }
}