namespace ClassLibrary
{
    public partial class Person(string firstName, string lastName, int age) // primary constructor (C# 12)
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public int Age { get; set; } = age;
        public string Gender { get; set; } = "Female";

        // partial property (introduced in C# 13) declaration
        public partial string Address { get; set; }

        // partial indexer (introduced in C# 13) declaration
        public partial string this[int index] { get; }
    }
}
