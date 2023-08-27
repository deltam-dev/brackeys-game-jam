using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public Wave wave;

    [Range(1, 10)]
    [SerializeField]private  int level;
    public float timeBetweenWaves  = 2f;
    private int i = 0;
    [SerializeField] private int limit = 10;
    private int spawnCount = 0;

    void Start()
    {
        InvokeRepeating("SpawnFish", 0f, timeBetweenWaves);
    }

    void SpawnFish()
    {
        if (spawnCount >= limit)
        {
            CancelInvoke("SpawnFish");
            return;
        }

        Vector3 position = new Vector3(transform.position.x + 1, transform.position.y, 0);

        if (wave.fishesInWave[i].deepingSpawn >= level)
        {
            Instantiate(wave.fishesInWave[i].prefab, position, transform.rotation);
            Debug.Log(gameObject.name + " ha espawneado un " + wave.fishesInWave[i].fishName);
            spawnCount++;
        }
        else
        {
            i = Random.Range(1, 10);
        }
    }
}
