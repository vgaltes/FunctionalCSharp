using System;

namespace FunctionalCSharp.Tests
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Person MarryInTheOldFashionedWayTo(Person man)
        {
            if (man != null)
            {
                return new Person(this.FirstName,man.LastName);
            }

            Console.WriteLine(man.FirstName);
            throw new ArgumentNullException(nameof(man), "Can't marry a null man");
        }
    }
}
