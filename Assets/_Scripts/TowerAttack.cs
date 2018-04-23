using EZObjectPools;
using System.Collections;
using UnityEngine;

public class TowerAttack : MonoBehaviour, ITower
{
    private static EZObjectPool m_pool;

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

    [SerializeField]
    private Transform shooting;

    private Enemy target;

    public bool canUpgrade { get { return m_UpgradeLevel != m_MaxUpgradeLevel; } }

    private void Start()
    {
        InvokeRepeating("FindTarget", 0.01f, 1f);
        StartCoroutine(Shoot());

        if(m_pool == null)
        {
            m_pool = FindObjectOfType<EZObjectPool>();
        }
    }

    private void Update()
    {
        if(target == null)
        {
            weapon.rotation = Quaternion.Slerp(weapon.rotation, Quaternion.Euler(0, 0, -45), Time.deltaTime * m_weaponSpeed);
            return;
        }
        var dir = target.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Slerp(weapon.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * m_weaponSpeed);
    }

    void FindTarget()
    {
        target = null;
        float minDist = float.MaxValue;
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        for(int i = 0; i < enemies.Length; i++)
        {
            float dist = Vector2.Distance(transform.position, enemies[i].transform.position);
            if(dist <= m_Range && dist < minDist)
            {
                minDist = dist;
                target = enemies[i];
            }
        }
    }

    IEnumerator Shoot()
    {
        if(target == null)
            yield return null;

        while(Vector2.Distance(transform.position, target.transform.position) < m_Range)
        {
            GameObject bulletObj;
            m_pool.TryGetNextObject(shooting.position, weapon.rotation, out bulletObj);

            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.SetDamage(m_Damage);
            bullet.SetSprite(m_BulletSprite);
            bullet.Shoot();

            yield return new WaitForSeconds(1 / m_ShootSpeed);
        }
    }

    public void SetTowerValues(int damage, int range, float shootSpeed)
    {
        m_Damage = damage;
        m_Range = range;
        m_ShootSpeed = shootSpeed;
    }

    //public void Highlight()
    //{
    //    Color color = GetComponent<SpriteRenderer>().color;
    //    color.g = 255;
    //    GetComponent<SpriteRenderer>().color = color;
    //}

    //public void RemoveHighlight()
    //{
    //    Color color = GetComponent<SpriteRenderer>().color;
    //    color.g = 155;
    //    GetComponent<SpriteRenderer>().color = color;
    //}

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
        if(m_UpgradeLevel > 1)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
        m_Damage = (int)(m_Damage * dmgMult);
        m_ShootSpeed = m_ShootSpeed * spdMult;
        m_Range = (int)(m_Range * rngMult);
        return true;
    }
}
