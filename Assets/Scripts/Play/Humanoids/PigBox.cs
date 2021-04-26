using System.Collections;
using UnityEngine;

public class PigBox : Box
{
    [SerializeField] private GameObject _pig;

    [SerializeField] private float _timeJump;
    [SerializeField] private Vector2 _minJumpForce;
    [SerializeField] private Vector2 _maxJumpForce;

    [SerializeField] private byte _damage;
    [SerializeField] private float _stun;
    [SerializeField] private Vector2 _pushForce;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private GameObject _player;
    private IEnumerator _jumpCoroutine;

    private bool _onGround = true;


    public void OnDetect(GameObject obj) => _player = obj;

    protected override void Crash()
    {
        base.Crash();

        ChangeObject(_pig);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_onGround)
        {
            Timer += Time.deltaTime;
            if (_player != null && _jumpCoroutine == null && Timer >= _timeJump)
            {
                _jumpCoroutine = Jump(_player.transform.position.x);

                StartCoroutine(_jumpCoroutine);
            }

            return;
        }

        Timer = 0f;
    }

    private void FixedUpdate()
    {
        if (!_onGround && _rigidbody.velocity.y < 0) { _animator.SetTrigger("Fall"); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _pushForce.x *= GetDirection(transform.position.x, collision.transform.position.x, true, ComparisonSigns.MoreAndEquals);

            collision.gameObject.GetComponent<IDamageable>().Hit(_damage, _pushForce, _stun);

            Crash();
        }

        _animator.ResetTrigger("Fall");

        _animator.SetTrigger("Ground");
        
        _onGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision) => _onGround = false;

    private IEnumerator Jump(float targetX)
    {
        LookAt(targetX, reverse: true, comparisonSign: ComparisonSigns.MoreAndEquals);

        _animator.SetTrigger("Jump");

        yield return new WaitForSeconds(0.5f);

        Vector2 force = GetForce(_minJumpForce, _maxJumpForce);

        force.x *= GetDirection(transform.position.x, targetX, true, ComparisonSigns.MoreAndEquals);

        _rigidbody.AddForce(force, ForceMode2D.Impulse);

        Timer = 0f;

        _jumpCoroutine = null;
    }
}