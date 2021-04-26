using UnityEngine;

public class Pig : Humanoid
{
    [SerializeField] private Vector2 _patrolArea;

    private GameObject _player;


    public void OnDetect(GameObject obj) => _player = obj;

    private void Update()
    {
        if (!IsAlive || IsStunned)
        {
            Move = 0f;
            
            return;
        }

        Timer += Time.deltaTime;

        Patrol(_player);

        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector2 vector1 = new Vector2(_patrolArea.x, transform.position.y);
        Vector2 vector2 = new Vector2(_patrolArea.y, transform.position.y);

        Gizmos.DrawLine(vector1, vector2);
    }

    new private void Attack()
    {
        if (_attackArea.GetObjectsInArea().Length != 0) { base.Attack(); }
    }

    private void Patrol(GameObject player)
    {
        if (player == null)
        {
            if (_patrolArea.x == _patrolArea.y)
            {
                Move = 0f;

                return;
            }

            if (Vector2.Distance(transform.position, new Vector2(_patrolArea.x, transform.position.y)) <= 0.5f)
            {
                _patrolArea.x += _patrolArea.y;
                _patrolArea.y = _patrolArea.x - _patrolArea.y;
                _patrolArea.x -= _patrolArea.y;
            }

            MoveTo(_patrolArea.x);
        }
        else
        {
            MoveTo(player.transform.position.x);
        }
    }

    private void MoveTo(float targetX)
    {
        float move = 1f;

        LookAt(targetX);

        move *= GetDirection();

        if (Vector2.Distance(transform.position, new Vector2(targetX, transform.position.y)) <= 0.5f) { move = 0f; }

        Move = CheckGround() ? move : 0;
    }

    private bool CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(new Vector2(1, -0.5f)), 1f, layerMask: LayerMask.GetMask("Border"));

        return hit.collider != null;
    }
}
