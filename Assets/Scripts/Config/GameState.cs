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
    public TMP_Text currentMoneyText;
    public TMP_Text totalMoneyText;

    private float depth;
    private float maxDepth;
    private float oxygen;
    private float thisDiveMoney;
    private float totalMoney;

    private bool isOnSurface = true;

    public float Oxygen { get => oxygen; }
    public bool IsOnSurface { get => isOnSurface; set => isOnSurface = value; }

    public float CurrentMoney { get => thisDiveMoney; }

    public ArrayList photos = new ArrayList();
    public ArrayList moneyValues = new ArrayList();
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

        currentMoneyText.text = "$: " + thisDiveMoney.ToString("0.00");
        totalMoneyText.text = "Total $: " + totalMoney.ToString("0.00");
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
        foreach (GameObject moneyValue in moneyValues)
        {
            Destroy(moneyValue);
        }
        moneyValues = new ArrayList();
    }

    public void returnedToSurface()
    {
        isOnSurface = true;
        oxygen = 100f;

        totalMoney += thisDiveMoney;
        thisDiveMoney = 0f;
    }

    public bool canTakePhoto()
    {
        if (photos.Count < 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void addPhoto(GameObject gameObject, float moneyValue)
    {
        int position = photos.Count;

        GameObject newFish = Instantiate(gameObject);
        newFish.GetComponent<NavMeshAgent>().enabled = false;
        newFish.transform.SetParent(photosArea.transform, false);
        newFish.transform.localPosition = new Vector3(photosArea.transform.position.x, photosArea.transform.position.y - (position * 75f), photosArea.transform.position.z);
        newFish.transform.localRotation = photosArea.transform.rotation;
        newFish.transform.localScale = new Vector3(100f, 100f, 0f);
        newFish.tag = "photoBeforeSold";

        GameObject textObject = new GameObject("moneyValueText");
        RectTransform rectTransform = textObject.AddComponent<RectTransform>();
        TMP_Text moneyValueText = textObject.AddComponent<TextMeshProUGUI>();
        moneyValueText.text = moneyValue.ToString("0.00");
        moneyValueText.fontSize = 28;
        moneyValueText.alignment = TextAlignmentOptions.Center;

        textObject = Instantiate(textObject);
        textObject.transform.SetParent(photosArea.transform, false);
        textObject.transform.localPosition = new Vector3(photosArea.transform.position.x, photosArea.transform.position.y - (position * 75f) - 10f, photosArea.transform.position.z);
        textObject.transform.localRotation = photosArea.transform.rotation;
        textObject.transform.localScale = new Vector3(1f, 1f, 0f);

        photos.Add(newFish);
        moneyValues.Add(textObject);

        thisDiveMoney += moneyValue;
    }
}
