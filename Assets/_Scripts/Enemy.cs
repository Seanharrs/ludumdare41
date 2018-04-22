using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_health = 100f;

    private void Damage(float damage)
    {
        m_health -= damage;
        if(m_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Damage(collision.GetComponent<Bullet>().damage);
            collision.GetComponent<Bullet>().Disable();
        }
    }
}
