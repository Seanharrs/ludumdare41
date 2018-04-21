using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    public float damage;
    [SerializeField] private float m_velocity;

    public void SetDirection(Vector2 target)
    {
        GetComponent<Rigidbody2D>().velocity = (target - (Vector2)transform.position).normalized * m_velocity;
        Destroy(gameObject, 5f);
    }
}
