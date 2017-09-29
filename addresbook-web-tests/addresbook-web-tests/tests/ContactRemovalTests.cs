using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests.tests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            if (!app.Contacts.IsAnyContactPresent())
            {
                app.Contacts
                .InitContactCreation()
                .FillContactForm(new ContactData("qqq", "111"))
                .SubmitContactCreation();
            }
            app.Contacts.Remove(1);
            app.Auth.Logout();
        }
    }
}
