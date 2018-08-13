using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Model
{
    [ProtoContract]
    public class Contact
    {
        public Contact()
        {
            FirstName = "";
            LastName = "";
            PhoneNumbers = new List<PhoneNumber>();
            Emails = new List<Email>();
            ContactGroup = "";
        }
        [ProtoMember(1)]
        public string FirstName { get; set; }
        [ProtoMember(2)]
        public string LastName { get; set; }
        [ProtoMember(3)]
        public List<PhoneNumber> PhoneNumbers { get; set; }
        [ProtoMember(4)]
        public List<Email> Emails { get; set; }
        [ProtoMember(5)]
        public string ContactGroup { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
