namespace ZorkFactory;

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
