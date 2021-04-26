using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float _timeClose;
    [SerializeField] private float _timePlayerEnter;
    [SerializeField] private bool _isStart;

    private Animator _animator;
    private IEnumerator _stateCoroutine;

    private bool _isOpen = false;
    

    public bool GetState()
    {
        return _isOpen;
    }

    public void ChangeState()
    {
        if (_stateCoroutine != null) { return; }

        _stateCoroutine = ChangeStateCoroutine();

        StartCoroutine(_stateCoroutine);
    }

    private void Awake() => _animator = GetComponent<Animator>();

    private void Start()
    {
        if (!_isStart) { return; }

        Time.timeScale = 1f;

        ChangeState();

        Invoke(nameof(PlayerEnter), _timePlayerEnter);

        Invoke(nameof(ChangeState), _timeClose);
    }

    private void PlayerEnter()
    {
        Vector2 spawnPosition = transform.position;

        spawnPosition.y -= 0.45f;

        GameObject player = GameObject.Find("Player");

        player.GetComponent<Player>().DoorOut(spawnPosition);
    }

    private IEnumerator ChangeStateCoroutine()
    {
        _animator.SetBool("Open", !_isOpen);

        yield return new WaitForSeconds(0.15f);

        _isOpen = !_isOpen;

        _stateCoroutine = null;
    }
}