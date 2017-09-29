using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests.tests
{
    [TestFixture]
    public class ContactModificaionTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Navigator.GoToHomePage();
            if (!app.Contacts.IsAnyContactPresent())
            {
                ContactData contact = new ContactData("S", "M");
                app.Contacts.Create(contact);
            }
            ContactData newData = new ContactData("Johny", "Kappa");
            app.Contacts.Modify(1, newData);
        }

    }
}
