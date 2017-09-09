using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GoToHomePage();
            Login(new AccountData ("admin", "secret"));
            GoToGroupsPage();
            InitGroupcreation();
            GroupData group = new GroupData("Test name");
            group.Header = "Test header";
            group.Footer = "Test footer";
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            Logout();
        }
  }
}

