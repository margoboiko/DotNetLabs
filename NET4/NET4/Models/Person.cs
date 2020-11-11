using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NET4.Models
{
    public partial class Person
    {
        public Person()
        {
            Calling = new HashSet<Calling>();
        }

        public int Id { get; set; }
        [Display(Name = "Прізвище")]
        public string Surname { get; set; }
        [Display(Name = "Ім'я")]
        public string Name { get; set; }
        [Display(Name = "По батькові")]
        public string Fname { get; set; }
        [Display(Name = "Адреса")]
        public string Adress { get; set; }
        [Display(Name = "Номер телефону")]
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
