// https://learn.microsoft.com/en-us/dotnet/standard/serialization/

using ClassLibrary;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

Country country = new Country() { CountryCode = "ES", Name = "Spain", Region = "Europe" };

string filePath = "spain.json";
FileStream fileStream =  new FileStream(filePath, FileMode.Create, FileAccess.Write);

// Binary serialization
// BinaryFormatter formatter = new BinaryFormatter(); // binary serialization is obsolete

// JSON serialization
var json = JsonSerializer.Serialize(country);

using (StreamWriter sw = new StreamWriter(fileStream))
{
    sw.WriteLine(json);
};

using (StreamReader sr = new StreamReader(filePath))
{
    var countryFromFile = JsonSerializer.Deserialize(sr.ReadToEnd(), typeof(Country));
    Console.WriteLine(countryFromFile);
}

fileStream.Close();

// XML serialization
filePath = "spain.xml";

XmlSerializer xmlSerializer = new XmlSerializer(typeof(Country));

using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
{
    xmlSerializer.Serialize(fs, country);
}

using (StreamReader sr = new StreamReader(filePath))
{
    var countryFromFile = xmlSerializer.Deserialize(sr);
    Console.WriteLine(countryFromFile);
}

// more
List<Country> countries = [
    new Country() { CountryCode = "JP", Name = "Japan", Region = "Asia" },
    new Country() { CountryCode = "ES", Name = "Spain", Region = "Europe" },
    new Country() { CountryCode = "US", Name = "United States of America", Region = "America" }
];

filePath = "countries.xml";

xmlSerializer = new XmlSerializer(typeof(List<Country>));

using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
{
    xmlSerializer.Serialize(fs, countries);
}

using (StreamReader sr = new StreamReader(filePath))
{
    var countriesFromFile = xmlSerializer.Deserialize(sr) as List<Country>;
    foreach (var countryFromFile in countriesFromFile)
    {
        Console.WriteLine(countryFromFile);
    }
    
}