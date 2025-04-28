namespace ZorkFactory;

public interface IContainer
{
    string Name { get; }
    List<IItem> Open();
}
