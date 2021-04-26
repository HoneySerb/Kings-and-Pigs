using UnityEngine;

public class AudioBomb : AudioBase
{
    [SerializeField] private AudioClip _clip;


    private void Start() => _audioSource.PlayOneShot(_clip);
}
