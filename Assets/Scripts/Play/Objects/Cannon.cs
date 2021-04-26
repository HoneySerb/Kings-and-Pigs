using UnityEngine;

public class Cannon : FunctionsBase
{
    [SerializeField] private GameObject _cannonBall;
    [SerializeField] private float _shootForce;

    private Animator _animator;

    private Vector2 _shootPosition;


    public void Shoot()
    {
        _animator.SetTrigger("Shoot");

        Invoke(nameof(SpawnCannonBall), 0.05f);
    }

    private void Awake() => _animator = GetComponent<Animator>();

    private void Start()
    {
        _shootPosition = transform.position;

        _shootPosition.y += 0.15f;

        _shootForce *= GetDirection();
    }

    private void SpawnCannonBall()
    {
        GameObject cannonBall = Instantiate(_cannonBall, _shootPosition, Quaternion.identity);

        ThrowObject(cannonBall, new Vector2(_shootForce, 1.5f));

        cannonBall.GetComponent<IDamageable>().Hit(2, null, float.NaN);
    }
}
