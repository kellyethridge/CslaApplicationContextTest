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

        /// <summary>
        /// Gets a value indicating whether this
        /// context manager is valid for use in
        /// the current environment.
        /// </summary>
        public bool IsValid
        {
            get { return AsyncHelper.HttpContext != null; }
        }

        /// <summary>
        /// Gets the current principal.
        /// </summary>
        public System.Security.Principal.IPrincipal GetUser()
        {
            var result = AsyncHelper.HttpContext.User;
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
            AsyncHelper.HttpContext.User = principal;
        }

        /// <summary>
        /// Gets the local context.
        /// </summary>
        public ContextDictionary GetLocalContext()
        {
            return (ContextDictionary)AsyncHelper.HttpContext.Items[_localContextName];
        }

        /// <summary>
        /// Sets the local context.
        /// </summary>
        /// <param name="localContext">Local context.</param>
        public void SetLocalContext(ContextDictionary localContext)
        {
            AsyncHelper.HttpContext.Items[_localContextName] = localContext;
        }

        /// <summary>
        /// Gets the client context.
        /// </summary>
        public ContextDictionary GetClientContext()
        {
            return (ContextDictionary)AsyncHelper.HttpContext.Items[_clientContextName];
        }

        /// <summary>
        /// Sets the client context.
        /// </summary>
        /// <param name="clientContext">Client context.</param>
        public void SetClientContext(ContextDictionary clientContext)
        {
            AsyncHelper.HttpContext.Items[_clientContextName] = clientContext;
        }

        /// <summary>
        /// Gets the global context.
        /// </summary>
        public ContextDictionary GetGlobalContext()
        {
            return (ContextDictionary)AsyncHelper.HttpContext.Items[_globalContextName];
        }

        /// <summary>
        /// Sets the global context.
        /// </summary>
        /// <param name="globalContext">Global context.</param>
        public void SetGlobalContext(ContextDictionary globalContext)
        {
            AsyncHelper.HttpContext.Items[_globalContextName] = globalContext;
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

        public static class AsyncHelper
        {
            private static readonly AsyncLocal<HttpContext> AsyncHttpContext = new AsyncLocal<HttpContext>();

            public static HttpContext HttpContext
            {
                get => AsyncHttpContext.Value;
                set
                {
                    Debug.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");
                    AsyncHttpContext.Value = value;
                }
            }
        }
    }
}