using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowFactory.Model
{
    class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {}
        public Person(int Id, string Name, string SurName, string PhoneNumber, string Email)
        {
            this.Id = Id;
            this.Name = Name;
            this.Surname = SurName;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name}, {Surname}, {PhoneNumber}, {Email} \n";
        }
    }
}
