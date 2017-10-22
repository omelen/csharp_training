using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]

    public class ContactInformationTest : AuthTestBase
    {
        [Test]

        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            //verifiction
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }

        [Test]

        public void ContactInformationDetailsConsistencyWithEditFormTest()
        {
            string detailsString = app.Contacts.GetContactInformatonFromDetailsPage(0);
            ContactData editForm = app.Contacts.GetContactInformationFromEditForm(0);

            Assert.AreEqual(detailsString, editForm.DetailsString);
        }
    }
}
