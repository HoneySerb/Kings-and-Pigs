using UnityEngine;

public abstract class PlayerButtons : MonoBehaviour
{
    private Animator _animator;


    protected void SetAnimation(bool isHold) => _animator.SetBool("IsHold", isHold);

    private void Awake() => _animator = GetComponent<Animator>();
}