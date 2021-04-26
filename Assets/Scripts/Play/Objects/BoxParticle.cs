using System.Collections;
using UnityEngine;

public class BoxParticle : MonoBehaviour
{
    private void Start() => StartCoroutine(Fade());

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(1f);

        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();

        while (_spriteRenderer.color.a > 0.01f)
        {
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, new Color(1f, 1f, 1f, 0f), Time.deltaTime * 2f);

            yield return null;
        }

        Destroy(gameObject);
    }
}
