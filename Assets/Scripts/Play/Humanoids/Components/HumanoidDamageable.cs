using System.Collections;
using UnityEngine;

public abstract class HumanoidDamageable : FunctionsBase, IDamageable
{
    [SerializeField] protected HumanoidAudioEvent _audioEvent;
    [SerializeField] private byte _health;
    protected byte Health => _health;

    protected bool IsAlive => _health != 0;
    protected bool IsStunned { get; private set; } = false;

    protected Animator _animator;

    private float _timeStun;


    public virtual void Hit(byte damage, Vector2? pushForce, float timeStun)
    {
        _audioEvent.Invoke(HumanoidAudioType.Hit);

        StartStun(timeStun);

        ThrowObject(gameObject, (Vector2)pushForce);

        if (_health > damage)
        {
            _health -= damage;

            if (HasParameter(_animator, "Hit"))
            {
                _animator.SetTrigger("Hit");
            }
        }
        else
        {
            _health = 0;

            StartCoroutine(Die());
        }
    }

    protected void OnHeal(byte health)
    {
        if (_health == 0) { return; }

        _health = _health + health >= 3 ? (byte)3 : (byte)(_health + health);
    }

    protected void SetAnimator(Animator animator) => this._animator = animator;

    private void StopStun() => IsStunned = false;

    private void StartStun(float timeStun)
    {
        _timeStun = !float.IsNaN(timeStun) ? timeStun : 0f;

        IsStunned = true;

        Invoke(nameof(StopStun), _timeStun);
    }

    private IEnumerator Die()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore");

        _animator.SetTrigger("Die");

        yield return new WaitForSeconds(1f);

        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();

        while (_spriteRenderer.color.a > 0.01f)
        {
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, new Color(1f, 1f, 1f, 0f), Time.deltaTime);

            yield return null;
        }

        Destroy(gameObject);
    }
}
