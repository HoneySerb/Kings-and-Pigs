[System.Serializable]
public class Item
{
    public Items Type;

    public uint Quantity;
}

public enum Items
{
    HealPotion,
    DiamondPotion,
    Diamond
}