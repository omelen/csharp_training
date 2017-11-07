using AutoItX3Lib;

namespace addresssbook_tests_autoit
{
    public class ApplicationManager
    {
        public static string WINTITLE = "Free Address Book";
        private AutoItX3 aux;

        private GroupHelper groupHepler;

        public ApplicationManager()
        {
            aux = new AutoItX3();
            aux.Run(@"C:\FreeAddressBookPortable\AddressBook.exe","",aux.SW_SHOW);
            aux.WinWait(WINTITLE);

            groupHepler = new GroupHelper(this);
        }

        public void Stop()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d510");
        }

        public AutoItX3 Aux
        {
            get
            {
                return aux;
            }
        }

        public GroupHelper Groups
        {
            get
            {
                return groupHepler;
            }
        }
    }

}
