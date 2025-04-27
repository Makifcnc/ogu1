using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float idleTime = 2.0f;
    private bool isWaiting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        // anim.SetBool("isRunning", true);
    }

    void Update()
    {
        if (!isWaiting)
        {
            if (currentPoint == pointB.transform)
            {
                rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            }
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && !isWaiting)
        {
            StartCoroutine(WaitAndChangePoint());
        }
    }

    private IEnumerator WaitAndChangePoint()
    {
        isWaiting = true;
        rb.linearVelocity = Vector2.zero;
        // anim.SetBool("isRunning", false);
        yield return new WaitForSeconds(idleTime);

        if (currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }
        else
        {
            currentPoint = pointB.transform;
        }

        Flip();
        // anim.SetBool("isRunning", true);
        isWaiting = false;
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pointA.transform.position, 0.3f);
            Gizmos.DrawWireSphere(pointB.transform.position, 0.3f);
            Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        }
    }
}
