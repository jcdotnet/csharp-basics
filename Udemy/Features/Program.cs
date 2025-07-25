// See https://aka.ms/new-console-template for more information
using Features;

Character c = new Character(Features.Type.Elf);
c.Name = "My Character";
Console.WriteLine(c.Armor);
Console.WriteLine(ArmedByKnife(c));
Console.WriteLine(c); // ToString()

List<Character> characters = new List<Character>()
{
    new Character(Features.Type.Elf),
    new Character(Features.Type.Ork)
};

List<Character> characters2 = new List<Character>() // C#6: collection initialization
{
    Features.Type.Elf,
    Features.Type.Ork
};

Point p = new Point() { X = 1, Y = 1 };
//int xx, yy;
p.GetCoords(out int xx, out int yy); // C#7: out variables
Console.WriteLine($"X: {xx}, Y:{yy}");

string digit = "10";
int.TryParse(digit, out int d); // out variables are commonly used in TryDoSomething scenarios
Console.WriteLine($"Parsed Digit: {d}");

#region static

static bool ArmedByKnife(Character c)
{
    //return c != null && c.Weapon != null && c.Weapon.Name == "Knife";
    return c?.Weapon?.Name == "Knife"; // C#6: safe navigation operator
}

static void StraightPatternMatching(object o)
{
    if (o is int x || (o is string s && int.TryParse(s, out x))) { // C# 7: pattern matching
        Console.WriteLine($"X parsed with matching {x}");
    }
}
static class Extensions
{
    internal static void Add(this IList<Character> characters, Features.Type type)
    {
        characters.Add(new Character(type));
    }
}

#endregion