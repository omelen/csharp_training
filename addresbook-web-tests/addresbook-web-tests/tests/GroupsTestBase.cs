using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupsTestBase : AuthTestBase
    {
        protected bool PerformLongUIChecks = true;

        [TearDown]
        public void CompareGroups_UI_DB()
        {
            if (PerformLongUIChecks)
            {
                List<GroupData> fromUI = app.Groups.GetGroupList();
                List<GroupData> fromDB = GroupData.GetAll();

                fromUI.Sort();
                fromDB.Sort();

                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}
