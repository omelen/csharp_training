using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificaionTests : ContactsTestBase
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

            List<ContactData> oldContacts = ContactData.GetAll();

            ContactData oldData = oldContacts[0];

            app.Contacts.Modify(oldData, newData);

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach(ContactData contact in newContacts)
            {
                if(contact.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Firstname, contact.Firstname);
                    Assert.AreEqual(newData.Lastname, contact.Lastname);
                }
            }
        }

    }
}
