using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [Table(Name = "address_in_groups")]
    public class GroupContactRelation
    {
        [Column(Name = "group_id")]
        public string GroupId { get; set; }

        [Column(Name = "id")]
        public string ContactId { get; set; }

        public static GroupData GetGroupWithContacts()
        {
            using (AddressBookDb db = new AddressBookDb())
            {
                // get all relations
                GroupContactRelation firstRelation = (from gcr in db.GCR select gcr).ToList().First();

                // get first relation and get group using group_id
                GroupData group = (from g in db.Groups where g.Id == firstRelation.GroupId select g).First();
                return group;
            }
        }
    }
}
