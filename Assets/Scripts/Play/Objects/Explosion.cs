using UnityEngine;

public class Explosion : FunctionsBase
{
    private byte _damage;
    private float _stun;
    private Vector2 _pushForce;


    public void SetParameters(byte damage, Vector2 pushForce, float stun)
    {
        _damage = damage;
        _pushForce = pushForce;
        _stun = stun;
    }

    private void Start()
    {
        StartCoroutine(CameraShake());

        Destroy(gameObject, 0.25f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 force = _pushForce;
        force.x *= GetDirection(transform.position.x, collision.transform.position.x, true, ComparisonSigns.MoreAndEquals);

        collision.gameObject.GetComponent<IDamageable>()?.Hit(_damage, force, _stun);
    }
}
