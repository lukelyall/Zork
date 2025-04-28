using System;

namespace ZorkFactory;

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