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

    [SerializeField] private int wave = 1;
    [SerializeField] private int enemyCount = 6;
    [SerializeField] private float enemyCountRate = 0.2f;
    [SerializeField] private float spawnDelayMax = 1f;
    [SerializeField] private float spawnDelayMin = 0.75f;

    [SerializeField] private float soldierRate = 0.4f;
    [SerializeField] private float tankRate = 0.25f;
    [SerializeField] private float fastRate = 0.35f;

    private bool waveDone = false;
    private List<GameObject> waveSet = new List<GameObject>();
    private int activeEnemy = 0;
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

        if (waveDone && activeEnemy <= 0 ) {
            wave++;
            waveDone = false;
            enemyCount += Mathf.RoundToInt(enemyCount * enemyCountRate);
            SetWave();
        }
    }

    private void SetWave() {
        int soldierCount = Mathf.RoundToInt(enemyCount * (soldierRate + tankRate));
        int tankCount = 0;
        int fastCount = Mathf.RoundToInt(enemyCount * fastRate);

        if (wave % 3 == 0) {
            soldierCount = Mathf.RoundToInt(enemyCount * soldierRate);
            tankCount = Mathf.RoundToInt(enemyCount * tankRate);
        }
        
        activeEnemy = soldierCount + tankCount+ fastCount;
        waveSet = new List<GameObject>();

        for (int i = 0; i < soldierCount; i++) waveSet.Add(soldier);
        for (int i = 0; i < tankCount; i++) waveSet.Add(tankEnemy);
        for (int i = 0; i < fastCount; i++) waveSet.Add(fastEnemy);

        waveSet = Shuffle(waveSet);

        StartCoroutine(spawn());
    }

    public List<GameObject> Shuffle(List<GameObject> list) {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }

    IEnumerator spawn() {
        for (int i = 0; i < waveSet.Count; i++) {
            Instantiate(waveSet[i], startPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(spawnDelayMin,spawnDelayMax));
        }
        waveDone = true;
    }

    public void OnEnemyDestroyed() {
        if (activeEnemy > 0) activeEnemy--;
    }
}
