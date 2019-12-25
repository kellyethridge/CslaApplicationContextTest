using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Csla;
using Server.Models;

namespace Server.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                ApplicationContext.User = new MyPrincipal();
                var threadId = Thread.CurrentThread.ManagedThreadId.ToString();

                var obj = await ManagerIdentity.GetAsync(threadId);

                return Ok(new
                {
                    name = obj.Name,
                    isCorrect = obj.FetchIsCorrect
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Ok(new
                {
                    name = "Error",
                    isCorrect = false
                });
            }
        }
    }
}