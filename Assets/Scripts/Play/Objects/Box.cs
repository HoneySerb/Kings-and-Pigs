using UnityEngine;

public abstract class Box : FunctionsBase, IDamageable
{
    [SerializeField] private GameObject _particle;
    [SerializeField] private Sprite[] _particleSprites;

    [SerializeField] private Vector2 _minParticleForce;
    [SerializeField] private Vector2 _maxParticleForce;

    [SerializeField] private Sprite _hitSprite;


    public void Hit(byte _damage, Vector2? pushForce, float stun)
    {
        if (pushForce != null) { ThrowObject(gameObject, (Vector2)pushForce); }

        SetHitSprite();

        Invoke(nameof(Crash), 0.1f);
    }

    protected virtual void Crash() => SpawnBoxParticles();

    protected void SpawnObj(GameObject obj, Vector2 force, bool hasTorque = false)
    {
        if (obj == null) { return; }

        GameObject spawnedObj = Instantiate(obj, transform.position, Quaternion.identity);

        Rigidbody2D spawnedObjRb = spawnedObj.GetComponent<Rigidbody2D>();

        spawnedObjRb.AddForce(force, ForceMode2D.Impulse);

        if (hasTorque) { spawnedObjRb.AddTorque(spawnedObjRb.velocity.magnitude * GetDirection(force.x, 0, comparisonSign: ComparisonSigns.MoreAndEquals)); }
    }

    protected Vector2 GetForce(Vector2 minForce, Vector2 maxForce)
    {
        Vector2 force;

        force.x = Random.Range(minForce.x, maxForce.x);
        force.y = Random.Range(minForce.y, maxForce.y);

        return force;
    }

    private void SetHitSprite() => GetComponent<SpriteRenderer>().sprite = _hitSprite;

    private void SpawnBoxParticles()
    {
        for (byte i = 0; i < 4; i++)
        {
            _particle.GetComponent<SpriteRenderer>().sprite = _particleSprites[i];

            SpawnObj(_particle, GetForce(_minParticleForce, _maxParticleForce), true);
        }
    }
}