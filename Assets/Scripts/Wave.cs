
using UnityEngine;
[CreateAssetMenu(fileName ="wave",menuName = "brackeys-game-jam/Wave", order = 1)]

public class Wave : ScriptableObject
{
    public Fish[] fishesInWave;

    private bool yetInit = false;

    private void Awake()
    {
        if (yetInit)
        {
            return;
        }
        
        yetInit = true;
    }
}