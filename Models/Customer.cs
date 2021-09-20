using System;
using System.Collections.Generic;

namespace Models
{
    
    public class Customer
    {
        string customerName = Console.ReadLine();

        public Customer() { }

        //Constructor overloading (this is an example of polymorphism)
        //The constructor behaves differently
        //depending on what is passed in
        public Customer(string name) : this()
        {
            this.Name = name;
        }

        //constructor chaining
        public Customer(string name, int age) : this(name)
        {
            this.Age = age;
        }

        public Customer(string name, int age, string city) : this(name, age)
        {
        }

        //Property
        public string Name { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public List<Order> Orders { get; set; }
        
    }
}