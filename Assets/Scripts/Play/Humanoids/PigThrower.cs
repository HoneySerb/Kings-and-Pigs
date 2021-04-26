using System.Collections;
using UnityEngine;

public class PigThrower : HumanoidDamageable
{
    [SerializeField] private GameObject _pig;
    [SerializeField] private GameObject _randomBox;

    [SerializeField] private Vector2 _throwForce;

    [SerializeField] private byte _damage;
    [SerializeField] private float _stun;
    [SerializeField] private Vector2 _pushForce;


    public void OnDetect(GameObject obj)
    {
        if (obj == null || !IsAlive) { return; }

        LookAt(obj.transform.position.x);

        _throwForce.x *= GetDirection();
        
        StartCoroutine(Throw());
    }

    private void Awake()
    {
        Animator _animator = GetComponent<Animator>();
        SetAnimator(_animator);
    }

    private IEnumerator Throw()
    {
        _animator.SetTrigger("Throw");

        yield return new WaitForSeconds(0.1f);

        _audioEvent.Invoke(HumanoidAudioType.Throw);

        yield return new WaitForSeconds(0.1f);

        GameObject box = Instantiate(_randomBox, transform.position, Quaternion.identity);

        box.GetComponent<RandomBox>().SetParameters(_damage, _pushForce, _stun);

        ThrowObject(box, _throwForce);

        ChangeObject(_pig);
    }
}
