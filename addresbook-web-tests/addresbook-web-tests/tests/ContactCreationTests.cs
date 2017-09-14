using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            app.Contacts
                .InitContactCreation()
                .FillContactForm(new ContactData("qqq", "111"))
                .SubmitContactCreation();
            app.Auth.Logout();
        }
    }
}
