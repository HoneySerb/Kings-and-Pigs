using UnityEngine;

public class ItemBox : Box
{
    [SerializeField] private GameObject _heart;
    [SerializeField] private byte _heartQuantity;

    [SerializeField] private GameObject _diamond;
    [SerializeField] private byte _diamondQuantity;

    [SerializeField] private Vector2 _minForce;
    [SerializeField] private Vector2 _maxForce;

    protected override void Crash()
    {
        base.Crash();

        SpawnObjects(_heart, _heartQuantity);
        SpawnObjects(_diamond, _diamondQuantity);

        Destroy(gameObject);
    }

    private void SpawnObjects(GameObject obj, byte quantity)
    {
        for (byte i = 0; i < quantity; i++) { SpawnObj(obj, GetForce(_minForce, _maxForce)); }
    }
}