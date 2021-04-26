using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _maxSize;
    [SerializeField] private float _speedZoom;
    [SerializeField] private float _offsetX, _offsetY;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _player;

    private Camera _camera;

    private Vector2 _minLimit, _maxLimit;
    private float _startSize;


    public void OnZoom(bool isZoom)
    {
        float target = isZoom ? _maxSize : _startSize;

        StopAllCoroutines();

        StartCoroutine(ChangeSize(target));
    }

    public bool InArea(Vector2 objectPosition)
    {
        Vector2 maxVector = GetCameraMax();

        float distanceX = Vector2.Distance(new Vector2(maxVector.x, 0), new Vector2(transform.position.x, 0));
        float distanceY = Vector2.Distance(new Vector2(0, maxVector.y), new Vector2(0, transform.position.y));

        Vector2 minVector = new Vector2(transform.position.x - distanceX, transform.position.y - distanceY);
        if (minVector.x < objectPosition.x && minVector.y < objectPosition.y)
        {
            if (maxVector.x > objectPosition.x && maxVector.y > objectPosition.y)
            {
                return true;
            }
        }

        return false;
    }

    public void SetLimits(Vector2 minLimit, Vector2 maxLimit)
    {
        _minLimit.x = minLimit.x + GetCameraMax().x - _offsetX;
        _minLimit.y = minLimit.y + GetCameraMax().y - _offsetY;

        _maxLimit.x = maxLimit.x - GetCameraMax().x + _offsetX;
        _maxLimit.y = maxLimit.y - GetCameraMax().y + _offsetY;
    }

    private void Awake() => _camera = GetComponent<Camera>();

    private void Start()
    {
        Time.timeScale = 0f;

        _startSize = _camera.orthographicSize;
    }

    private void Update() => transform.position = Vector3.Lerp(transform.position, GetTargetPosition(), _speed * Time.deltaTime);

    private Vector3 GetTargetPosition()
    {
        Vector3 targetVector = _player.position;

        targetVector.x += _player.rotation.y == 0f ? _offsetX : _offsetX * -1f;

        targetVector.y += _offsetY;

        targetVector.z = -10f;

        GetPositionWithLimit(ref targetVector.x, in _minLimit.x, in _maxLimit.x);
        GetPositionWithLimit(ref targetVector.y, in _minLimit.y, in _maxLimit.y);

        return targetVector;
    }

    private void GetPositionWithLimit(ref float variable, in float minValue, in float maxValue)
    {
        if (variable > maxValue)
        {
            variable = maxValue;
        }
        else if (variable < minValue)
        {
            variable = minValue;
        }
    }

    private Vector2 GetCameraMax()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        Ray maxRay = Camera.main.ViewportPointToRay(Vector2.one);

        plane.Raycast(maxRay, out float maxHit);

        Vector2 maxVector = maxRay.GetPoint(maxHit);

        return maxVector;
    }

    private IEnumerator ChangeSize(float target)
    {
        while (_camera.orthographicSize != target)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, target, _speedZoom * Time.deltaTime);

            yield return null;
        }
    }
}
