using System;

namespace ZorkFactory;

public interface IZorkFactory
{
    IArea LoadArea();
}

public interface IArea
{
    string Name { get; }
    string Description { get; }
    Dictionary<string, IArea> Exits { get; }
    Dictionary<string, string> BlockedExits => new Dictionary<string, string>();

    public void DisplayInformation();
}

public interface IContainer
{
    string Name { get; }
    IItem Open();
}

public interface IItem
{
    string Name { get; }
    void Use();
}

public class WestOfHouse : IArea
{
    public string Name => "West of House";
    public string Description => "You are standing in an open field west of a white house, with a boarded front door.\n" +
                                 "There is a small mailbox here.";
    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "east", "The door is boarded and you can't remove the boards" }
    };

    private IContainer mailbox = new Mailbox();

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }

    public IContainer GetMailbox() => mailbox;
}

public class Mailbox : IContainer
{
    public string Name => "mailbox";

    public IItem Open()
    {
        Console.WriteLine("Opening the small mailbox reveals a leaflet.");
        return new Leaflet();
    }
}

public class Leaflet : IItem
{
    public string Name => "Leaflet";
    public void Use()
    {
        Console.WriteLine("(Taken)\n\"WELCOME TO ZORK!\n\nZORK is a game of adventure, danger, and low cunning. In it you will explore some of the most amazing territory ever seen by mortals. No computer should be without one!\"\n");
    }
}

public class Forest : IArea
{
    public string Name => "Forest";
    public string Description => "This is a forest. Trees in all directions. To the east, there appears to be sunlight";
    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class WestOfHouseFactory : IZorkFactory
{
    public IArea LoadArea()
    {
        IArea westOfHouse = new WestOfHouse();
        IArea forest = new Forest();

        westOfHouse.Exits.Add("north", forest);
        westOfHouse.Exits.Add("west", forest);
        westOfHouse.Exits.Add("south", forest);

        return westOfHouse;
    }
}

class Program
{
    static void Main()
    {
        IZorkFactory factory = new WestOfHouseFactory();
        IArea area = factory.LoadArea();

        area.DisplayInformation();
        var westOfHouse = area as WestOfHouse;
        IContainer mailbox = westOfHouse?.GetMailbox();

        while (true)
        {
            string input = Console.ReadLine().Trim().ToLower();

            if (string.IsNullOrEmpty(input)) continue;

            if (input == "open mailbox")
            {
                IItem leaflet = mailbox.Open();
                leaflet.Use();
            }

            else if (area.Exits.TryGetValue(input, out IArea nextArea))
            {
                area = nextArea;
                area.DisplayInformation();
            }
            else if (area.BlockedExits.ContainsKey(input))
            {
                Console.WriteLine(area.BlockedExits[input]);
            }
            else
            {
                Console.WriteLine($"I do not know the word \"{input}\"");
            }
        }
    }
}