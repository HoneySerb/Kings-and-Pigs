using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _task1, _task2, _task3;
    [SerializeField] private Text _awesomeTry, _bestTime, _death;

    [SerializeField] private Image _skull;

    [SerializeField] private Color _startColor;
    [SerializeField] private Color _finalColor;

    private LevelData _level;
    private byte _actualIndex = 255;


    public void OnLevelUpdate(byte index)
    {
        if (_actualIndex != index)
        {
            _actualIndex = index;

            _level = SaveData.GetLevel(index);

            SetName();
            SetTasks();
            SetSkull();
        }
    }

    private void Start() => StartCoroutine(DelayedLevelUpdate());

    private void SetTasks()
    {
        _task1.text = "1 - " + _level.Part1;
        _task2.text = "2 - " + _level.Part2;
        _task3.text = "3 - " + _level.Part3;

        SetColor(_task1, _level.IsPart1Complete);
        SetColor(_task2, _level.IsPart2Complete);
        SetColor(_task3, _level.IsPart3Complete);
    }

    private void SetSkull()
    {
        float completedParts = 0f;

        completedParts += _level.IsPart1Complete ? 0.33f : 0f;
        completedParts += _level.IsPart2Complete ? 0.33f : 0f;
        completedParts += _level.IsPart3Complete ? 0.33f : 0f;

        if (completedParts == 0.99f) { completedParts = 1f; }

        _skull.fillAmount = completedParts;
    }

    private void SetName() => _name.text = _level.Name;

    private void SetColor(Text text, bool value) => text.color = value ? _finalColor : _startColor;

    private IEnumerator DelayedLevelUpdate()
    {
        yield return new WaitForEndOfFrame();

        OnLevelUpdate(0);
    }
}
