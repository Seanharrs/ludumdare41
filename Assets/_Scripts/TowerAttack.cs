using EZObjectPools;
using System.Collections;
using UnityEngine;

public class TowerAttack : MonoBehaviour, ITower
{
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

    private int m_UpgradeLevel = 1;
    public int upgradeLevel { get { return m_UpgradeLevel; } }

    [SerializeField]
    private int m_MaxUpgradeLevel = 3;

    [SerializeField]
    public Transform weapon;

    public bool canUpgrade { get { return m_UpgradeLevel != m_MaxUpgradeLevel; } }

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

    public void Highlight()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.g = 255;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void RemoveHighlight()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.g = 155;
        GetComponent<SpriteRenderer>().color = color;
    }

    /// <summary>
    /// Called when an upgrade tower card is used on a tower
    /// </summary>
    /// <param name="dmgMult">The damage increase multiplier</param>
    /// <param name="spdMult">The speed increase multiplier</param>
    /// <param name="rngMult">The range increase multiplier</param>
    /// <returns><c>true</c> if upgrade successful, <c>false</c> if tower is fully upgraded</returns>
    public bool TryUpgradeTower(float dmgMult, float spdMult, float rngMult)
    {
        if(m_UpgradeLevel >= m_MaxUpgradeLevel)
            return false;

        m_UpgradeLevel++;
        m_Damage = (int)(m_Damage * dmgMult);
        m_ShootSpeed = m_ShootSpeed * spdMult;
        m_Range = (int)(m_Range * rngMult);
        return true;
    }

    private IEnumerator ShootEnemies()
    {
        if(m_pool != null)
        {
            var enemies = FindObjectsOfType<Enemy>();
            float min = float.MaxValue;
            Enemy minEnemy = null;
            foreach(var enemy in enemies)
            {
                if(enemy == null)
                {
                    continue;
                }
                var dis = Vector2.Distance(enemy.transform.position, transform.position);
                if(dis < m_Range && dis < min)
                {
                    min = dis;
                    minEnemy = enemy;
                }
            }

            if(minEnemy != null)
            {
                GameObject bulletObj;
                m_pool.TryGetNextObject(transform.position, Quaternion.identity, out bulletObj);

                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.SetDirection(minEnemy.transform.position);
                bullet.SetDamage(m_Damage);

                var dir = minEnemy.transform.position - transform.position;
                
				weapon.right = -dir.normalized;

                bullet.transform.Translate(dir.normalized * 0.8f);

                yield return new WaitForSeconds(m_ShootSpeed);
            }
            else
            {
                weapon.rotation = Quaternion.Euler(0, 0, -45);
            }
        }
    }
}
