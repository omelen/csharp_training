using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactsTestBase : AuthTestBase
    {
        protected bool PerformLongUIChecks = true;

        [TearDown]
        public void CompareContacts_UI_DB()
        {
            if (!PerformLongUIChecks)
            {
                List<ContactData> fromUI = app.Contacts.GetContactList();
                List<ContactData> fromDB = ContactData.GetAll();

                fromUI.Sort();
                fromDB.Sort();

                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}
