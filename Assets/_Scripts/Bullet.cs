using EZObjectPools;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PooledObject))]
public class Bullet : MonoBehaviour
{
    private float m_Damage;
    public float damage { get { return m_Damage; } }

    [SerializeField] private float m_velocity;

    public void SetDirection(Vector2 target)
    {
        GetComponent<Rigidbody2D>().velocity = (target - (Vector2)transform.position).normalized * m_velocity;
        StartCoroutine(DeactiveDelay());
    }

    public void Shoot()
    {
        GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.right);
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

    public void SetDamage(int dmg)
    {
        m_Damage = dmg;
    }
}
