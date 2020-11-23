using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NET5.Models
{
    public partial class City
    {
        public City()
        {
            Calling = new HashSet<Calling>();
        }

        public int Id { get; set; }
        [Display(Name = "Місто")]
        public string City1 { get; set; }
        [Display(Name = "Код міста")]
        public int? Code { get; set; }
        [Display(Name = "Тариф на дзвінки")]
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
