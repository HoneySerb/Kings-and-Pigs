using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Items _type;

    [SerializeField] private ByteEvent _potionEvent;

    private Item _potion;


    public void UsePotion()
    {
        if (_potion.Quantity > 0)
        {
            _potion.Quantity--;

            SaveData.UpdateItem(_potion);

            _potionEvent.Invoke((byte)_type);

            SetText(_potion.Quantity);
        }
    }

    private void Start()
    {
        _potion = SaveData.GetItem(_type);

        SetText(_potion.Quantity);
    }

    private void SetText(uint value) => _text.text = value.ToString();
}
