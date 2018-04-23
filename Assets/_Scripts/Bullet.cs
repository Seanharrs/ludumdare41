using EZObjectPools;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PooledObject))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    private float m_Damage;
    public float damage { get { return m_Damage; } }

    [SerializeField]
    private float m_Velocity = 10;

    private bool m_Rotate;
    private float m_RotateDirection;
    private float m_RotateSpeed;

    private void Start()
    {
        m_RotateDirection = Random.Range(0, 2) == 0 ? 1 : -1;
        m_RotateSpeed = Random.Range(30f, 120f);
    }

    private void Update()
    {
        if(m_Rotate)
        {
            transform.Rotate(m_RotateDirection * Vector3.forward * Time.deltaTime * m_RotateSpeed);
        }
    }
    
    private void OnBecameInvisible()
    {
        Disable();
    }

    public void Disable()
    {
        GetComponent<PooledObject>().Disable();
    }

    public void SetDirection(Vector2 target)
    {
        GetComponent<Rigidbody2D>().velocity = (target - (Vector2)transform.position).normalized * m_Velocity;
    }

    public void Shoot()
    {
        GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.right);
        m_Rotate = true;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
    }

    public void SetDamage(int dmg)
    {
        m_Damage = dmg;
    }

    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
