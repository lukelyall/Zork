namespace ZorkFactory;

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
