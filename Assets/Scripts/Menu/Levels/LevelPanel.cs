using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Vector2 _maxSize;
    [SerializeField] private float _speed;

    private Vector2 _startSize;

    private short _index;
    private bool _isOpen = false;


    public void LoadLevel()
    {
        if (_isOpen)
        {
            PlayerPrefs.SetInt("ActualLevel", _index);

            SceneManager.LoadScene("Play");
        }
    }

    public void ChangeSize(bool isRise)
    {
        StopAllCoroutines();

        Vector2 targetSize = isRise ? _maxSize : _startSize;

        StartCoroutine(ChangeSizeCoroutine(targetSize));
    }

    private void Awake() => _startSize = transform.localScale;

    private void Start()
    {
        _index = short.Parse(gameObject.name.Remove(0, 5));

        _text.text = $"Lvl." + _index;

        SetProgress(_index);
    }

    private void SetProgress(short index)
    {
        if (index == 1) 
        {
            _isOpen = true;

            return; 
        }

        LevelData level = SaveData.GetLevel((short)(index - 2));

        if (level.IsPart1Complete || level.IsPart2Complete || level.IsPart3Complete)
        {
            _isOpen = true;
        }
        
        if (!_isOpen)
        {
            Image[] childs = transform.GetComponentsInChildren<Image>();
            foreach (Image image in childs)
            {
                image.color = new Color(1, 1, 1, 0.75f);
            }
        }
    }

    private IEnumerator ChangeSizeCoroutine(Vector2 targetSize)
    {
        while ((Vector2)transform.localScale != targetSize)
        {
            transform.localScale = Vector2.Lerp((Vector2)transform.localScale, targetSize, Time.deltaTime * _speed);

            yield return null;
        }
    }
}
