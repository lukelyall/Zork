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

public class NorthOfHouse : IArea
{
    public string Name => "North of House";
    public string Description => "You are facing the north side of a white house. There is no door here, and all the windows are boarded up. To the north a narrow path winds through the trees.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "south", "The windows are all boarded." }
    };

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class SouthOfHouse : IArea
{
    public string Name => "South of House";
    public string Description => "You are facing the south side of a white house. There is no door here, and all the windows are boarded";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "north", "The windows are all boarded." }
    };

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class GameFactory : IZorkFactory
{
    public IArea LoadArea()
    {
        IArea westOfHouse = new WestOfHouse();
        IArea forest = new Forest();
        IArea northOfHouse = new NorthOfHouse();
        IArea southOfHouse = new SouthOfHouse();

        westOfHouse.Exits.Add("north", northOfHouse);
        westOfHouse.Exits.Add("west", forest);
        westOfHouse.Exits.Add("south", southOfHouse);

        northOfHouse.Exits.Add("west", westOfHouse);

        southOfHouse.Exits.Add("west", westOfHouse);

        return westOfHouse;
    }
}

class Program
{
    static void Main()
    {
        IZorkFactory factory = new GameFactory();
        IArea area = factory.LoadArea();

        area.DisplayInformation();

        while (true)
        {
            string input = Console.ReadLine().Trim().ToLower();

            if (string.IsNullOrEmpty(input))
            {
                continue;
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