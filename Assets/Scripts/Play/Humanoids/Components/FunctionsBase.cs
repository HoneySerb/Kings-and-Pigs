using System.Collections;
using UnityEngine;

public abstract class FunctionsBase : MonoBehaviour
{
    protected float Timer = 0f;


    protected bool HasParameter(Animator animator, string name)
    {
        if (animator == null) { return false; }

        AnimatorControllerParameter[] parameters = animator.parameters;
        foreach(AnimatorControllerParameter parameter in parameters)
        {
            if (parameter.name == name)
            {
                return true;
            }
        }

        return false;
    }

    protected void ThrowObject(GameObject obj, Vector2? force, float dir = 0f)
    {
        Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();

        if (force != null) { objRb.AddForce((Vector2)force, ForceMode2D.Impulse); }

        objRb.AddTorque(objRb.velocity.magnitude * dir);
    }

    protected void ChangeObject(GameObject obj)
    {
        Instantiate(obj, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    protected void LookAt(float target, float compared = float.NaN, bool reverse = false, ComparisonSigns comparisonSign = ComparisonSigns.MoreAndEquals)
    {
        (Vector2 answer1, Vector2 answer2) = reverse ? (Vector2.zero, Vector2.up * 180f) : (Vector2.up * 180f, Vector2.zero);

        if (float.IsNaN(compared)) { compared = transform.position.x; }

        switch (comparisonSign)
        {
            case ComparisonSigns.Equals: transform.eulerAngles = compared == target ? answer1 : answer2; break;
            case ComparisonSigns.More: transform.eulerAngles = compared > target ? answer1 : answer2; break;
            default: transform.eulerAngles = compared >= target ? answer1 : answer2; break;
        }
    }

    protected float GetDirection(float compared1 = float.NaN, float compared2 = float.NaN, bool reverse = false, ComparisonSigns comparisonSign = ComparisonSigns.Equals)
    {
        float mod = reverse ? -1f : 1f;

        if (float.IsNaN(compared1)) { compared1 = transform.eulerAngles.y; }
        if (float.IsNaN(compared2)) { compared2 = 0; }

        switch (comparisonSign)
        {
            case ComparisonSigns.More: return compared1 > compared2 ? 1 * mod : -1 * mod;
            case ComparisonSigns.MoreAndEquals: return compared1 >= compared2 ? 1 * mod : -1 * mod;
            default: return compared1 == compared2 ? 1 * mod : -1 * mod;
        }
    }

    protected IEnumerator CameraShake()
    {
        yield return new WaitForSeconds(0.05f);

        GameObject camera = GameObject.Find("Main Camera");

        if (camera.GetComponent<CameraMovement>().InArea(gameObject.transform.position))
        {
            Transform cameraTransform = camera.GetComponent<Transform>();

            cameraTransform.position = new Vector2(cameraTransform.position.x + (GetDirection() * 0.1f), cameraTransform.position.y - 0.1f);
        }
    }
}

public enum ComparisonSigns
{
    More,
    MoreAndEquals,
    Equals
}