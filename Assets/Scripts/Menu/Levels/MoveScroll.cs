using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;

public class MoveScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Content _content;
    [SerializeField] private float _speed;

    private IEnumerator _moveCoroutine;


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);

            _moveCoroutine = null;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_moveCoroutine == null)
        {
            float position = _content.GetMovePosition();

            if (position == _content.GetComponent<RectTransform>().localPosition.x) { return; }

            _moveCoroutine = MoveTo();

            StartCoroutine(_moveCoroutine);
        }
    }

    private IEnumerator MoveTo()
    {
        RectTransform contentTransform = _content.GetComponent<RectTransform>();
        while (contentTransform.localPosition.x != _content.GetMovePosition())
        {
            contentTransform.localPosition = Vector2.Lerp(contentTransform.localPosition, new Vector2(_content.GetMovePosition(), contentTransform.localPosition.y), Time.deltaTime * _speed);

            yield return null;
        }

        _moveCoroutine = null;
    }
}
