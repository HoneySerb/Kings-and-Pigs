using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().OnCollect(tag);

            Destroy(gameObject);
        }
    }
}
