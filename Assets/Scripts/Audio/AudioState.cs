using UnityEngine;

public class AudioState : MonoBehaviour
{
    [SerializeField] private AudioType _type;

    protected AudioSource _audioSource;

    private string _saveString => _type == AudioType.Sound ? "Sound" : "Music";


    public void UpdateVolume()
    {
        string save = string.Concat(_saveString, "Volume");

        _audioSource.volume = PlayerPrefs.GetFloat(save);
    }

    public void UpdateAudio() => GetAudio();

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void Start()
    {
        CheckAudio();

        UpdateVolume();

        GetAudio();
    }

    private void GetAudio() => _audioSource.enabled = PlayerPrefs.GetInt(_saveString) == 1;

    private void CheckAudio()
    {
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
            PlayerPrefs.SetInt("Music", 1);
        }
    }
}

public enum AudioType
{
    Sound,
    Music
}