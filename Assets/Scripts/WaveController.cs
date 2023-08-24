using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public Wave wave;
    public float timeBetweenWaves  = 2f;
    private float timeToLastWave = 0f;
    private int i = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float currenTimePass = Time.time - timeToLastWave;
        if (currenTimePass > timeBetweenWaves && i <= 1)
        {
            Vector3 position=new Vector3(transform.position.x+1, transform.position.y,0);
            Instantiate(wave.fishesInWave[i].prefab, position, transform.rotation);
            timeToLastWave = Time.time;
            i++;
        }

    }
}
