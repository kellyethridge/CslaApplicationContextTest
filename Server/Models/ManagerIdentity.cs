using System;
using System.Threading.Tasks;
using Csla;
using Csla.Server;
using DataPortal = Csla.DataPortal;

namespace Server.Models
{
    [Serializable]
    [ObjectFactory(typeof(ManagerIdentityRepository))]
    public class ManagerIdentity : BusinessBase<ManagerIdentity>
    {
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get => GetProperty(NameProperty);
            set => SetProperty(NameProperty, value);
        }

        public static readonly PropertyInfo<bool> FetchIsCorrectProperty = RegisterProperty<bool>(c => c.FetchIsCorrect);
        public bool FetchIsCorrect
        {
            get => GetProperty(FetchIsCorrectProperty);
            set => SetProperty(FetchIsCorrectProperty, value);
        }

        public static async Task<ManagerIdentity> GetAsync(string name)
        {
            ApplicationContext.ClientContext["Name"] = name;
            return await DataPortal.FetchAsync<ManagerIdentity>(name);
        }
    }
}