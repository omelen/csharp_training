using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class RemoveContactsFromGroupTests : AuthTestBase
    {
        [Test]
        public void RemoveContactsFromGroupTest()
        {
            GroupData group = GroupContactRelation.GetGroupWithContacts();
            List<ContactData> oldContacts = group.GetContacts();
            ContactData toBeRemoved = oldContacts[0];

            app.Contacts.RemoveContactFromGroup(toBeRemoved, group);

            List<ContactData> newContacts = group.GetContacts();

            oldContacts.RemoveAt(0);

            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }

    }
}
