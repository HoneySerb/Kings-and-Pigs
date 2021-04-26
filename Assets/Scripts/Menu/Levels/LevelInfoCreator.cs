using UnityEngine;

public class LevelInfoCreator : MonoBehaviour
{
    [Header("Level Name & quantity")]
    [SerializeField] private string[] _names;

    [SerializeField] private ByteEvent _levelsCreationEvent;


    private void Start()
    {
        _levelsCreationEvent.Invoke((byte)_names.Length);
        for (byte i = 0; i < _names.Length; i++)
        {
            if (SaveData.GetLevel(i) == null)
            {
                CreateInfo(i);
            }
        }
    }

    private void CreateInfo(byte index)
    {
        LevelData level = new LevelData { Name = _names[index] };

        SaveData.SetLevel(level, index);
    }
}
