using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;

    public List<GameObject> HitList;


    public GameObject[] GetObjectsInArea()
    {
        HitList.RemoveAll(x => x == null);

        return HitList.ToArray();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_mask.value & (1 << collision.gameObject.layer)) != 0)
        {
            HitList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HitList.Remove(collision.gameObject);
    }
}
