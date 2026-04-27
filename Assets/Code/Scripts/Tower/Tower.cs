using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Настройки башни")]
    public float range = 5f;
    public int damage = 25;
    public float fireRate = 1f;
    public float rotationSpeed = 30f;

    public GameObject target;
    private float cooldown = 0f;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (target) {
            Vector3 direction = target.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
            if (cooldown >= fireRate)
            {
                transform.up = target.transform.position - transform.position;
                target.GetComponent<Enemy>().damage(damage);
                cooldown = 0f;
            }
            else {
                cooldown += 1 *Time.deltaTime;
            }
            
        }
    }
}
