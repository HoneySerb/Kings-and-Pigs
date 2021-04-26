using UnityEngine.UI;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _levels;

    [SerializeField] private Text _name;
    [SerializeField] private Text[] _tasks;

    private LevelData _actualLevel;
    private short _levelIndex;
    private byte _difficulty;


    public void OnComplete(byte isWin)
    {
        if (isWin != 1) { return; }

        switch(_difficulty)
        {
            case 0: _actualLevel.IsPart1Complete = true; break;
            case 1: _actualLevel.IsPart2Complete = true; break;
            case 2: _actualLevel.IsPart3Complete = true; break;
        }

        SaveData.UpdateLevel(_actualLevel, _levelIndex);
    }

    public void SpawnLevel(byte difficulty)
    {
        _difficulty = difficulty;

        PlayerPrefs.SetInt("LevelsQuantity", _levels.Length);

        _levelIndex = (short)(PlayerPrefs.GetInt("ActualLevel") - 1);

        Instantiate(_levels[(_levelIndex * 3) + difficulty], transform.parent);

        GetLevelInfo();
    }

    private void GetLevelInfo()
    {
        _actualLevel = SaveData.GetLevel(_levelIndex);

        _name.text = _actualLevel.Name;
    }
}