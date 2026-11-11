using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRepositories.Entities
{
    public record Name
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string? SurName { get; protected set; }
        public Name(string firstName, string lastName, string? surName)
        {
            FirstName = firstName;
            LastName = lastName;
            SurName = surName;
        }
        public override string ToString()
        {
            return $"{LastName} {FirstName} {SurName}";
        }
    }
}
