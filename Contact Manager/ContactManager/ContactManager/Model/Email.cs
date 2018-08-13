using ContactManager.Enum;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Model
{
    [ProtoContract]
    public class Email
    {
        [ProtoMember(1)]
        public string EmailContact { get; set; }
        [ProtoMember(2)]
        public string EmailType { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{EmailContact}: ");
            sb.Append(EmailType);
            return sb.ToString();
        }
    }
}
