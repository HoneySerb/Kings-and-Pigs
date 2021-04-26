using UnityEngine;

public class AudioToggle : MonoBehaviour
{
    [SerializeField] private GameObject Checkmark;
    [SerializeField] private AudioType _type;

    private string _saveString;

    private bool _state;


    public void OnChangeState()
    {
        _state = !_state;

        int state = _state ? 1 : 0;

        PlayerPrefs.SetInt(_saveString, state);

        _state = PlayerPrefs.GetInt(_saveString) == 1;

        Checkmark.SetActive(_state);

        UpdateMusic();
    }

    private void Start()
    {
        _saveString = _type == AudioType.Sound ? "Sound" : "Music";

        _state = PlayerPrefs.GetInt(_saveString) == 1;

        Checkmark.SetActive(_state);
    }

    private void UpdateMusic()
    {
        if (_saveString == "Music")
        {
            GameObject.FindWithTag("Music").GetComponent<AudioSource>().enabled = _state;
        }
    }
}
