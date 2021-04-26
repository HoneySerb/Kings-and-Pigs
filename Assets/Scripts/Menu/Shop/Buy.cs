using UnityEngine.UI;
using UnityEngine;

public class Buy : MonoBehaviour
{
    [SerializeField] private ShopItem[] _goods;
    [SerializeField] private Text _price;

    private (Item Good, uint Price) _good;


    public void OnChangeItem(Items type, uint price, string itemName)
    {
        _good = (SaveData.GetItem(type), price);

        _price.text = $"{itemName}: {price}$";
    }

    public void BuyItem()
    {
        Item diamond = SaveData.GetItem(Items.Diamond);

        if (diamond.Quantity >= _good.Price)
        {
            diamond.Quantity -= _good.Price;

            SaveData.UpdateItem(diamond);

            _good.Good.Quantity++;

            SaveData.UpdateItem(_good.Good);

            foreach (ShopItem Good in _goods)
            {
                Good.UpdateInfo();
            }
        }
    }
}