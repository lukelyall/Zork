namespace ZorkFactory;
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
