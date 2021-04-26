using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveData
{
    public static void SetLevel(LevelData level, short index) => SetObject(level, $"/Level{index}.dat");

    public static LevelData GetLevel(short index)
    {
        string path = Application.persistentDataPath + $"/Level{index}.dat";

        if (!File.Exists(path)) { return null; }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream fileStream = new FileStream(path, FileMode.Open);

        LevelData level = formatter.Deserialize(fileStream) as LevelData;

        fileStream.Close();

        return level;
    }

    public static void UpdateLevel(LevelData level, short index)
    {
        string path = Application.persistentDataPath + $"/Level{index}.dat";

        File.Delete(path);

        SetLevel(level, index);
    }

    public static void SetItem(Item item) => SetObject(item, GetItemDocumentName(item.Type));

    public static Item GetItem(Items type)
    {
        string path = Application.persistentDataPath + GetItemDocumentName(type);

        if (!File.Exists(path))
        {
            Item newItem = new Item { Type = type };

            SetItem(newItem);

            return GetItem(newItem.Type);
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream fileStream = new FileStream(path, FileMode.Open);

        Item item = formatter.Deserialize(fileStream) as Item;

        fileStream.Close();

        return item;
    }

    public static void UpdateItem(Item item)
    {
        string path = Application.persistentDataPath + GetItemDocumentName(item.Type);

        File.Delete(path);

        SetItem(item);
    }

    private static string GetItemDocumentName(Items type)
    {
        string documentName = string.Empty;
        switch (type)
        {
            case Items.HealPotion: documentName = "/HealPotion.dat"; break;
            case Items.DiamondPotion: documentName = "/DiamondPotion.dat"; break;
            case Items.Diamond: documentName = "/Diamond.dat"; break;
        }

        return documentName;
    }

    private static void SetObject<T>(T classObject, string documentName)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + documentName;

        FileStream fileStream = new FileStream(path, FileMode.Create);

        formatter.Serialize(fileStream, classObject);

        fileStream.Close();
    }
}