using System.Threading.Tasks;
using System.Web.Http;
using Server.Models;

namespace Server.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var obj = await ManagerIdentity.GetAsync("Bob");

            return Ok(new
            {
                name = obj.Name,
                isCorrect = obj.FetchIsCorrect
            });
        }
    }
}