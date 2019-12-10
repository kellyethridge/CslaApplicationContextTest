using System.Diagnostics;
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
                var obj = await ManagerIdentity.GetAsync("Bob");

                return Ok(new
                {
                    name = obj.Name,
                    isCorrect = obj.FetchIsCorrect
                });
            }
            catch (DataPortalException ex)
            {
                Debug.WriteLine(ex);
                return Ok(new
                {
                    name="Error",
                    isCorrect = false
                });
            }
        }
    }
}