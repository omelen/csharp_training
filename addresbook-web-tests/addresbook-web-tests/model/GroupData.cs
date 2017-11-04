using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [Table("group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        public GroupData()
        { }

        public GroupData(string name)
        {
            Name = name;
        }

        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name= " + Name
                + "\r\nheader= " + Header
                + "\r\nfooter= " + Footer;
        }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        [Column("group_id", IsPrimaryKey = true, IsIdentity = true)]
        public string Id { get; set; }

        [Column("group_name")]
        public string Name { get; set; }

        [Column("group_header")]
        public string Header { get; set; }

        [Column("group_footer")]
        public string Footer { get; set; }

        [Column("deprecated")]
        public string Deprecated { get; set; }

        public static List<GroupData> GetAll()
        {
            using (AddressBookDb db = new AddressBookDb())
            {
                return (from g in db.Groups.Where(x => x.Deprecated == "0000-00-00 00:00:00") select g).ToList();
            }
        }

        public List<ContactData> GetContacts()
        {
            using (AddressBookDb db = new AddressBookDb())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00")
                        from gcr in db.GCR.Where(rel => rel.GroupId == Id && rel.ContactId == c.Id)
                        select c).Distinct().ToList();
            }
        }
    }
}
