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
    List<IDoor> Doors => new List<IDoor>(); 

    public void DisplayInformation(List<IItem> inventory);
}

public interface IContainer
{
    string Name { get; }
    List<IItem> Open();
}

public interface IItem
{
    string Name { get; }
    string Description { get; }
    string Examine { get; }
    bool inInventory { get; set; }

    void Text();
}

public interface IDoor
{ 
    string Name { get; }
    string Description { get; }
    string Direction { get; }

    void Open();
}

public class WestOfHouse : IArea
{
    public string Name => "West of House";
    public string Description => "You are standing in an open field west of a white house, with a boarded front door.\n" +
                                 "There is a small mailbox here.";
    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "east", "The door is boarded and you can't remove the boards." },
        { "up", "You can't go that way." },
        { "down", "You can't go that way." }
    };

    private readonly List<IContainer> _containers = new();
    private readonly List<IItem> _items = new();

    public List<IContainer> Containers => _containers;
    public List<IItem> Items => _items;

    public WestOfHouse()
    {
        _containers.Add(new Mailbox());
    }

    public void DisplayInformation(List<IItem> inventory)
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

    public List<IItem> Open()
    {
        if (!isOpened)
        {
            Console.WriteLine("Opening the small mailbox reveals a leaflet.");
            isOpened = true;
            return new List<IItem> { item };
        }
        else
        {
            Console.WriteLine("It is already open.");
            return new List<IItem>();
        }
    }
}

public class Leaflet : IItem
{
    public string Name => "leaflet";
    public string Description => "\"WELCOME TO ZORK!\n\nZORK is a game of adventure, danger, and low cunning. " +
        "In it you will explore some of the most amazing territory ever seen by mortals. No computer should be without one!\"\n";
    public string Examine => "\"WELCOME TO ZORK!\n\nZORK is a game of adventure, danger, and low cunning. " +
        "In it you will explore some of the most amazing territory ever seen by mortals. No computer should be without one!\"\n";
    public bool inInventory { get; set; } = false;
    public void Text()
    {
        Console.WriteLine(Description);
    }
}

