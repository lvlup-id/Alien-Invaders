using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 3f;
    public float lifetime = 2f;
    Rigidbody2D rb;

    public void Init()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        StopAllCoroutines();
        Release(lifetime);
        rb.velocity = Vector2.up * speed;
    }

    public void Release(float delay = 0f)
    {
        if (delay <= 0)
            gameObject.SetActive(false);
        else
            StartCoroutine(ReleaseCoroutine(delay));
    }

    IEnumerator ReleaseCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
