using System.Collections;
using UnityEngine;

public class Player : Humanoid
{
    [SerializeField] private float _jumpForce;

    [SerializeField] private ByteEvent _healthEvent;
    [SerializeField] private ByteEvent _finishEvent;
    [SerializeField] private StringEvent _diamondEvent;

    private (bool OnGround, byte JumpQuantity) _position = (true, 2);
    private byte _diamond = 0;

    private bool _doorStun = false;

    private bool _isDoubleDiamonds = false;

    private IEnumerator _doorCoroutine;


    public void OnGroundChange(bool isEntered)
    {
        _position = isEntered ? (true, (byte)2) : (false, _position.JumpQuantity);

        _animator.SetBool("OnGround", _position.OnGround);
    }

    public void OnPotion(byte type)
    {
        switch((Items)type)
        {
            case Items.HealPotion: 
                OnHeal(1);
                _healthEvent.Invoke(Health);
                break;
            case Items.DiamondPotion:
                _isDoubleDiamonds = true;
                break;
        }
    }

    public void OnCollect(string type, byte quantity = 1)
    {
        switch(type)
        {
            case "Heart":
                OnHeal(quantity);
                _healthEvent.Invoke(Health);
                break;
            case "Diamond":
                _diamond += _isDoubleDiamonds? (byte)(quantity * 2) : quantity;
                _diamondEvent.Invoke(_diamond.ToString());
                break;
        }
    }

    public void DoorOut(Vector3 position)
    {
        transform.position = position;

        _animator.SetTrigger("DoorOut");

        _doorStun = false;
    }

    new public void Move(float move) => base.Move = move;

    public void Jump()
    {
        if (_position.JumpQuantity > 0)
        {
            _audioEvent.Invoke(HumanoidAudioType.Jump);

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);

            _rigidbody.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);

            _position.JumpQuantity--;
        }
    }

    public override void Hit(byte damage, Vector2? pushForce, float timeStun)
    {
        if (Health <= damage)
        {
            _healthEvent.Invoke(0);

            StartCoroutine(SendFinishEvent(2, 1f));
        }

        base.Hit(damage, pushForce, timeStun);

        _healthEvent.Invoke(Health);
    }

    private void Start() => _doorStun = true;

    private void Update()
    {
        if (!IsAlive || IsStunned || _doorStun) { return; }

        Timer += Time.deltaTime;

        //base.Move = Input.GetAxis("Horizontal");

        //if (Input.GetKeyDown(KeyCode.R)) { Attack(); }

        //if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name != "DoorFinish") { return; }

        if (!collision.GetComponent<Door>().GetState()) { return; }

        if (_doorCoroutine == null)
        {
            float targetX = collision.gameObject.transform.position.x;
            _doorCoroutine = DoorIn(new Vector2(targetX, 0f));

            StartCoroutine(_doorCoroutine);
        }
    }

    private IEnumerator DoorIn(Vector2 doorPosition)
    {
        _doorStun = true;

        base.Move = GetDirection(transform.position.x, doorPosition.x, true, ComparisonSigns.MoreAndEquals);
        while (true)
        {
            if (Vector2.Distance(new Vector2(transform.position.x, 0f), doorPosition) < 0.1f)
            {
                base.Move = 0f;

                _animator.SetTrigger("DoorIn");

                PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") + _diamond);

                StartCoroutine(SendFinishEvent(1, 0.5f));

                break;
            }

            yield return null;
        }
    }

    private IEnumerator SendFinishEvent(byte type, float time)
    {
        yield return new WaitForSeconds(time);

        UpdateDiamonds();

        _finishEvent.Invoke(type);
    }

    private void UpdateDiamonds()
    {
        Item diamond = SaveData.GetItem(Items.Diamond);

        diamond.Quantity += _diamond;

        SaveData.SetItem(diamond);
    }
}