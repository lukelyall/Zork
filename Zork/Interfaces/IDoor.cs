namespace ZorkFactory;

public interface IDoor
{
    string Name { get; }
    string Description { get; }
    string Direction { get; }

    void Open();
}