namespace ClassLibrary
{
    public enum EmployeeType
    {
        FullTime, PartTime, Temporary, Seasonal, onCall, Casual
    }
    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public EmployeeType Type { get; set; } = EmployeeType.FullTime;

    }
}
