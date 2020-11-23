using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NET5.Models
{
    public partial class Calling
    {
        public int Id { get; set; }
        [Display(Name = "Дата")]
        public DateTime? Data { get; set; }
        [Display(Name = "Продовжуваність дзвінку")]
        public string During { get; set; }
        [Display(Name = "Користувач")]
        public int? Personid { get; set; }
        [Display(Name = "Місто виклику")]
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
