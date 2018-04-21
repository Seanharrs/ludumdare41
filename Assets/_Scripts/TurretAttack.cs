using EZObjectPools;
using System.Collections;
using UnityEngine;

public class TurretAttack : MonoBehaviour, ITower
{
    //[SerializeField] private GameObject m_bulletPrefab;
    //[SerializeField] private float m_delay;
    //[SerializeField] private float m_maxShootingRange;

    private static EZObjectPool m_pool;

    private int m_count;

    [SerializeField]
    private int m_Damage;
    public int damage { get { return m_Damage; } }

    [SerializeField]
    private int m_Range;
    public int range { get { return m_Range; } }

    [SerializeField]
    private float m_ShootSpeed;
    public float shootSpeed { get { return m_ShootSpeed; } }
    
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
                if(Vector2.Distance(enemy.transform.position, transform.position) < m_Range)
                {
                    GameObject bulletObj;
                    m_pool.TryGetNextObject(transform.position, Quaternion.identity, out bulletObj);
                    Bullet bullet = bulletObj.GetComponent<Bullet>();
                    bullet.SetDirection(enemy.transform.position);
                    bullet.SetDamage(m_Damage);
                    yield return new WaitForSeconds(m_ShootSpeed);
                }
            }
        }
    }
}
