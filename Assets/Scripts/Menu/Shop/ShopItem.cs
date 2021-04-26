using UnityEngine.UI;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Items _type;
    [SerializeField] private uint _price;
    [SerializeField] private Text _quantity;

    [SerializeField] private ShopEvent _selectEvent;

    private Item _item;


    public void UpdateInfo()
    {
        _item = SaveData.GetItem(_type);

        _quantity.text = _item.Quantity.ToString();
    }

    public void OnClick() => _selectEvent.Invoke(_item.Type, _price, _name);

    private void Start() => UpdateInfo();
}