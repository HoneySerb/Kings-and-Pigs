using System.Collections;
using UnityEngine;

public class PigBomber : HumanoidDamageable
{
    [SerializeField] private GameObject _pig;

    [SerializeField] private GameObject _bomb;
    [SerializeField] private float _timeThrow;
    [SerializeField] private float _timePickUp;
    [SerializeField] private Vector2 _throwForce;

    private Coroutine _bombCoroutine;

    private bool _hasBomb = true;
    private float dir;


    public void OnDetect(GameObject obj)
    {
        if (obj == null || !IsAlive) { return; }

        StopAllCoroutines();

        ChangeObject(_pig);
    }

    private void Awake()
    {
        Animator _animator = GetComponent<Animator>();
        SetAnimator(_animator);
    }

    private void Start()
    {
        _throwForce.x *= GetDirection();

        dir = GetDirection(reverse: true);
    }

    private void Update()
    {
        if (!IsAlive) { return; }

        Timer += Time.deltaTime;

        (string action, float timeCoroutine, float timeAction) = _hasBomb ? ("Throw", 0.25f, _timeThrow) : ("PickUp", 0.2f, _timePickUp);

        if (Timer >= timeAction && _bombCoroutine == null)
        {
            _bombCoroutine = StartCoroutine(BombAction(action, timeCoroutine));

            Timer = 0f;
        }
    }

    private void ThrowBomb()
    {
        GameObject bomb = Instantiate(_bomb, transform.position, Quaternion.identity);

        _audioEvent.Invoke(HumanoidAudioType.Throw);

        ThrowObject(bomb, _throwForce, dir);

        bomb.GetComponent<IDamageable>().Hit(1, null, float.NaN);
    }

    private IEnumerator BombAction(string action, float time)
    {
        _animator.SetTrigger(action);

        yield return new WaitForSeconds(time);

        if (_hasBomb) { ThrowBomb(); }

        _animator.SetBool("HasBomb", _hasBomb = !_hasBomb);

        _bombCoroutine = null;
    }
}
