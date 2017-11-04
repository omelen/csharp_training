using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests.tests
{
    [TestFixture]
    public class ContactRemovalTests : ContactsTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            if (!app.Contacts.IsAnyContactPresent())
            {
                ContactData contact = new ContactData("S", "M");
                app.Contacts.Create(contact);
            }
            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData tobeRemoved = oldContacts[0];

            app.Contacts.Remove(tobeRemoved);

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(tobeRemoved.Id, contact.Id);
            }
        }
    }
}
