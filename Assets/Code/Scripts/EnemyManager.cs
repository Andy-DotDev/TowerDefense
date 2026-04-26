using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager: MonoBehaviour
{
    public static EnemyManager main;

    public Transform startPoint;
    public Transform[] path;

    [Header("Enemies")]
    [SerializeField] private GameObject soldier;
    [SerializeField] private GameObject tankEnemy;
    [SerializeField] private GameObject fastEnemy;

    [SerializeField] private int enemyCount = 6;

    [SerializeField] private float soldierRate = 0.5f;
    [SerializeField] private float tankEnemyRate = 0.1f;
    [SerializeField] private float fastEnemyRate = 0.4f;

    private List<GameObject> waveSet = new List<GameObject>();

    private int soldierCount;
    private int fastEnemyCount;
    private int tankEnemyCount;
    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        SetWave();
    }
    private void Update()
    {
        
    }

    private void SetWave() {
        soldierCount = Mathf.RoundToInt(enemyCount * soldierRate);
        tankEnemyCount = Mathf.RoundToInt(enemyCount * tankEnemyRate);
        fastEnemyCount = Mathf.RoundToInt(enemyCount * fastEnemyRate);

        waveSet = new List<GameObject>();

        for (int i = 0; i < soldierCount; i++) waveSet.Add(soldier);
        for (int i = 0; i < tankEnemyCount; i++) waveSet.Add(tankEnemy);
        for (int i = 0; i < fastEnemyCount; i++) waveSet.Add(fastEnemy);

        StartCoroutine(spawn());
    }

    IEnumerator spawn() {
        for (int i = 0; i < waveSet.Count; i++) {
            Instantiate(waveSet[i], startPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
