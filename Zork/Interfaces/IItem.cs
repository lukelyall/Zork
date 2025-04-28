namespace ZorkFactory;

public interface IItem
{
    string Name { get; }
    string Description { get; }
    string Examine { get; }
    bool inInventory { get; set; }

    void Text();
}