public class Forest : IArea
{
    public string Name => "Forest";
    public string Description => "This is a forest. Trees in all directions. To the east, there appears to be sunlight.";
    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "up", "There is no tree here suitable for climbing." },
        { "down", "You can't go that way." }
    };


    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class NorthOfHouse : IArea
{
    public string Name => "North of House";
    public string Description => "You are facing the north side of a white house. There is no door here, and all the windows are boarded up. " +
        "To the north a narrow path winds through the trees.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "south", "The windows are all boarded." },
        { "up", "You can't go that way." }, 
        { "down", "You can't go that way." }
    };

    public void DisplayInformation(List<IItem> inventory)
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
        { "north", "The windows are all boarded." },
        { "up", "You can't go that way." }, 
        { "down", "You can't go that way." }
    };

    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class BehindHouse : IArea
{
    public string Name => "Behind House";
    public string Description => "You are behind the white house. A path leads into the forest to the east. " +
        "In one corner of the house there is a small window which is slightly ajar.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    private readonly List<IDoor> _doors = new();
    public List<IDoor> Doors => _doors;

    public Dictionary<string, string> BlockedExits
    {
        get
        {
            var blockedExits = new Dictionary<string, string>
            {
                { "north", "The forest becomes impenetrable to the north." },
                { "up", "You can't go that way." },
                { "down", "You can't go that way." }

            };

            var window = _doors.OfType<Window>().FirstOrDefault();
            if (window != null && !window.IsOpened)
            {
                blockedExits.Add("west", "The kitchen window is closed.");
            }

            return blockedExits;
        }
    }

    public BehindHouse()
    {
        _doors.Add(new Window());
    }

    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class Window : IDoor
{
    public string Name => "window";
    public string Description => "The kitchen window is closed.";
    public string Direction => "west";
    private bool isOpened = false;
    public bool IsOpened => isOpened;

    private IArea kitchen = new Kitchen();

    public void Open()
    {
        if (!isOpened)
        {
            Console.WriteLine("With great effort, you open the window far enough to allow entry.");
            isOpened = true;
        }
        else
        {
            Console.WriteLine("Too late for that");
        }
    }
}

public class ForestNorthEast : IArea
{
    public string Name => "Forest";
    public string Description => "You hear the chirping of birds.";
    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();

    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class ForestPath : IArea
{
    public string Name => "Forest Path";
    public string Description => "This is a path winding through a dimly lit forest. " +
        "The path heads north-south here. One particularly large tree with some low branches stands at the edge of the path.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "down", "You can't go that way." }
    };


    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class UpATree : IArea
{
    public string Name => "Up a Tree";
    public string Description => "You are about 10 feet above the ground nestled among some large branches. " +
        "The nearest branch above you is above your reach. Beside you on the branch is a small bird's nest. " +
        "In the bird's nest is a large egg encrusted with precious jewels, apparently scavenged by a childless songbird." +
        "The egg is covered with fine gold inlay, and ornamented in lapis lazuli and mother-of-pearl. " +
        "Unlike most eggs, this one is hinged and closed with a delicate looking clasp. The egg appears extremely fragile.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "up", "You cannot climb any higher." },
        { "west", "You can't go that way."},
        { "south", "You can't go that way."},
        { "east", "You can't go that way."},
        { "north", "You can't go that way.\nYou hear in the distance the chirping of a song bird."},
    };
    private readonly List<IItem> _items = new();
    public List<IItem> Items => _items;

    public UpATree()
    {
        _items.Add(new Egg());
    }

    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class Egg : IItem
{
    public string Name => "jewel-encrusted egg";
    public string Description => "How does one read a jewel-encrusted egg?";
    public string Examine => "The jewel-encrusted egg is closed.";
    public bool inInventory { get; set; } = false;
    public void Text()
    {
        Console.WriteLine(Description);
    }
}

public class Clearing : IArea
{
    public string Name => "Clearing";
    public string Description => "You are in a clearing, with a forest surrounding you on all sides." +
        " A path leads south.\nOn the ground is a pile of leaves.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "north", "The forest becomes impenetrable to the north." },
        { "up", "You can't go that way." },
        { "down", "You can't go that way." }
    };

    public void DisplayInformation(List<IItem> inventory)
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
        { "up", "There is no tree here suitable for climbing." }, 
        { "down", "You can't go that way." }
    };

    public void DisplayInformation(List<IItem> inventory)
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
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "up", "You can't go that way." },
        { "down", "You can't go that way." }
    };


    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class CanyonView : IArea
{
    public string Name => "Canyon View";
    public string Description => "You are at the top of the Great Canyon on its west wall. " +
        "From here there is a marvelous view of the canyon and parts of the Frigid River upstream. Across the canyon, " +
        "the walls of the White Cliffs join the mighty ramparts of the Flathead Moutains to the east. Following the canyon upstream to the north, " +
        "Aragain Falls may be seen, complete with rainbow. The mighty Frigid River flows out from a great dark cavern. " +
        "To the west and south can be seen an immense forest, stretching for miles around. A path leads northwest." +
        " It is possible to climb down into the canyon from here.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "north", "You can't go that way." },
        { "south", "Storm-tossed trees block the way." },
        { "up", "You can't go that way." }
    };

    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class RockyLedge : IArea
{
    public string Name => "Rocky Ledge";
    public string Description => "You are on a ledge about halfway up the wall of the river canyon. " +
        "You can see from here that the main flow from Aragain Falls twists along a passage which is impossible for you to enter. " +
        "Below you is the canyon bottom. Above you is more cliff, which appears climbable.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "west", "You can't go that way." },
        { "east", "You can't go that way." }
    };

    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class CanyonBottom : IArea
{
    public string Name => "Canyon Bottom";
    public string Description => "You are beneath the walls of the river canyon which may be climbable here. " +
        "The lesser part of the runoff of Aragain Falls flows by below. To the north is a narrow passage.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "east", "You can't go that way." },
        { "west", "You can't go that way." },
        { "south", "You can't go that way." }
    };

    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class EndOfRainbow : IArea
{
    public string Name => "End of Rainbow";
    public string Description => "You are on a small, rocky beach on the continuation of the Frigid River past the Falls. " +
        "The beach is narrow due to the presence of the White Cliffs. The river canyon opens here and sunlight shines in from above. " +
        "A rainbow crosses over the falls to the east and a narrow passage continues to the southwest.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "north", "You can't go that way." },
        { "south", "You can't go that way." },
        { "east", "You can't go that way." }
    };

    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class Kitchen : IArea
{
    public string Name => "Kitchen";
    public string Description => "You are in the kitchen of the white house. " +
        "A table seems to have been used recently for the preparation of food. " +
        "A passage leads to the west and a dark stairway can be seen leading upwards." +
        " A dark chimney leads down and to the east is a small window which is open.";

    private readonly List<IContainer> _containers = new();
    private readonly List<IItem> _items = new();

    public List<IContainer> Containers => _containers;
    public List<IItem> Items => _items;

    public Kitchen()
    {
        _containers.Add(new BrownSack());
    }


    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "south", "You can't go that way." },
        { "down", "Only Santa Claus climbs down chimneys." }
    };

    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class BrownSack : IContainer
{
    public string Name => "brown sack";
    private bool isOpened = false;
    private List<IItem> items = new List<IItem> { new Lunch(), new Garlic() };

    public List<IItem> Open()
    {
        if (!isOpened)
        {
            Console.WriteLine("Opening the brown sack reveals a lunch, and a clove of garlic.");
            isOpened = true;
            return items;
        }
        else
        {
            Console.WriteLine("It is already open.");
            return new List<IItem>();
        }
    }
}

public class Lunch : IItem
{
    public string Name => "lunch";
    public string Description => "You can't read a lunch.";
    public string Examine => "There's nothing special about the lunch.";
    public bool inInventory { get; set; } = false;
    public void Text()
    {
        Console.WriteLine(Description);
    }
}

