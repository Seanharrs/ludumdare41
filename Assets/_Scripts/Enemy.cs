using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_health = 100f;

    public float turnSpeed;
    public float speed;

    Path path;
    int lineIndex;
    bool previousSide;
    bool currentSide;
    Vector2 perpendicularPointOnLine;

    bool stop;

    [SerializeField]
    SpriteRenderer m_SpriteRenderer;

    void Start()
    {
        path = FindObjectOfType<EnemeyPathGenerator>().path;
    }

    void Update()
    {
        Vector3 targetPos = Vector3.zero;

        if(path != null)
        {
            targetPos = FollowPath();
        }

        //Vector2 targetDir = Seek(targetPos);
        //if(targetDir != Vector2.zero)
        //{
        //    Rotate(targetDir);
        //}
        Move(targetPos);
    }

    Vector2 FollowPath()
    {

        // find the  shortest perpendicular to the line ..
        Line line = path.lines[lineIndex];
        currentSide = line.GetSide(transform.position);

        if(currentSide != previousSide)
        {
            lineIndex++;

            if(lineIndex >= path.lines.Length)
            {
                lineIndex = path.lines.Length - 1;
                currentSide = line.GetSide(transform.position);
                previousSide = currentSide;
            }

            line = path.lines[lineIndex];
            perpendicularPointOnLine = line.starPoint;

            previousSide = currentSide = line.GetSide(transform.position);
        }

        previousSide = currentSide;
        perpendicularPointOnLine = line.PerpendicularPointOnLine(transform.position);

        Vector2 targetPosition = perpendicularPointOnLine + (line.endPoint - line.starPoint).normalized * 0.5f;


        return (targetPosition);

    }

    Vector2 Seek(Vector2 targetPos)
    {
        return (targetPos - (Vector2)transform.position).normalized;
    }

    //void Rotate(Vector2 targetDir)
    //{

    //    float targetAngle = Mathf.Atan2(targetDir.y, (targetDir.x + 0.01f)) * Mathf.Rad2Deg;

    //    Quaternion finalRot = Quaternion.AngleAxis(targetAngle, Vector3.forward);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, finalRot, turnSpeed * Time.deltaTime);

    //}

    void Move(Vector3 targetPos)
    {

        if(stop)
        {
            return;
        }
        Vector2 offset = (transform.position - targetPos);
        if(offset.x < 0 && !m_SpriteRenderer.flipX)
            m_SpriteRenderer.flipX = true;
        else if(offset.x >= 0 && m_SpriteRenderer.flipX)
            m_SpriteRenderer.flipX = false;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        //transform.Translate((Vector3.right) * speed * Time.deltaTime, Space.Self);

    }


    private void Damage(float damage)
    {
        m_health -= damage;
        if(m_health <= 0)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponentInChildren<Animator>().SetTrigger("Die");
            Destroy(gameObject, 4f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Damage(collision.GetComponent<Bullet>().damage);
            collision.GetComponent<Bullet>().Disable();
        }
    }
}
