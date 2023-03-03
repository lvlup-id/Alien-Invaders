using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ObjectPool pool;
    public float speed;
    public float lifetime;
    Rigidbody2D rb;

    public void Init()
    {
        if (rb == null) GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
    }

    public void Release()
    {
        pool.Release(gameObject);
    }
}