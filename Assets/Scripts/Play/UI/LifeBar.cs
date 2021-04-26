using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField] GameObject[] _hearts;


    public void ChangeLifeBar(byte health)
    {
        ChangeState(false);

        ChangeState(true, health);
    }

    private void ChangeState(bool isEnable, byte quantity = 3)
    {
        for (byte i = 0; i < quantity; i++)
        {
            _hearts[i].SetActive(isEnable);
        }
    }
}
