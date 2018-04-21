using EZObjectPools;
using System.Collections;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private float m_delay;
    [SerializeField] private float m_maxShootingRange;

    private static EZObjectPool m_pool;

    private int m_count;

    private void Start()
    {
        if(m_pool == null)
        {
            m_pool = FindObjectOfType<EZObjectPool>();
        }
    }

    private void FixedUpdate()
    {
        if(++m_count == 20)
        {
            StartCoroutine(ShootEnemies());
            m_count = 0;
        }
    }

    private IEnumerator ShootEnemies()
    {
        if(m_pool != null)
        {
            var enemies = FindObjectsOfType<Enemy>();
            foreach(var enemy in enemies)
            {
                if(enemy == null)
                {
                    continue;
                }
                if(Vector2.Distance(enemy.transform.position, transform.position) < m_maxShootingRange)
                {
                    GameObject bullet;
                    m_pool.TryGetNextObject(transform.position, Quaternion.identity, out bullet);
                    bullet.GetComponent<Bullet>()
                        .SetDirection(enemy.transform.position);
                    yield return new WaitForSeconds(m_delay);
                }
            }
        }
    }


}
