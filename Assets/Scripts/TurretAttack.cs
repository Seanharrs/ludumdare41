using System.Collections;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private float m_delay;
    [SerializeField] private float m_maxShootingRange;

    private int m_count;

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
        var enemies = FindObjectsOfType<Enemy>();
        foreach(var enemy in enemies)
        {
            if(enemy == null)
            {
                continue;
            }
            if(Vector2.Distance(enemy.transform.position, transform.position) < m_maxShootingRange)
            {
                Instantiate(m_bulletPrefab, transform.position, Quaternion.identity, transform)
                    .GetComponent<Bullet>()
                    .SetDirection(enemy.transform.position);
                yield return new WaitForSeconds(m_delay);
            }
        }
    }


}
