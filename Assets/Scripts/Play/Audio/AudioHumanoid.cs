using UnityEngine;

public class AudioHumanoid : AudioBase
{
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _attack;
    [SerializeField] private AudioClip _jump;
    [SerializeField] private AudioClip _throw;


    public void PlayAudio(HumanoidAudioType type)
    {
        AudioClip clip = null;
        switch (type)
        {
            case HumanoidAudioType.Hit: clip = _hit; break;
            case HumanoidAudioType.Attack: clip = _attack; break;
            case HumanoidAudioType.Jump: clip = _jump; break;
            case HumanoidAudioType.Throw: clip = _throw; break;
        }

        _audioSource.PlayOneShot(clip);
    }
}

public enum HumanoidAudioType
{
    Hit,
    Attack,
    Jump,
    Throw
}
