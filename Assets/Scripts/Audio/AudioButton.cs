using UnityEngine;

public class AudioButton : AudioState
{
    [SerializeField] private AudioClip _clip;


    public void PlayAudio() => _audioSource.PlayOneShot(_clip);
}
