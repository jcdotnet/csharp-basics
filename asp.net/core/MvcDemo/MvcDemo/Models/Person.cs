namespace MvcDemo.Models
{
    public class Person
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }

    }

    public enum Gender
    {
        Male, Female, Other

    }
}
