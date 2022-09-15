using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Class
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string FULL { get; set; }
        public override string ToString()
        {
            return $"{Id}. {Name} {Surname}";
        }
        public Author(int ıd, string? name, string? surname)
        {
            Id = ıd;
            Name = name;
            Surname = surname;
            FULL = Id.ToString()+Name+Surname;
        }   
    }
}

