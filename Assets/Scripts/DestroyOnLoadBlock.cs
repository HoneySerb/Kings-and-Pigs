using UnityEngine;

public class DestroyOnLoadBlock : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (GameObject.FindGameObjectsWithTag(gameObject.tag).Length > 1) { Destroy(GameObject.FindGameObjectsWithTag(gameObject.tag)[1]); }
    }
}
