using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests.tests
{
    [TestFixture]
    public class GroupModificationsTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            app.Navigator.GoToGroupsPage();
            if (!app.Groups.IsAnyGroupPresent())
            {
                GroupData group = new GroupData("");
                group.Header = "";
                group.Footer = "";
                app.Groups.Create(group);
            }

            GroupData newData = new GroupData("Unicorn tears");
            newData.Header = null;
            newData.Footer = null;
            app.Groups.Modify(1, newData);
        }
    }
}
