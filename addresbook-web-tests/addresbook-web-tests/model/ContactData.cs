using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [Table("addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;

        private string detailsString;

        public ContactData()
        { }
        
        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        [Column("id", IsPrimaryKey = true, IsIdentity = true)]
        public string Id { get; set; }

        [Column("firstname")]
        public string Firstname { get; set; }

        [Column("lastname")]
        public string Lastname { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("home")]
        public string HomePhone { get; set; }

        [Column("mobile")]
        public string MobilePhone { get; set; }

        [Column("work")]
        public string WorkPhone { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("deprecated")]
        public string Deprecated { get; set; }

        public string AllPhones
        {
            get
            {
                if(allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }

            set
            {
                allPhones = value;
            }
        }

        public string DetailsString
        {
            get
            {
                if(detailsString != null)
                {
                    return detailsString;
                }
                else
                {
                    string result = "";

                    if (!string.IsNullOrEmpty(Firstname))
                    {
                        result = Firstname;
                    }
                    if (!string.IsNullOrEmpty(Lastname))
                    {
                        result = result + " " + Lastname;
                    }
                    if (!string.IsNullOrEmpty(Address))
                    {
                        result += "\r\n" + Address;
                    }

                    result += "\r\n";

                    if (!string.IsNullOrEmpty(HomePhone))
                    {
                        result += "\r\nH: " + HomePhone;
                    }
                    if (!string.IsNullOrEmpty(MobilePhone))
                    {
                        result += "\r\nM: " + MobilePhone;
                    }
                    if (!string.IsNullOrEmpty(WorkPhone))
                    {
                        result += "\r\nW: " + WorkPhone;
                    }

                    result += "\r\n";

                    if (!string.IsNullOrEmpty(Email))
                    {
                        result += "\r\n" + Email;
                    }

                    return result.Trim();
                }
            }
            set
            {
                detailsString = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Lastname == other.Lastname)
            {
                return Firstname.CompareTo(other.Firstname);
            }

            return Lastname.CompareTo(other.Lastname);
        }

        public bool Equals(ContactData other)
        {

            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Lastname == other.Lastname && Firstname == other.Firstname;
        }

        public override int GetHashCode()
        {
            return (Lastname + Firstname).GetHashCode();
        }

        public override string ToString()
        {
            return "Firstname=" + Firstname 
                + "\r\nLastname=" + Lastname
                + "\r\nAdress=" + Address
                + "\r\nHomePhone=" + HomePhone
                + "\r\nWorkPhone=" + WorkPhone
                + "\r\nMobilePhone=" + MobilePhone;
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDb db = new AddressBookDb())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }
    }
}
