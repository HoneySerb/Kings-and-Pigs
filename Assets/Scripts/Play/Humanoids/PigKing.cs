using UnityEngine;

public class PigKing : Humanoid
{
    private GameObject _player;


    public void OnDetect(GameObject obj) => _player = obj;

    public override void Hit(byte damage, Vector2? pushForce, float timeStun)
    {
        base.Hit(damage, pushForce, timeStun);
        
        if (!IsAlive) { GameObject.Find("DoorFinish").GetComponent<Door>().ChangeState(); }
    }

    private void Update()
    {
        if (!IsAlive || _player == null) { return; }

        Timer += Time.deltaTime;

        LookAt(_player.transform.position.x);

        Attack();
    }

    new private void Attack()
    {
        if (_attackArea.GetObjectsInArea().Length != 0) { base.Attack(); }
    }
}
