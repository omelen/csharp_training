using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class AddingContactsToGroupTests : AuthTestBase
    {
        [Test]
        public void AddingContactToGroupTest()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldContacts = group.GetContacts();
            ContactData contactData = ContactData.GetAll().Except(oldContacts).First();

            app.Contacts.AddContactToGroup(contactData, group);

            List<ContactData> newContacts = group.GetContacts();
            oldContacts.Add(contactData);

            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
