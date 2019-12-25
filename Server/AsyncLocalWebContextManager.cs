using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using Csla;
using Csla.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Server
{
    public class AsyncLocalWebContextManager : IContextManager
    {
        private const string _localContextName = "Csla.LocalContext";
        private const string _clientContextName = "Csla.ClientContext";
        private const string _globalContextName = "Csla.GlobalContext";
        private static readonly AsyncLocal<HttpContext> AsyncHttpContext = new AsyncLocal<HttpContext>();

        public AsyncLocalWebContextManager(HttpApplication httpApplication)
        {
            httpApplication.BeginRequest += HttpApplication_BeginRequest;
        }

        private void HttpApplication_BeginRequest(object sender, EventArgs e)
        {
            HttpContext = HttpContext.Current;
        }

        /// <summary>
        /// Gets a value indicating whether this
        /// context manager is valid for use in
        /// the current environment.
        /// </summary>
        public bool IsValid
        {
            get { return HttpContext != null; }
        }

        /// <summary>
        /// Gets the current principal.
        /// </summary>
        public System.Security.Principal.IPrincipal GetUser()
        {
            var result = HttpContext.User;
            if (result == null)
            {
                result = new Csla.Security.UnauthenticatedPrincipal();
                SetUser(result);
            }
            return result;
        }

        /// <summary>
        /// Sets the current principal.
        /// </summary>
        /// <param name="principal">Principal object.</param>
        public void SetUser(System.Security.Principal.IPrincipal principal)
        {
            HttpContext.User = principal;
        }

        /// <summary>
        /// Gets the local context.
        /// </summary>
        public ContextDictionary GetLocalContext()
        {
            return (ContextDictionary)HttpContext.Items[_localContextName];
        }

        /// <summary>
        /// Sets the local context.
        /// </summary>
        /// <param name="localContext">Local context.</param>
        public void SetLocalContext(ContextDictionary localContext)
        {
            HttpContext.Items[_localContextName] = localContext;
        }

        /// <summary>
        /// Gets the client context.
        /// </summary>
        public ContextDictionary GetClientContext()
        {
            return (ContextDictionary)HttpContext.Items[_clientContextName];
        }

        /// <summary>
        /// Sets the client context.
        /// </summary>
        /// <param name="clientContext">Client context.</param>
        public void SetClientContext(ContextDictionary clientContext)
        {
            HttpContext.Items[_clientContextName] = clientContext;
        }

        /// <summary>
        /// Gets the global context.
        /// </summary>
        public ContextDictionary GetGlobalContext()
        {
            return (ContextDictionary)HttpContext.Items[_globalContextName];
        }

        /// <summary>
        /// Sets the global context.
        /// </summary>
        /// <param name="globalContext">Global context.</param>
        public void SetGlobalContext(ContextDictionary globalContext)
        {
            HttpContext.Items[_globalContextName] = globalContext;
        }

        /// <summary>
        /// Gets the default IServiceProvider
        /// </summary>
        public IServiceProvider GetDefaultServiceProvider()
        {
            IServiceProvider result;
            result = (IServiceProvider)Csla.ApplicationContext.LocalContext["__dsp"];
            if (result == null)
                result = GetDefaultServiceProvider();
            return result;
        }

        /// <summary>
        /// Sets the default IServiceProvider
        /// </summary>
        /// <param name="serviceProvider">IServiceProvider instance</param>
        public void SetDefaultServiceProvider(IServiceProvider serviceProvider)
        {
            Csla.ApplicationContext.LocalContext["__dsp"] = serviceProvider;
        }

        /// <summary>
        /// Gets the service provider scope
        /// </summary>
        /// <returns></returns>
        public IServiceScope GetServiceProviderScope()
        {
            return (IServiceScope)ApplicationContext.LocalContext["__sps"];
        }

        /// <summary>
        /// Sets the service provider scope
        /// </summary>
        /// <param name="scope">IServiceScope instance</param>
        public void SetServiceProviderScope(IServiceScope scope)
        {
            Csla.ApplicationContext.LocalContext["__sps"] = scope;
        }

        private static HttpContext HttpContext
        {
            get => AsyncHttpContext.Value;
            set => AsyncHttpContext.Value = value;
        }
    }
}