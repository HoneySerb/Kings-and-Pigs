using System.Collections;
using UnityEngine;

public class Humanoid : HumanoidDamageable, IDamageable
{
    [SerializeField] private float _speed;

    [SerializeField] private byte _damage;
    [SerializeField] private float _stun;

    [SerializeField] private float _attackDelay;
    [SerializeField] private float _attackTime;

    [SerializeField] private Vector2 _pushForce;

    protected float Move;

    protected AttackArea _attackArea;
    protected Rigidbody2D _rigidbody;

    
    public void Attack()
    {
        if (Timer < _attackTime) { return; }

        _audioEvent.Invoke(HumanoidAudioType.Attack);

        Timer = 0f;

        _animator.SetTrigger("Attack");

        StartCoroutine(CameraShake());

        StartCoroutine(DealDamage(_attackArea.GetObjectsInArea(), _attackDelay));
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _attackArea = GetComponentInChildren<AttackArea>();
        SetAnimator(GetComponent<Animator>());
    }

    private void FixedUpdate()
    {
        if (HasParameter(_animator, "Run")) { _animator.SetBool("Run", Move != 0); }

        if (HasParameter(_animator, "VelocityY")) { _animator.SetFloat("VelocityY", _rigidbody.velocity.y); }

        if (IsStunned) { return; }

        _rigidbody.velocity = new Vector2(Move * _speed, _rigidbody.velocity.y);

        Flip();
    }

    private void Flip()
    {
        if (Move == 0) { return; }

        LookAt(0f, Move, true, ComparisonSigns.More);
    }

    private IEnumerator DealDamage(GameObject[] objc, float _attackDelay)
    {
        yield return new WaitForSeconds(_attackDelay);

        foreach(GameObject obj in objc)
        {
            Vector2 force = _pushForce;
            force.x *= GetDirection(transform.position.x, obj.transform.position.x, true, ComparisonSigns.MoreAndEquals);

            obj.GetComponent<IDamageable>().Hit(_damage, force, _stun);
        }
    }
}