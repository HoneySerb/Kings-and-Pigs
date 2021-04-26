using UnityEngine.Events;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    [SerializeField] private byte _difficulty;

    [SerializeField] private ByteEvent _difficultyByteEvent;
    [SerializeField] private UnityEvent _difficultyEvent;


    public void SetDifficulty()
    {
        _difficultyEvent.Invoke();
        _difficultyByteEvent.Invoke(_difficulty);
        
        _difficultyByteEvent.RemoveAllListeners();
        _difficultyEvent.RemoveAllListeners();
    }
}
