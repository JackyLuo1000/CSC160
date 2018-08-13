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
    public class PhoneNumber
    {
        [ProtoMember(1)]
        public string Number { get; set; }
        [ProtoMember(2)]
        public string PhoneType { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Number}: ");
            sb.Append(PhoneType);
            return sb.ToString();
        }
    }
}
