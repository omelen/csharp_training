using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupsTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            app.Navigator.GoToGroupsPage();
            if (!app.Groups.IsAnyGroupPresent())
            {
                GroupData group = new GroupData("");
                group.Header = "";
                group.Footer = "";
                app.Groups.Create(group);
            }
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData tobeRemoved = oldGroups[0];

            app.Groups.Remove(tobeRemoved);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();

            GroupData toBeRemoved = oldGroups[0];
            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            foreach(GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
