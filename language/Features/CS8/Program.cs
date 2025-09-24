using ClassLibrary;
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-80
// https://learn.microsoft.com/en-us/shows/connect-microsoft-connect--2018/d140

// static local functions
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions
{
    Student student = new Student();
    student.DisplayGradesWithStaticFunction(100, 80); // 100, 80, 90
}

// feat: pattern matching // switch expressions
Console.WriteLine("--- Switch Expressions ---");
string role = "admin";
string access;


switch (role) // classic switch
{
    case "admin":
        access = "Full Access";
        break;
    case "user":
        access = "Limited Access";
        break;
    case "guest":
        access = "Read Only";
        break;
    default: access = "No Access"; break;
}

access = role switch // switch expressions
{
    "admin" => "Full Access",
    "user" => "Limited Access",
    "guest" => "Read Only",
    _ => "No Access"
};

Console.WriteLine($"Access: {access}");

Months month = Months.March;
Console.WriteLine(CheckMonthOldWay(month));
Console.WriteLine(CheckMonth(month));

#region methods
static string CheckMonth(Months month) => month switch
{
    Months.January => "Winter",
    Months.February => "Winter",
    Months.March => "Winter",
    Months.April => "Spring",
    Months.May => "Spring",
    Months.June => "Spring",
    Months.July => "Summer",
    Months.August => "Summer",
    Months.September => "Summer",
    Months.October => "Autumn",
    Months.November => "Autumn",
    Months.December => "Winter",
    _ => "Invalid Input"
};
static string CheckMonthOldWay(Months month)
{
    switch (month)
    {
        case Months.December:
        case Months.January:
        case Months.February:
        case Months.March:
            return "Winter";

        case Months.April:
        case Months.May:
        case Months.June:
            return "Spring";

        case Months.July:
        case Months.August:
        case Months.September:
            return "Summer";

        case Months.October:
        case Months.November:
            return "Autumn";

        default:
            return "Invalid input";
    }

}
#endregion