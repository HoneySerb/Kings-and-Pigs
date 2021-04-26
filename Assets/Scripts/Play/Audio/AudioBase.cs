using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioBase : MonoBehaviour
{
    protected AudioSource _audioSource;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            _audioSource.volume = PlayerPrefs.GetFloat("SoundVolume");
        }
    }
}
