using UnityEngine;

public class Enemy: MonoBehaviour
{
    private SpriteRenderer sr;
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;


    [Header("Attributes")]
    [SerializeField] private int health = 50;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float flipThreshold = 0.1f;
    [SerializeField] private float minMoveThreshold = 0.01f;

    private Transform target;
    private int pathIndex = 0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        target = EnemyManager.main.path[pathIndex];
    }

    private void Update()
    {


        if (Vector2.Distance(target.position, transform.position) <= 0.1f) { 
            pathIndex++;
            if (pathIndex >= EnemyManager.main.path.Length)
            {
                EnemyManager.main.OnEnemyDestroyed();
                Destroy(gameObject);
                return;
            }
            else {
                target = EnemyManager.main.path[pathIndex];
            }
        }
        if (health <= 0) {
            EnemyManager.main.OnEnemyDestroyed();
            Destroy(gameObject);
            return;
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        if (direction.magnitude > minMoveThreshold) {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Round(angle / 90f) * 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
           

        rb.linearVelocity = direction * moveSpeed;
    }

    public void damage(int damage) { 
        health-=damage;
        Debug.Log("-25");
    }
}
