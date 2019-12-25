using System.Threading.Tasks;
using Csla;
using Csla.Server;

namespace Server.Models
{
    public class ManagerIdentityRepository : ObjectFactory
    {
        public async Task<object> Fetch(string criteriaName)
        {
            await Task.Delay(10);

            var clientName = ApplicationContext.ClientContext["Name"] as string;
            var userName = ApplicationContext.User.Identity.Name;

            var obj = new ManagerIdentity
            {
                Name = $"Criteria: {criteriaName}, ClientContext: {clientName} , User: {userName}",
                FetchIsCorrect = criteriaName == clientName
            };

            return obj;
        }
    }
}