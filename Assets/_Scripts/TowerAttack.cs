using EZObjectPools;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TowerUpgrade))]
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

    [SerializeField]
    private int m_MaxUpgradeLevel = 3;

    private TowerUpgrade upgrade;

    private void Start()
    {
        upgrade = GetComponent<TowerUpgrade>();

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

    /// <summary>
    /// Called when an upgrade tower card is used on a tower
    /// </summary>
    /// <returns><c>true</c> if upgrade successful, <c>false</c> if tower is fully upgraded</returns>
    public bool UpgradeTower()
    {
        if(upgrade.level >= upgrade.maxLevel)
            return false;

        upgrade.level++;
        m_Damage = (int)(m_Damage * upgrade.damageBoost);
        m_Range = (int)(m_Range * upgrade.rangeBoost);
        m_ShootSpeed = m_ShootSpeed * upgrade.speedBoost;
        return true;
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
