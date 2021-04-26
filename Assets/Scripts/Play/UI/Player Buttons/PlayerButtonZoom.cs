using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerButtonZoom : PlayerButtons, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private BoolEvent _zoomEvent;


    public void OnPointerEnter(PointerEventData eventData)
    {
        _zoomEvent.Invoke(true);

        SetAnimation(true);

        Time.timeScale = 0.25f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _zoomEvent.Invoke(false);

        SetAnimation(false);

        Time.timeScale = 1f;
    }
}