using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests.tests
{
    [TestFixture]
    public class GroupModificationsTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("Unicorn tears");
            newData.Header = "Rainbow";
            newData.Footer = "Sparkle test";

            app.Groups.Modify(1, newData);
        }
    }
}
