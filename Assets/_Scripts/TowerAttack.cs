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
    private Sprite m_BulletSprite;
 
    [SerializeField]
    private int m_MaxUpgradeLevel = 3;

    [SerializeField]
    public Transform weapon;
    [SerializeField] private float m_weaponSpeed;

    public bool canUpgrade { get { return m_UpgradeLevel != m_MaxUpgradeLevel; } }

    private void Start()
    {
        if(m_pool == null)
        {
            m_pool = FindObjectOfType<EZObjectPool>();
        }
    }

    private void Update()
    {
        if(minEnemy == null)
        {
            weapon.rotation = Quaternion.Slerp(weapon.rotation, Quaternion.Euler(0, 0, -45), Time.deltaTime * m_weaponSpeed);
            return;
        }
        var dir = minEnemy.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Slerp(weapon.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * m_weaponSpeed);
    }

    private void FixedUpdate()
    {
        if(++m_count == 20)
        {
            StartCoroutine(ShootEnemies());
            m_count = 0;
        }
    }

    public void SetTowerValues(int damage, int range, float shootSpeed)
    {
        m_Damage = damage;
        m_Range = range;
        m_ShootSpeed = shootSpeed;
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

    private float min = float.MaxValue;
    private Enemy minEnemy = null;
    [SerializeField] private Transform shooting;


    private IEnumerator ShootEnemies()
    {
        min = float.MaxValue;
        minEnemy = null;

        if(m_pool != null)
        {
            var enemies = FindObjectsOfType<Enemy>();

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
                m_pool.TryGetNextObject(shooting.position, weapon.rotation, out bulletObj);

                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.SetDamage(m_Damage);
                bullet.SetSprite(m_BulletSprite);
                bullet.Shoot();

                yield return new WaitForSeconds(m_ShootSpeed);
            }
        }
    }
}
