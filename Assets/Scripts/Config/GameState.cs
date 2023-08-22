using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    public TMP_Text depthText;
    public TMP_Text maxDepthText;

    private float depth;
    private float maxDepth;

    private void Awake()
    {
        Instance = this;
    }

    public void updateDepth(float value)
    {
        depth = value;
        if (depth < maxDepth) {
            maxDepth = depth;
        }
        
        string str = (value * -1).ToString("0");
        string str2 = (maxDepth * -1).ToString("0");
        depthText.text = str + " m";
        maxDepthText.text = "max: " + str2 + " m";
    }
}
