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

    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "north", "The forest becomes impenetrable to the north." }
    };

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class ForestNorthEast : IArea
{
    public string Name => "Forest";
    public string Description => "You hear the chirping of birds.";
    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class ForestPath : IArea
{
    public string Name => "Forest Path";
    public string Description => "This is a path winding through a dimly lit forest. The path heads north-south here. One particularly large tree with some low branches stands at the edge of the path.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class Clearing : IArea
{
    public string Name => "Clearing";
    public string Description => "You are in a clearing, with a forest surrounding you on all sides. A path leads south.\nOn the ground is a pile of leaves.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "north", "The forest becomes impenetrable to the north." }
    };

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class ForestSouth : IArea
{
    public string Name => "Forest";
    public string Description => "This is a dimly lit forest, with large trees all around.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "east", "The rank undergrowth prevents eastward movement." },
        { "south", "Storm-tossed trees block your way." }
    };

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class ClearingEast : IArea
{
    public string Name => "Clearing";
    public string Description => "You are in a small clearing in a well marked forest path that extends to the east and west.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class CanyonView : IArea
{
    public string Name => "Canyon View";
    public string Description => "You are at the top of the Great Canyon on its west wall. From here there is a marvelous view of the canyon and parts of the Frigid River upstream. Across the canyon, the walls of the White Cliffs join the mighty ramparts of the Flathead Moutains to the east. Following the canyon upstream to the north, Aragain Falls may be seen, complete with rainbow. The mighty Frigid River flows out from a great dark cavern. To the west and south can be seen an immense forest, stretching for miles around. A path leads northwest. It is possible to climb down into the canyon from here.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "north", "You can't go that way." },
        { "south", "Storm-tossed trees block the way." }
    };

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class RockyLedge : IArea
{
    public string Name => "Rocky Ledge";
    public string Description => "You are on a ledge about halfway up the wall of the river canyon. You can see from here that the main flow from Aragain Falls twists along a passage which is impossible for you to enter. Below you is the canyon bottom. Above you is more cliff, which appears climbable.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "west", "You can't go that way." },
        { "east", "You can't go that way." }
    };

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class CanyonBottom : IArea
{
    public string Name => "Canyon Bottom";
    public string Description => "You are beneath the walls of the river canyon which may be climbable here. The lesser part of the runoff of Aragain Falls flows by below. To the east is a narrow passage.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "west", "You can't go that way." },
        { "south", "You can't go that way." }
    };

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class EndOfRainbow : IArea
{
    public string Name => "End of Rainbow";
    public string Description => "You are on a small, rocky beach on the continuation of the Frigid River past the Falls. The beach is narrow due to the presence of the White Cliffs. The river canyon opens here and sunlight shines in from above. A rainbow crosses over the falls to the east and a narrow passage continues to the southwest.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "north", "You can't go that way." },
        { "south", "You can't go that way." },
        { "east", "You can't go that way." }
    };

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class Kitchen : IArea
{
    public string Name => "Kitchen";
    public string Description => "You are in the kitchen of the white house. A table seems to have been used recently for the preparation of food. A passage leads to the west and a dark stairway can be seen leading upwards. A dark chimney leads down and to the east is a small window which is open.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "south", "You can't go that way." },
    };

    public void DisplayInformation()
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class LivingRoom : IArea
{
    public string Name => "Living Room";
    public string Description => "You are in the living room. There is a doorway to the east, a wooden door with strange gothic lettering to the west, which appears to be nailed shut, a trophy case, and a large oriental rug in the center of the room. Above the trophy case hangs an elvish sword of great antiquity. A battery-powered brass lantern is on the trophy case.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "north", "You can't go that way." },
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
        IArea behindHouse = new BehindHouse();
        IArea forestPath = new ForestPath();
        IArea forestNorthEast = new ForestNorthEast();
        IArea clearing = new Clearing();
        IArea forestSouth = new ForestSouth();
        IArea clearingEast = new ClearingEast();
        IArea canyonView = new CanyonView();
        IArea rockyLedge = new RockyLedge();
        IArea canyonBottom = new CanyonBottom();
        IArea endOfRainbow = new EndOfRainbow();
        IArea kitchen = new Kitchen();
        IArea livingRoom = new LivingRoom();

        westOfHouse.Exits.Add("north", northOfHouse);
        westOfHouse.Exits.Add("west", forest);
        westOfHouse.Exits.Add("south", southOfHouse);

        northOfHouse.Exits.Add("west", westOfHouse);
        northOfHouse.Exits.Add("east", behindHouse);
        northOfHouse.Exits.Add("north", forestPath);

        southOfHouse.Exits.Add("west", westOfHouse);
        southOfHouse.Exits.Add("east", behindHouse);
        southOfHouse.Exits.Add("south", forestSouth);

        behindHouse.Exits.Add("north", northOfHouse);
        behindHouse.Exits.Add("south", southOfHouse);
        behindHouse.Exits.Add("east", clearingEast);
        behindHouse.Exits.Add("west", kitchen);

        forest.Exits.Add("west", forest);
        forest.Exits.Add("east", forestPath);
        forest.Exits.Add("north", clearing);

        forestPath.Exits.Add("south", northOfHouse);
        forestPath.Exits.Add("west", forest);
        forestPath.Exits.Add("east", forestNorthEast);
        forestPath.Exits.Add("north", clearing);

        forestNorthEast.Exits.Add("west", forestPath);
        forestNorthEast.Exits.Add("east", forestNorthEast);
        forestNorthEast.Exits.Add("south", clearingEast);

        clearing.Exits.Add("south", forestPath);
        clearing.Exits.Add("east", forestNorthEast);
        clearing.Exits.Add("west", forest);

        forestSouth.Exits.Add("west", forest);
        forestSouth.Exits.Add("north", clearingEast);

        clearingEast.Exits.Add("north", forestNorthEast);
        clearingEast.Exits.Add("south", forestSouth);
        clearingEast.Exits.Add("west", behindHouse);
        clearingEast.Exits.Add("east", canyonView);

        canyonView.Exits.Add("west", forestSouth);
        canyonView.Exits.Add("east", rockyLedge);

        rockyLedge.Exits.Add("north", canyonView);
        rockyLedge.Exits.Add("south", canyonBottom);

        canyonBottom.Exits.Add("north", rockyLedge);
        canyonBottom.Exits.Add("east", endOfRainbow);

        endOfRainbow.Exits.Add("west", canyonBottom);

        kitchen.Exits.Add("east", behindHouse);
        kitchen.Exits.Add("west", livingRoom);

        livingRoom.Exits.Add("east", kitchen);

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