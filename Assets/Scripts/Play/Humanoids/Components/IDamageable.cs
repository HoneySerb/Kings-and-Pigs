using UnityEngine;

public interface IDamageable
{
    void Hit(byte damage, Vector2? pushForce, float stun);
}