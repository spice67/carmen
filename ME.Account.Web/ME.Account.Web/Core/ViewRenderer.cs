﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ME.Account.Web.Core
{
    /// <summary>
    /// Class that renders MVC views to a string using the
    /// standard MVC View Engine to render the view. 
    /// </summary>
    public class ViewRenderer
    {
        /// <summary>
        /// Required Controller Context
        /// </summary>
        protected ControllerContext Context { get; set; }

        /// <summary>
        /// Initializes the ViewRenderer with a Context.
        /// </summary>
        /// <param name="controllerContext">
        /// If you are running within the context of an ASP.NET MVC request pass in
        /// any controller's context. 
        /// If no context is available, ViewRenderer attempts to create one 
        /// based on the embedded ErrorController class.
        /// </param>
        public ViewRenderer(ControllerContext controllerContext = null)
        {
            if (controllerContext == null)
            {
                if (HttpContext.Current != null)
                    controllerContext =
                        CreateController<EmptyController>()
                             .ControllerContext;
                else
                    throw new InvalidOperationException(
                      "ViewRenderer must run in the context of an " +
                      "ASP.NET Application and requires " +
                      "HttpContext.Current to be present.");
            }
            Context = controllerContext;
        }

        /// <summary>
        /// Renders a full MVC view to a string. Will render with the full MVC
        /// View engine including running _ViewStart and merging into _Layout        
        /// </summary>
        /// <param name="viewPath">
        /// The path to the view to render. Either in same controller, shared by 
        /// name or as fully qualified ~/ path including extension
        /// </param>
        /// <param name="model">The model to render the view with</param>
        /// <returns>String of the rendered view or null on error</returns>
        public string RenderViewToString(string viewPath, object model)
        {
            return RenderViewToStringInternal(viewPath, model, false);
        }


        /// <summary>
        /// Renders a partial MVC view to string. Use this method to render
        /// a partial view that doesn't merge with _Layout and doesn't fire
        /// _ViewStart.
        /// </summary>
        /// <param name="viewPath">
        /// The path to the view to render. Either in same controller, shared by 
        /// name or as fully qualified ~/ path including extension
        /// </param>
        /// <param name="model">The model to pass to the ViewRenderer</param>
        /// <returns>String of the rendered view or null on error</returns>
        public string RenderPartialViewToString(string viewPath,
                                        object model)
        {
            return RenderViewToStringInternal(viewPath, model, true);
        }

        /// <summary>
        /// Renders a partial MVC view to string. Use this method to render
        /// a partial view that doesn't merge with _Layout and doesn't fire
        /// _ViewStart.
        /// </summary>
        /// <param name="viewPath">
        /// The path to the view to render. Either in same controller, shared by 
        /// name or as fully qualified ~/ path including extension
        /// </param>
        /// <param name="model">The model to pass to the ViewRenderer</param>
        /// <param name="controllerContext">Active Controller context</param>
        /// <returns>String of the rendered view or null on error</returns>
        public static string RenderView(string viewPath,
                                        object model = null,
                            ControllerContext controllerContext = null)
        {
            var renderer = new ViewRenderer(controllerContext);
            return renderer.RenderViewToString(viewPath, model);
        }


        /// <summary>
        /// Renders a partial MVC view to string. Use this method to render
        /// a partial view that doesn't merge with _Layout and doesn't fire
        /// _ViewStart.
        /// </summary>
        /// <param name="viewPath">
        /// The path to the view to render. Either in same controller, shared by 
        /// name or as fully qualified ~/ path including extension
        /// </param>
        /// <param name="model">The model to pass to the ViewRenderer</param>
        /// <param name="controllerContext">Active controller context</param>
        /// <returns>String of the rendered view or null on error</returns>
        public static string RenderPartialView(string viewPath,
                                                  object model = null,
                         ControllerContext controllerContext = null)
        {
            var renderer = new ViewRenderer(controllerContext);
            return renderer.RenderPartialViewToString(viewPath, model);
        }


        /// <summary>
        /// Internal method that handles rendering of either partial or 
        /// or full views.
        /// </summary>
        /// <param name="viewPath">
        /// The path to the view to render. Either in same controller, shared by 
        /// name or as fully qualified ~/ path including extension
        /// </param>
        /// <param name="model">Model to render the view with</param>
        /// <param name="partial">Determines whether to render a full or partial view</param>
        /// <returns>String of the rendered view</returns>
        protected string RenderViewToStringInternal(string viewPath,
                                                  object model,
                                                  bool partial)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(
                                                    Context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(
                                               Context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException(
                       "View could not be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            Context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(Context, view,
                                          Context.Controller.ViewData,
                                          Context.Controller.TempData,
                                          sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }


        /// <summary>
        /// Creates an instance of an MVC controller from scratch 
        /// and initializes its controller context when no
        /// existing ControllerContext is present.       
        /// </summary>
        /// <typeparam name="T">Type of the controller to create</typeparam>
        /// <returns>Controller Context for T</returns>
        /// <exception cref="InvalidOperationException">thrown if HttpContext not available</exception>
        public static T CreateController<T>(RouteData routeData = null)
                    where T : Controller, new()
        {
            // create a disconnected controller instance
            T controller = new T();

            // get context wrapper from HttpContext if available
            HttpContextBase wrapper = null;
            if (HttpContext.Current != null)
                wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
            else
                throw new InvalidOperationException(
                    "Can't create Controller Context if no active " +
                    "HttpContext instance is available.");

            if (routeData == null)
                routeData = new RouteData();

            // add the controller routing if not existing
            if (!routeData.Values.ContainsKey("controller") &&
                !routeData.Values.ContainsKey("Controller"))

                routeData.Values.Add("controller",
                                      controller.GetType()
                                           .Name
                                           .ToLower()
                                           .Replace("controller", ""));

            controller.ControllerContext = new ControllerContext(wrapper,
                                                                routeData, controller);
            return controller;
        }
    }

    /// <summary>
    /// Empty MVC Controller instance that can be used to create
    /// a generic controller instance and context for the
    ///  CreateContext method.
    /// </summary>
    public class EmptyController : Controller
    { }
}