using System;
using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Настройки башни")]
    public float range = 5f;
    public int damage = 25;
    public float fireRate = 1f;
    public float rotationSpeed = 120f;
    public float projectileSpeed = 7f;

    [Header("Targeting mode")]
    public bool first = true;
    public bool last = false;
    public bool strong = false;

    [Header("Effects")]
    [SerializeField] GameObject fireEffect;

    [NonSerialized]
    public GameObject target;
    private float cooldown = 0f;
    void Start()
    {
        fireEffect.SetActive(false);
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
                Shoot();
                cooldown = 0f;
                
            }
            else {
                cooldown += 1 * Time.deltaTime;
            }

        }
    }

    private void Shoot() {
        target.GetComponent<Enemy>().damage(damage);
        StartCoroutine(FireEffect());
    }
    IEnumerator FireEffect() {
        GameObject projectile = Instantiate(fireEffect, transform.position, transform.rotation);
        projectile.SetActive(true);

        Vector3 targetPosition = target.transform.position;
        float spawnTime = Time.time;

        // Двигаем снаряд к цели
        while (Vector3.Distance(projectile.transform.position, targetPosition) > 0.1f)
        {
            projectile.transform.position = Vector3.MoveTowards(
                projectile.transform.position,
                targetPosition,
                projectileSpeed * Time.deltaTime
            );

            // Поворачиваем снаряд по направлению движения
            Vector3 direction = targetPosition - projectile.transform.position;
            if (direction != Vector3.zero)
            {
                projectile.transform.up = direction;
            }

            yield return null;
        }

        // Уничтожаем снаряд
        Destroy(projectile);
    }

}
