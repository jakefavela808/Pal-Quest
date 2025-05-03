using System.Text.Json;

namespace AdventureS25;

public static class Items
{
    private static Dictionary<string, Item> nameToItem = 
        new Dictionary<string, Item>();
    
    public static void Initialize()
    {
        string path = Path.Combine(Environment.CurrentDirectory, "Items.json");
        string rawText = File.ReadAllText(path);
        
        ItemsJsonData data = JsonSerializer.Deserialize<ItemsJsonData>(rawText);

        foreach (ItemJsonData item in data.Items)
        {
            Item newItem = CreateItem(item.Name, item.Description,
                item.InitialDescription, item.IsTakeable);
            Map.AddItem(newItem.Name, item.Location);
        }
    }

    public static Item CreateItem(string name, string description,
        string initialDescription, bool isTakeable)
    {
        Item newItem = new Item(name,
            description, 
            initialDescription, isTakeable);
        nameToItem.Add(name, newItem);
        return newItem;
    }

    public static Item GetItemByName(string itemName)
    {
        if (nameToItem.ContainsKey(itemName))
        {
            return nameToItem[itemName];
        }
        return null;
    }
}