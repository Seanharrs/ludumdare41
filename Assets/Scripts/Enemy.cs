using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    public static List<Enemy> enemiesAlive;
    [SerializeField] private float m_health = 100f;

    private void Start()
    {
        if(enemiesAlive == null)
        {
            enemiesAlive = new List<Enemy>();
        }
        enemiesAlive.Add(this);
    }

    private void Damage(float damage)
    {
        m_health -= damage;
        if(m_health <= 0)
        {
            enemiesAlive.Remove(this);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Damage(collision.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject);
        }
    }
}
