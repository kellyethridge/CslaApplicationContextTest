using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla;

namespace Server
{
    [Serializable]
    public class MyIdentity : Csla.Security.CslaIdentity
    {
        public static MyIdentity New(string name)
        {
            return DataPortal.Create<MyIdentity>(name);
        }

        [Create]
        private void CreateMyIdentity(string name)
        {
            Name = name;
        }
    }
}