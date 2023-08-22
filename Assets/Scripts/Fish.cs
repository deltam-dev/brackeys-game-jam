using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Fish", menuName = "brackeys-game-jam/Fish", order = 0)]
public class Fish : ScriptableObject
{
    [Tooltip("Rareza del pez siendo 1 el mas bajo")]
    [Range(1, 4)]
    public int rare;
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

        RaycastHit2D raycastHit2D;
    

    private bool yetInit = false;
    private void Awake()
    {
        if (yetInit)
        {
            return;
        }

        yetInit = true;
    }

    private void Update(){
        
        raycastHit2D=Physics2D.Raycast(prefab.transform.position,prefab.transform.right,1f);
    }

}