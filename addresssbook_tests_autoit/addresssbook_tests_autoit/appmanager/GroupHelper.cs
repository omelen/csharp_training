using System;
using System.Collections.Generic;

namespace addresssbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public static string DELETEGROUPWINTITLE = "Delete group";

        public GroupHelper(ApplicationManager manager) : base(manager)
        {

        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            OpenGroupsDialogue();
            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                "GetItemCount", "#0", "");
            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(
                    GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                    "GetText", "#0|#" + i, "");
                list.Add(new GroupData()
                {
                    Name = item
                });
            }
            CloseGroupsDialog();
            return list;
        }
        public void Add(GroupData newGroup)
        {
            OpenGroupsDialogue();
            InitNewGroupCreation();
            aux.Send(newGroup.Name);
            aux.Send("{ENTER}");
            CloseGroupsDialog();
        }
        public void Remove(GroupData removeGroup)
        {
            OpenGroupsDialogue();
            SelectGroup(removeGroup);
            InitGroupDeletion();
            SubmitGroupDeletion();
            CloseGroupsDialog();
        }

        public void InitNewGroupCreation()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
        }

        public void CloseGroupsDialog()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d54");
        }

        public void OpenGroupsDialogue()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d512");
            aux.WinWait(GROUPWINTITLE);
        }

        public void SelectGroup(GroupData group)
        {
            int index = 0;
            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                "GetItemCount", "#0", "");
            for (int i = 0; i < int.Parse(count); i++)
            {
                string groupName = aux.ControlTreeView(
                    GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                    "GetText", "#0|#" + i, "");
                if(groupName == group.Name)
                {
                    index = i;
                    break;
                }
            }

            aux.ControlTreeView(GROUPWINTITLE, "",
            "WindowsForms10.SysTreeView32.app.0.2c908d51", "Select", "#0|#" + index, "");
        }

        public void InitGroupDeletion()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d51");
            aux.WinWait(DELETEGROUPWINTITLE);
        }

        public void SubmitGroupDeletion()
        {
            aux.Send("{ENTER}");
            aux.Sleep(300);
        }

    }
}