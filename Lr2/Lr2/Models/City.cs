using System;
using System.Collections.Generic;
using System.Text;

namespace Lr2.Models
{
    public partial class City
    {
        public City()
        {
            Calling = new HashSet<Calling>();
        }

        public int Id { get; set; }
        public string City1 { get; set; }
        public int? Code { get; set; }
        public decimal? Tariff { get; set; }

        public virtual ICollection<Calling> Calling { get; set; }

        public override string ToString()
        {
            StringBuilder city = new StringBuilder()
                .Append(Id)
                .Append("   ")
                .Append(City1)
                .Append("   ")
                .Append(Code)
                .Append("   ")
                .Append(Tariff);
            return city.ToString();
        }
    }
}
