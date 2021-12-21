using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public GameObject powerupPrefabs;
    public float spawnRange = 9;
    public int enemyCount;
    public int wavwNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(wavwNumber);
        SpawnPowerups();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            wavwNumber++;
            SpawnEnemyWave(wavwNumber);
            SpawnPowerups();
        }
    }

    //Select random spawn location
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomSpawnPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomSpawnPos;
    }
    //spawn enemy wave
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs, GenerateSpawnPosition(), enemyPrefabs.transform.rotation);
        }
    }
    //spawn powerup
    void SpawnPowerups()
    {

        Instantiate(powerupPrefabs, GenerateSpawnPosition(), powerupPrefabs.transform.rotation);

    }

}
