using System.Collections;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    [SerializeField] private GameObject mBulletPrefab;
    [SerializeField] private float delay;

    private void Start()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        if(Enemy.enemiesAlive != null)
        {
            foreach(var enemy in Enemy.enemiesAlive)
            {
                if(Vector2.Distance(enemy.transform.position, transform.position) < 5)
                {
                    Instantiate(mBulletPrefab, transform.position, Quaternion.identity, transform)
                        .GetComponent<Bullet>()
                        .SetDirection(enemy.transform.position);
                    yield return new WaitForSeconds(delay);
                }
            }
            StartCoroutine(Shooting());
        }
    }
}
