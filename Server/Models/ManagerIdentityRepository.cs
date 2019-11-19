using System.Threading.Tasks;
using Csla;
using Csla.Server;

namespace Server.Models
{
    public class ManagerIdentityRepository : ObjectFactory
    {
        public async Task<object> Fetch(string name)
        {
            var s = ApplicationContext.ClientContext["Name"] as string;

            var obj = new ManagerIdentity
            {
                Name = name,
                FetchIsCorrect = name == s
            };

            await Task.Delay(1);
            return obj;
        }
    }
}