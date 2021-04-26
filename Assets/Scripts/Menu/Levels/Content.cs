using UnityEngine.UI;
using UnityEngine;

public class Content : MonoBehaviour
{
    [SerializeField] private GameObject _level;

    [SerializeField] private ByteEvent _selectEvent;

    private byte _selectedLevel;

    private RectTransform _rectTransform;
    private HorizontalLayoutGroup _layoutGroup;
    
    private GameObject[] _levels;
    private LevelPositionRange[] _levelsRange;

    private float _startPosition;


    public void OnLevelsCreation(byte quantity)
    {
        _levels = new GameObject[quantity];

        _levelsRange = new LevelPositionRange[quantity];

        _startPosition = _rectTransform.localPosition.x;

        SpawnLevels();
    }

    public float GetMovePosition()
    {
        float centerPosition;
        if (_selectedLevel == 0)
        {
            centerPosition = _levelsRange[0].Min;
        }
        else if (_selectedLevel == _levels.Length)
        {
            centerPosition = _levelsRange[_selectedLevel].Max;
        }
        else
        {
            centerPosition = (_levelsRange[_selectedLevel].Max + _levelsRange[_selectedLevel].Min) / 2f;
        }

        return _startPosition - centerPosition;
    }

    public void SelectLevel()
    {
        float actualPos = _rectTransform.localPosition.x;

        if (actualPos > _startPosition)
        {
            _selectedLevel = 0;
            SelectedLevelSize(0);

            _selectEvent.Invoke(_selectedLevel);
            return;
        }

        float distance = Mathf.Abs(actualPos - _startPosition);

        for (byte i = 0; i < _levelsRange.Length; i++)
        {
            if (_levelsRange[i].InRange(distance))
            {
                _selectedLevel = i;
                SelectedLevelSize(i);

                _selectEvent.Invoke(_selectedLevel);
                break;
            }
        }
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _layoutGroup = GetComponent<HorizontalLayoutGroup>();
    }

    private void SelectedLevelSize(byte index)
    {
        _levels[index].GetComponent<LevelPanel>().ChangeSize(true);

        if (index - 1 >= 0)
        {
            _levels[index - 1].GetComponent<LevelPanel>().ChangeSize(false);
        }

        if (index + 1 < _levels.Length)
        {
            _levels[index + 1].GetComponent<LevelPanel>().ChangeSize(false);
        }
    }

    private void SpawnLevels()
    {
        for (byte i = 0; i < _levels.Length; i++)
        {
            _levels[i] = Instantiate(_level, transform, false);

            _levels[i].name = $"Level{i + 1}";

            if (i == 0)
            {
                _levels[i].GetComponent<LevelPanel>().ChangeSize(true);

                _levelsRange[i].SetRange(0f, _layoutGroup.spacing / 2f);
            }
            else
            {
                float previousRangeMax = _levelsRange[i - 1].Max;

                _levelsRange[i].SetRange(previousRangeMax, previousRangeMax + _layoutGroup.spacing);
            }
        }
    }
}

internal struct LevelPositionRange
{
    internal float Max { get; private set; }
    internal float Min { get; private set; }


    internal void SetRange(float min, float max)
    {
        Min = min;
        Max = max;
    }

    internal bool InRange(float position)
    {
        return Min < position && position <= Max;
    }
}