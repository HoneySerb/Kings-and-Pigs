using UnityEngine.Events;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public GroundEvent GroundEvent;


    private void OnTriggerEnter2D(Collider2D collision) => GroundEvent.Invoke(true);

    private void OnTriggerExit2D(Collider2D collision) => GroundEvent.Invoke(false);
}

[System.Serializable]
public class GroundEvent : UnityEvent<bool> { }
