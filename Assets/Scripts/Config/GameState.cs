using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    public TMP_Text depthText;
    public TMP_Text maxDepthText;
    public TMP_Text oxygenText;

    private float depth;
    private float maxDepth;
    private float oxygen;

    private bool isOnSurface = true;

    public float Oxygen { get => oxygen; }
    public bool IsOnSurface { get => isOnSurface; set => isOnSurface = value; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        oxygen = 100f;
    }

    void FixedUpdate()
    {
        if (!isOnSurface)
        {
            oxygen -= Time.deltaTime;
        }

        string strOxygen = oxygen.ToString("0");
        oxygenText.text = "O2: " + strOxygen;
    }

    public void updateDepth(float value)
    {
        depth = value;
        if (depth < maxDepth)
        {
            maxDepth = depth;
        }

        string str = (value * -1).ToString("0");
        string str2 = (maxDepth * -1).ToString("0");
        depthText.text = str + " m";
        maxDepthText.text = "max: " + str2 + " m";
    }

    public void startDiving()
    {
        isOnSurface = false;
    }

    public void returnedToSurface()
    {
        isOnSurface = true;
        oxygen = 100f;
    }
}
