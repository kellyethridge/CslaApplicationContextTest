using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server
{
    [Serializable]
    public class MyPrincipal : Csla.Security.CslaPrincipal
    {
        private string[] Roles { get; set; }

        public MyPrincipal()
            : base(MyIdentity.New("Demo"))
        {
            this.Roles = new string[] { "Admin", "Tester" };
        }

        public override bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }
    }
}