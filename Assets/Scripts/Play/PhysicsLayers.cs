using UnityEngine;

public class PhysicsLayers : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < 32; i++)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ignore"), i, true);
        }

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ignore"), LayerMask.NameToLayer("Border"), false);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Collectable"), true);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Damageable"), LayerMask.NameToLayer("Collectable"), true);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Collectable"), LayerMask.NameToLayer("Collectable"), true);
    }
}
