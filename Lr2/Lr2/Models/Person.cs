using System;
using System.Collections.Generic;
using System.Text;

namespace Lr2.Models
{
    public partial class Person
    {
        public Person()
        {
            Calling = new HashSet<Calling>();
        }

        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Fname { get; set; }
        public string Adress { get; set; }
        public int? Number { get; set; }

        public virtual ICollection<Calling> Calling { get; set; }

        public override string ToString()
        {
            StringBuilder person = new StringBuilder()
                .Append(Id)
                .Append("   ")
                .Append(Surname)
                .Append("   ")
                .Append(Name)
                .Append("   ")
                .Append(Fname)
                .Append("   ")
                .Append(Adress)
                .Append("   ")
                .Append(Number);
            return person.ToString();
        }
    }
}
