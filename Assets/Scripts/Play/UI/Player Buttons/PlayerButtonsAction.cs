using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class PlayerButtonsAction : PlayerButtons
{
    [SerializeField] private UnityEvent _pressEvent;

    public void Action()
    {
        StopAllCoroutines();

        SetAnimation(true);

        _pressEvent.Invoke();

        StartCoroutine(DelayedRelease(0.1f));
    }

    private IEnumerator DelayedRelease(float time)
    {
        yield return new WaitForSeconds(time);

        SetAnimation(false);
    }
}
