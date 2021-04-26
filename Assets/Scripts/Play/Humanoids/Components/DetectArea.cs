using UnityEngine.Events;
using UnityEngine;

public class DetectArea : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;

    public DetectEvent detectEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_mask.value & (1 << collision.gameObject.layer)) != 0) { detectEvent.Invoke(collision.gameObject); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_mask.value & (1 << collision.gameObject.layer)) != 0) { detectEvent.Invoke(null); }
    }
}

[System.Serializable]
public class DetectEvent : UnityEvent<GameObject> { }