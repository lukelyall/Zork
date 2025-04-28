namespace ZorkFactory;

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
