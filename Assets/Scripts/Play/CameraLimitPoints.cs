using UnityEngine;

public class CameraLimitPoints : MonoBehaviour
{
    [SerializeField] private GameObject _minLimit, _maxLimit;


    private void Start()
    {
        GameObject.Find("Main Camera").GetComponent<CameraMovement>().SetLimits(_minLimit.transform.position, _maxLimit.transform.position);
    }
}
