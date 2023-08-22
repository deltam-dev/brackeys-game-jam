using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public Wave wave;
    public float timeToWave = 2f;
    private float timeToLastWave = 0f;
    private int i = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float currenTimePass = Time.time - timeToLastWave;
        if (currenTimePass > timeToLastWave && i < 1)
        {
            Instantiate(wave.fishesInWave[0].prefab, transform.position, transform.rotation);
            timeToLastWave = Time.time;
            i++;
        }

    }
}
