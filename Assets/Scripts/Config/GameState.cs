using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

    public ArrayList photos = new ArrayList();
    public GameObject photosArea;

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

        foreach (GameObject fish in photos)
        {
            Destroy(fish);
        }
        photos = new ArrayList();
    }

    public void returnedToSurface()
    {
        isOnSurface = true;
        oxygen = 100f;
    }

    public void addPhoto(GameObject gameObject) {
        int position = photos.Count;

        GameObject newFish = Instantiate(gameObject); 
        newFish.GetComponent<NavMeshAgent>().enabled = false; 
        newFish.transform.parent = photosArea.transform;
        newFish.transform.localPosition = new Vector3(photosArea.transform.position.x, photosArea.transform.position.y - (position * 75f), photosArea.transform.position.z);
        newFish.transform.localRotation = photosArea.transform.rotation;
        newFish.transform.localScale = new Vector3(100f, 100f, 0f); 
        newFish.tag = "photoBeforeSold";

        photos.Add(newFish); 
    }
}
