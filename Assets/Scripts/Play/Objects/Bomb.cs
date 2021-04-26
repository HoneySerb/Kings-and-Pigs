using UnityEngine;

public class Bomb : FunctionsBase, IDamageable
{
    [SerializeField] private GameObject _explosion;
    
    [SerializeField] private float _explosionTime;

    [SerializeField] private byte _damage;
    [SerializeField] private float _stun;
    [SerializeField] private Vector2 _pushForce;

    private Animator _animator;

    private bool _readyToExplode = false;
    private bool _isFired = false;


    public void Hit(byte damage, Vector2? pushForce, float timeStun)
    {
        ThrowObject(gameObject, pushForce);

        if (damage == 2) { _readyToExplode = true; }

        if (HasParameter(_animator, "Fire")) { _animator.SetBool("Fire", _isFired = true); }
    }

    private void Awake()
    {
        if (GetComponent<Animator>() != null) { _animator = GetComponent<Animator>(); }
    }

    private void Update()
    {
        if (_isFired) { Timer += Time.deltaTime; }

        if (Timer >= _explosionTime) { _readyToExplode = true; }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_readyToExplode)
        {
            _readyToExplode = false;

            Explosion(collision);
        }
    }

    private void Explosion(Collision2D collision)
    {       
        ContactPoint2D contact = collision.GetContact(0);

        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);

        Vector2 pos = contact.point;

        GameObject explosion = Instantiate(_explosion, pos, rot);

        explosion.GetComponent<Explosion>().SetParameters(_damage, _pushForce, _stun);

        Destroy(gameObject);
    }
}