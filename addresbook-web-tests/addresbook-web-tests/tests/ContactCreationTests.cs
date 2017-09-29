using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            app.Contacts.Create(new ContactData("qqq", "111"));
            app.Auth.Logout();
        }
    }
}
