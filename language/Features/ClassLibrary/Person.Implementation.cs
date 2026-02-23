namespace ClassLibrary
{
    public partial class Person
    {
        private string _address;
        private string[] _data = ["one", "two", "three"];

        public partial string Address { 
            get { return _address; } 
            set {
                if ( string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("The Address cannot be empty");
                _address = value; 
            } 
        }

        public partial string this[int index]
        {
            get
            {
                if (index < 0 || index >= _data.Length)
                    throw new IndexOutOfRangeException();
                return _data[index]; 
            }
        }

        public void Deconstruct(out Person person, out int? age, out string? gender) // tuple matching
        {
            person = this;
            age = Age;
            gender = Gender;
        }

        public override string ToString() => $"{FirstName} {LastName}, {Age} : Address: {Address}";
    }
}
