// Structure type: stores (encapsulate) data and related functionality
// Structure type: value type
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct

namespace Structure
{
    

    public struct Category // : ValueType // internally derived from System.ValueType
    {
        private int _id;
        private string _name;

        // parameterized constructor (optional)
        // explicit constructors: unlike classes, default is implicitly created
        public Category(int id, string name)
        {
            // prior to C#11 all fields had to be initialized in the constructor
            _id = id;
            _name = name;
        }
        public int Id
        {
            set
            {
                if (value >= 0 && value <= 100)
                    _id = value;
            }

            get { return _id; }
        }

        public string Name { get => _name; set => _name = value; }

        public int GetCategoryLength()
        {
            return this._name.Length; // this._name.Length;
        }
    }


    internal class Program
    {
        static void Main()
        {
            Category category = new Category(); // not creating an object
            category.Id = 1;
            category.Name = "C#";
            Console.WriteLine(category.Id);
            Console.WriteLine(category.Name);
            Console.WriteLine(category.GetCategoryLength());
        }
    }
}
