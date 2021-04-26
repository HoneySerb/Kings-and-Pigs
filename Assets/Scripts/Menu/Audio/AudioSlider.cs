using UnityEngine.UI;
using UnityEngine;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioType _type;

    private Slider _slider;

    private string _saveString;


    public void OnChangeVolume() => PlayerPrefs.SetFloat(_saveString, _slider.value);

    private void Awake() => _slider = GetComponent<Slider>();

    private void Start()
    {
        _saveString = _type == AudioType.Music ? "MusicVolume" : "SoundVolume";

        _slider.value = PlayerPrefs.GetFloat(_saveString);
    }
}
