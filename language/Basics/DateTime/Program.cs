// DateTime structure
// https://learn.microsoft.com/en-us/dotnet/api/system.datetime?view=net-9.0
using Classes;

Student student = new Student();
student.Name = "Olsi";
student.BirthDate = DateTime.Parse("1987-05-22");
Console.WriteLine(student.Name);
Console.WriteLine(student.BirthDate);
Console.WriteLine(student.BirthDate.Day);
Console.WriteLine(student.BirthDate.Month);
Console.WriteLine(student.BirthDate.Year);
Console.WriteLine(student.BirthDate.DayOfWeek);

DateTime today = DateTime.Now;
Console.WriteLine("\nNOW");
Console.WriteLine(today);
Console.WriteLine("Short date: " + today.ToShortDateString());
Console.WriteLine("Long date: " + today.ToLongDateString());
Console.WriteLine("Short time: " + today.ToShortTimeString());
Console.WriteLine("Long time: " + today.ToLongTimeString());
Console.WriteLine("Today is " + today.DayOfWeek);
Console.WriteLine($"It's been {today.DayOfYear} since New Year");

DateTime customFormat = DateTime.ParseExact("2018/11/22 18:15", 
    "yyyy/MM/dd HH:mm", 
    System.Globalization.CultureInfo.InvariantCulture
);
Console.WriteLine("\nMarina");
Console.WriteLine(customFormat);
Console.WriteLine(customFormat.DayOfWeek);

DateTime myBirthDate = new DateTime(1977, 7, 3, 7, 0, 0);

Console.WriteLine("\nJC");
Console.WriteLine(myBirthDate);
Console.WriteLine(myBirthDate.ToLongDateString());
Console.WriteLine(myBirthDate.DayOfWeek);

Console.WriteLine("\nOther");
Console.WriteLine(DateTime.DaysInMonth(2024, 02)); // 29 (leap year)

// TimeSpan
// https://learn.microsoft.com/en-us/dotnet/api/system.timespan?view=net-9.0

Console.WriteLine(today.CompareTo(myBirthDate));  // 1 
Console.WriteLine(myBirthDate.CompareTo(student.BirthDate));  // -1 // JC is older than Olsi

TimeSpan myAge = today.Subtract(myBirthDate);
Console.WriteLine("My Age: " + myAge.TotalDays / 365);
TimeSpan ageDiff = student.BirthDate.Subtract(myBirthDate);
Console.WriteLine($"JC is years {Math.Floor(ageDiff.TotalDays / 365)} older than Olsi");

Console.WriteLine(myBirthDate.AddYears(3).ToShortDateString()); 