public class Garlic : IItem
{
    public string Name => "clove of garlic";
    public string Description => "You can't read a clove of garlic.";
    public string Examine => "There's nothing special about the clove of garlic.";
    public bool inInventory { get; set; } = false;
    public void Text()
    {
        Console.WriteLine(Description);
    }
}

public class LivingRoom : IArea
{
    public string Name => "Living Room";
    public string Description => "You are in the living room. " +
        "There is a doorway to the east, a wooden door with strange gothic lettering to the west, " +
        "which appears to be nailed shut, a trophy case, and a large oriental rug in the center of the room. " +
        "Above the trophy case hangs an elvish sword of great antiquity. A battery-powered brass lantern is on the trophy case.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "north", "You can't go that way." },
        { "up", "You can't go that way." },
        { "down", "You can't go that way." }
    };

    private readonly List<IContainer> _containers = new();
    private readonly List<IItem> _items = new();

    public List<IContainer> Containers => _containers;
    public List<IItem> Items => _items;

    public LivingRoom()
    {
        _items.Add(new Sword());
        _items.Add(new Lantern());
    }

    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);
        Console.WriteLine(Description);
    }
}

public class Sword : IItem
{
    public string Name => "sword";
    public string Description => "How does one read a sword?";
    public string Examine => "There's nothing special about the sword.";
    public bool inInventory { get; set; } = false;
    public void Text()
    {
        Console.WriteLine(Description);
    }
}

// implement turning on and off lantern
public class Lantern : IItem
{
    public string Name => "brass lantern";
    public string Description => "How does one read a brass lantern?";
    public string Examine => (isLanternOn) ? "The lantern is turned off." : "The lantern is turned on.";
    public bool inInventory { get; set; } = false;

    // turn on lantern - 1 health, moving to a new area with lantern on - 1 health
    public int lanternHealth { get; set; } = 450;
    public bool isLanternOn { get; set; } = false;

    public void Text()
    {
        Console.WriteLine(Description);
    }
}

public class Attic : IArea
{
    public string Name => "Attic";

    public string Description => "You are in a dark attic.";

    public Dictionary<string, IArea> Exits { get; } = new Dictionary<string, IArea>();
    public Dictionary<string, string> BlockedExits => new Dictionary<string, string>
    {
        { "north", "You can't go that way." }
    };

    public void DisplayInformation(List<IItem> inventory)
    {
        Console.WriteLine(Name);

        bool hasLantern = inventory.Any(i => i.Name.Contains("lantern", StringComparison.OrdinalIgnoreCase));
        if (hasLantern)
        {
            Console.WriteLine("This is the attic. The only exit is a stairway leading down. " +
                "A large coil of rope is lying in the corner. On a table is a nasty-looking knife.");
        }
        else
        {
            Console.WriteLine("You have moved into a dark place. It is pitch black. You are likely to be eaten by a grue.");
        }
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
        IArea attic = new Attic();
        IArea upATree = new UpATree();

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
        forestPath.Exits.Add("up", upATree);

        upATree.Exits.Add("down", forestPath);

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
        canyonView.Exits.Add("down", rockyLedge);

        rockyLedge.Exits.Add("up", canyonView);
        rockyLedge.Exits.Add("down", canyonBottom);

        canyonBottom.Exits.Add("up", rockyLedge);
        canyonBottom.Exits.Add("north", endOfRainbow);

        endOfRainbow.Exits.Add("south", canyonBottom);

        kitchen.Exits.Add("east", behindHouse);
        kitchen.Exits.Add("west", livingRoom);
        kitchen.Exits.Add("up", attic);

        livingRoom.Exits.Add("east", kitchen);

        attic.Exits.Add("down", kitchen);

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

        area.DisplayInformation(inventory);

        while (true)
        {
            string input = (Console.ReadLine() ?? string.Empty).Trim().ToLower();
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
                    var container = area.Containers.FirstOrDefault(c => c.Name.Contains(target));
                    var door = area.Doors.FirstOrDefault(c => c.Name.Contains(target));
                    if (container != null)
                    {
                        List<IItem> items = container.Open();
                        foreach (IItem item in items)
                        {
                            if (item != null && !item.inInventory)
                            {
                                area.Items.Add(item);
                            }
                        }
                    }
                    else if (door != null)
                    {
                        door.Open();
                    }
                    else
                    {
                        Console.WriteLine($"There is no {target} to open here.");
                    }
                    break;
                case "take":
                    var receivableItem = area.Items.FirstOrDefault(i => i.Name.Contains(target) && !i.inInventory);
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
                    var readableItem = inventory.FirstOrDefault(i => i.Name.Contains(target));
                    if (readableItem != null)
                    {
                        readableItem.Text();
                    }
                    else
                    {
                        Console.WriteLine($"You can't see any {target} here!");
                    }
                    break;
                case "":
                    Console.WriteLine("I beg your pardon?");
                    break;
                default:
                    if (area.BlockedExits.ContainsKey(input))
                    {
                        Console.WriteLine(area.BlockedExits[input]);
                    }
                    else if (area.Exits.TryGetValue(input, out IArea? nextArea) && nextArea != null)
                    {
                        area = nextArea;
                        area.DisplayInformation(inventory);
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