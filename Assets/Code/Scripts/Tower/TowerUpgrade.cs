using System;
using Unity.VisualScripting;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    [System.Serializable]
    class Level {
        public float range = 8f;
        public int damage = 25;
        public float fireRate = 1f;
    }
    [SerializeField] private Level[] levels = new Level[2];
    [NonSerialized] public int currentLevel = 0;

    private Tower tower;
    [SerializeField] private TowerRange towerRange;
    void Awake()
    {
        tower = GetComponent<Tower>();
    }

    public void Upgrade() {
        if (currentLevel < levels.Length) { 
            tower.range = levels[currentLevel].range;
            tower.damage = levels[currentLevel].damage;
            tower.fireRate = levels[currentLevel].fireRate;

            towerRange.UpdateRange();

            currentLevel++;
            Debug.Log("Upgraded");
        }
    }
}
