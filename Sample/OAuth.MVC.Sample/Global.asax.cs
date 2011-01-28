using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using OAuth.Core;
using OAuth.Core.Interfaces;
using OAuth.Core.Provider;
using OAuth.Core.Provider.Inspectors;
using OAuth.Core.Storage;
using OAuth.Core.Storage.Interfaces;
using OAuth.MVC.Library.Controllers;
using OAuth.MVC.Sample.Infrastructure;


namespace OAuth.MVC.Sample
{
  // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
  // visit http://go.microsoft.com/?LinkId=9394801

  public class MvcApplication : NinjectHttpApplication
  {
      protected static void RegisterRoutes(RouteCollection routes)
      {
          routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          routes.MapRoute(
              "Default",                                              // Route name
              "{controller}/{action}/{id}",                           // URL with parameters
              new { controller = "SignIn", action = "Login", id = "" }  // Parameter defaults
          );

      }
      protected override void OnApplicationStarted()
      {
          base.OnApplicationStarted();
          RegisterRoutes(RouteTable.Routes);
      }
      protected override IKernel CreateKernel()
      {
          var kernel = new StandardKernel(new SampleModule());
		  kernel.Load(Assembly.GetExecutingAssembly());
		  return kernel;
      }
   
  }














}