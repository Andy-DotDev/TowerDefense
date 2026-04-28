using UnityEngine;

public class RocketTower : Tower
{
    [Header("Настройки ракеты-башни")]
    public float rocketSpeed = 3f;
    public float explosionRadius = 2f;

    void Start()
    {
        fireRate = 2f;
        damage = 70;
        rotationSpeed = 60f;
        projectileSpeed = rocketSpeed;
    }
}
