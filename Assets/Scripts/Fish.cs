using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Fish", menuName = "brackeys-game-jam/Fish", order = 0)]
public class Fish : ScriptableObject
{
    [Tooltip("Rareza del pez normal siendo 1 el mas bajo")]
    [Range(1, 10)]
    public int rare;
    [Tooltip("probabilidad de que sea shiny")]
    [Range(0, 0.5f)]
    public float shinyProb;
    [Range(1, 10)]
    [Tooltip("Velocidad de spawn del pez")]
    public float speed;
    [Range(1, 10)]
    [Tooltip("Profundidad de spawn del pez")]
    public int deepingSpawn;
    [Multiline(3)]
    [Tooltip("nombre del pez")]
    public string fishName;


    public GameObject prefab;




    private bool yetInit = false;
    private void Awake()
    {        
        // if (yetInit)
        // {
        //     return;
        // }

        // yetInit = true;
    }



}
