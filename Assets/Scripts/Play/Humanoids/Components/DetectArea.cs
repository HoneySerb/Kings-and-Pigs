using UnityEngine.Events;
using UnityEngine;

public class DetectArea : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;

    [SerializeField] private GameObjectEvent _detectEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_mask.value & (1 << collision.gameObject.layer)) != 0) { _detectEvent.Invoke(collision.gameObject); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_mask.value & (1 << collision.gameObject.layer)) != 0) { _detectEvent.Invoke(null); }
    }
}
