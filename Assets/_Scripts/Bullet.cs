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

    [SerializeField] private float m_velocity;

    [SerializeField] private Sprite[] sprites;

    private bool rotate;
    private float rotateDirection;
    private float rotateSpeed;

    private void Start()
    {
        if(sprites != null)
        {
            var sprite = GetComponent<SpriteRenderer>();
            sprite.sprite = sprites[Random.Range(0, sprites.Length)];
            rotateDirection = Random.Range(0, 2) == 0 ? 1 : -1;
            rotateSpeed = Random.Range(30f, 120f);
        }
    }

    private void Update()
    {
        if(rotate)
        {
            transform.Rotate(rotateDirection * Vector3.forward * Time.deltaTime * rotateSpeed);
        }
    }

    public void SetDirection(Vector2 target)
    {
        GetComponent<Rigidbody2D>().velocity = (target - (Vector2)transform.position).normalized * m_velocity;
        StartCoroutine(DeactiveDelay());
    }

    public void Shoot()
    {
        GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.right);
        rotate = true;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        StartCoroutine(DeactiveDelay());
    }

    private IEnumerator DeactiveDelay()
    {
        yield return new WaitForSeconds(5f);
        Disable();
    }

    public void Disable()
    {
        GetComponent<PooledObject>().Disable();
    }

    public void SetDamage(int dmg)
    {
        m_Damage = dmg;
    }
}
