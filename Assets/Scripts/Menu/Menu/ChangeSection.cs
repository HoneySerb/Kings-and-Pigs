using UnityEngine;

public class ChangeSection : MonoBehaviour
{
    [SerializeField] private GameObject _actualSection;
    [SerializeField] private GameObject _targetSection;


    public void Change()
    {
        _targetSection.SetActive(true);

        _actualSection.SetActive(false);
    }
}
