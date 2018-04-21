using EZObjectPools;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PooledObject))]
public class Bullet : MonoBehaviour
{
    public float damage;
    [SerializeField] private float m_velocity;

    public void SetDirection(Vector2 target)
    {
        GetComponent<Rigidbody2D>().velocity = (target - (Vector2)transform.position).normalized * m_velocity;
        StartCoroutine(DeactiveDelay());
    }

    private IEnumerator DeactiveDelay()
    {
        yield return new WaitForSeconds(5f);
        Disable();
    }

    public void Disable()
    {
        GetComponent<PooledObject>().Disable();
    }
}
