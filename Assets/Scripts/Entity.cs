using UnityEngine;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{
    public GameObject explosionPrefab;
    public List<string> triggerTag;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerTag.Contains(other.tag))
        {
            OnDie();
            Destroy(gameObject);
            if (other.TryGetComponent<Projectile>(out Projectile p))
                p.Release();
        }
    }

    protected virtual void OnDie()
    {
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.SetParent(transform.parent.parent);
        explosion.transform.position = transform.position;
    }
}
