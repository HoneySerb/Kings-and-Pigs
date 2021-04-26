using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private BoolEvent _groundEvent;


    private void OnTriggerEnter2D(Collider2D collision) => _groundEvent.Invoke(true);

    private void OnTriggerExit2D(Collider2D collision) => _groundEvent.Invoke(false);
}
