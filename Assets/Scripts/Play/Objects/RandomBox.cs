using UnityEngine;

public class RandomBox : Box
{
    [SerializeField] private GameObject[] _content;
    
    [SerializeField] private bool _isThrowable;

    private byte _damage;
    private float _stun;
    private Vector2 _pushForce;


    public void SetParameters(byte damage, Vector2 pushForce, float stun)
    {
        _damage = damage;
        _pushForce = pushForce;
        _stun = stun;
    }

    protected override void Crash()
    {
        base.Crash();

        if (_isThrowable) { StartCoroutine(CameraShake()); }

        ChangeObject(_content[Random.Range(0, _content.Length)]);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isThrowable)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Border"))
            {
                _pushForce.x *= GetDirection(transform.position.x, collision.transform.position.x, true, ComparisonSigns.MoreAndEquals);

                collision.gameObject.GetComponent<IDamageable>()?.Hit(_damage, _pushForce, _stun);

                Crash();
            }
        }
    }
}