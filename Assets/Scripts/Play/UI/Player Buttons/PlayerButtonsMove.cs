using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerButtonsMove : PlayerButtons, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _move;

    [SerializeField] private FloatEvent _moveEvent;


    public void OnPointerEnter(PointerEventData eventData)
    {
        _moveEvent.Invoke(_move);

        SetAnimation(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _moveEvent.Invoke(0f);

        SetAnimation(false);
    }
}
