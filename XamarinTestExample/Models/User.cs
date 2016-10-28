using System;
namespace XamarinTestExample
{
    public struct User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public object Birthday { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string NameFor => FirstName[0].ToString();

        public int Age => ((DateTime)Birthday - DateTime.Now).Days;
    }
}