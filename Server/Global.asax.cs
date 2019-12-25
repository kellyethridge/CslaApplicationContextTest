using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Csla;

namespace Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        public override void Init()
        {
            base.Init();
            ApplicationContext.WebContextManager = new AsyncLocalWebContextManager(this);
        }
    }
}
