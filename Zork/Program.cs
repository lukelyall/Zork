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

    List<IContainer> Containers => new List<IContainer>();
    List<IItem> Items => new List<IItem>();

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
    string Description { get; }
    bool inInventory { get; set; }

    void PickUp();
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

    private readonly List<IContainer> _containers = new();
    private readonly List<IItem> _items = new();

    public List<IContainer> Containers => _containers;
    public List<IItem> Items => _items;

    public WestOfHouse()
    {
        _containers.Add(new Mailbox());
    }

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class Mailbox : IContainer
{
    public string Name => "mailbox";
    private bool isOpened = false;
    private IItem item = new Leaflet();

    public IItem Open()
    {
        if (!isOpened)
        {
            Console.WriteLine("Opening the small mailbox reveals a leaflet.");
            isOpened = true;
            return item;
        }
        else
        {
            Console.WriteLine("It is already open.");
            return null;
        }
    }
}

public class Leaflet : IItem
{
    public string Name => "leaflet";
    public string Description => "\"WELCOME TO ZORK!\n\nZORK is a game of adventure, danger, and low cunning. In it you will explore some of the most amazing territory ever seen by mortals. No computer should be without one!\"\n";
    public bool inInventory { get; set; } = false;
    public void PickUp()
    {
        Console.WriteLine(Description);
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
    public string Description => "You are facing the south side of a white house. There is no door here, and all the windows are boarded.";

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

public class BehindHouse : IArea
{
    public string Name => "Behind House";
    public string Description => "You are behind the white house. A path leads into the forest to the east. In one corner of the house there is a small window which is slightly ajar.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();

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
        IArea behindHouse = new BehindHouse();

        westOfHouse.Exits.Add("north", northOfHouse);
        westOfHouse.Exits.Add("west", forest);
        westOfHouse.Exits.Add("south", southOfHouse);

        northOfHouse.Exits.Add("west", westOfHouse);
        northOfHouse.Exits.Add("east", behindHouse);

        southOfHouse.Exits.Add("west", westOfHouse);
        southOfHouse.Exits.Add("east", behindHouse);

        behindHouse.Exits.Add("north", northOfHouse);
        behindHouse.Exits.Add("south", southOfHouse);

        forest.Exits.Add("west", forest);
        forest.Exits.Add("north", forest);

        return westOfHouse;
    }
}

class Program
{
    static void Main()
    {
        List<IItem> inventory = new List<IItem>();

        IZorkFactory factory = new GameFactory();
        IArea area = factory.LoadArea();

        area.DisplayInformation();

        while (true)
        {
            string input = Console.ReadLine().Trim().ToLower();
            string[] parts = input.Split(' ', 2);
            string command = parts[0];
            string target = parts.Length > 1 ? parts[1] : "";

            switch (command)
            {
                case "inventory":
                    if (inventory.Count == 0)
                    {
                        Console.WriteLine("You are empty-handed.");
                    }
                    else
                    {
                        Console.WriteLine("You are carrying:");
                        foreach (IItem item in inventory)
                        {
                            Console.WriteLine($"\t{item.Name}");
                        }
                    }
                    break;
                case "open":
                    var container = area.Containers.FirstOrDefault(c => c.Name == target);
                    if (container != null)
                    {
                        IItem item = container.Open();
                        if (item != null && !item.inInventory)
                        {
                            area.Items.Add(item);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"There is no {target} to open here.");
                    }
                    break;

                case "take":
                    var receivableItem = area.Items.FirstOrDefault(i => i.Name == target && !i.inInventory);
                    if (receivableItem != null)
                    {
                        receivableItem.inInventory = true;
                        inventory.Add(receivableItem);
                        area.Items.Remove(receivableItem);
                        Console.WriteLine("Taken.");
                    }
                    else
                    {
                        Console.WriteLine($"I do not know the word \"{target}\"");
                    }
                    break;

                case "read":
                    var readableItem = inventory.FirstOrDefault(i => i.Name == target);
                    if (readableItem != null)
                    {
                        readableItem.PickUp();
                    }
                    else
                    {
                        Console.WriteLine($"You can't see any {target} here!");
                    }
                    break;
                default:
                    if (area.Exits.TryGetValue(input, out IArea nextArea))
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
                    break;
            }
        }
    }
}