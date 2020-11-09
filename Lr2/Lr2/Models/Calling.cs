using System;
using System.Collections.Generic;
using System.Text;

namespace Lr2.Models
{
    public partial class Calling
    {
        public int Id { get; set; }
        public DateTime? Data { get; set; }
        public string During { get; set; }
        public int? Personid { get; set; }
        public int? Cityid { get; set; }

        public virtual City City { get; set; }
        public virtual Person Person { get; set; }

        public override string ToString()
        {
            StringBuilder calling = new StringBuilder()
                .Append(Id)
                .Append("   ")
                .Append(Data)
                .Append("   ")
                .Append(During)
                .Append("   ")
                .Append(Personid)
                .Append("   ")
                .Append(Cityid);
            return calling.ToString();
        }
    }
}
