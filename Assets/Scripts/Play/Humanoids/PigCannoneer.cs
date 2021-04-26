using System.Collections;
using UnityEngine;

public class PigCannoneer : HumanoidDamageable
{
    [SerializeField] private GameObject _pig;
    [SerializeField] private GameObject _cannon;
    [SerializeField] private float _timeShoot;


    public void OnDetect(GameObject obj)
    {
        if (obj == null) { return; }

        StopAllCoroutines();

        ChangeObject(_pig);
    }

    private void Awake()
    {
        Animator _animator = GetComponent<Animator>();
        SetAnimator(_animator);
    }

    private void Update()
    {
        if (!IsAlive) { return; }

        Timer += Time.deltaTime;

        if (Timer >= _timeShoot)
        {
            StartCoroutine(Shoot());

            Timer = 0f;
        }
    }

    private IEnumerator Shoot()
    {
        _animator.SetTrigger("LightCannon");

        yield return new WaitForSeconds(0.1f);

        _cannon.GetComponent<Cannon>().Shoot();

        StartCoroutine(CameraShake());

        _animator.SetTrigger("LightMatch");
    }
}
