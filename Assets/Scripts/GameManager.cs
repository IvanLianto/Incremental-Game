using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    [Range(0f, 1f)]
    [SerializeField] private float autoCollectPercentage = 0.1f;

    [SerializeField] private ResourceConfig[] ResourcesConfigs;
    [SerializeField] private Sprite[] resourceSprites;

    [SerializeField] private Transform resourcesParent;
    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private TapText tapTextPrefab;

    [SerializeField] private Transform coinIcon;

    [SerializeField] private List<ResourceController> _activeResources = new List<ResourceController>();
    private List<TapText> tapTextPool = new List<TapText>();
    private float _collectSecond;

    private double _totalGold;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    private void Start()
    {
        AddAllResources();
    }

    private void Update()
    {
        _collectSecond += Time.unscaledDeltaTime;
        if (_collectSecond >= 1f)
        {
            CollectPerSecond();
            _collectSecond = 0f;
        }

        CheckResourceCost();

        coinIcon.localScale = Vector3.LerpUnclamped(coinIcon.localScale, Vector3.one * 2f, 0.15f);
        coinIcon.Rotate(0f, 0f, Time.deltaTime * -100f);
    }

    private void AddAllResources()
    {
        bool showResources = true;

        foreach(var config in ResourcesConfigs)
        {
            var obj = Instantiate(resourcePrefab.gameObject, resourcesParent, false);
            var resource = obj.GetComponent<ResourceController>();

            resource.SetConfig(config);
            obj.gameObject.SetActive(showResources);

            if (showResources && !resource.isUnlocked)
            {
                showResources = false;
            }

            _activeResources.Add(resource);
        }
    }

    public void ShowNextResource()
    {
        foreach(var resource in _activeResources)
        {
            if (!resource.gameObject.activeSelf)
            {
                resource.gameObject.SetActive(true);
                break;
            }
        }
    }

    private void CollectPerSecond()
    {
        double output = 0;
        foreach(var resource in _activeResources)
        {
            if (resource.isUnlocked)
            {
                output += resource.GetOutput();
            }
        }

        output *= autoCollectPercentage;

        UIMain.Instance.SetAutoCollectInfo(output);

        AddGold(output);
    }

    public void AddGold(double value)
    {
        _totalGold += value;
        UIMain.Instance.SetGoldInfoText(_totalGold);
    }

    public void CollectByTap(Vector3 tapPosition, Transform parent)
    {
        double output = 0;

        foreach(var resource in _activeResources)
        {
            if (resource.isUnlocked)
            {
                output += resource.GetOutput();
            }
        }

        var tapText = GetOrCreateTapText();
        tapText.transform.SetParent(parent, false);
        tapText.transform.position = tapPosition;

        tapText.SetText(output);
        tapText.gameObject.SetActive(true);

        coinIcon.localScale = Vector3.one * 1.75f;

        AddGold(output);

    }

    private TapText GetOrCreateTapText()
    {
        var tapText = tapTextPool.Find(t => !t.gameObject.activeSelf);

        if (tapText == null)
        {
            tapText = Instantiate(tapTextPrefab).GetComponent<TapText>();
            tapTextPool.Add(tapText);
        }
        return tapText;
    }

    private void CheckResourceCost()
    {
        foreach (var resource in _activeResources)
        {
            bool isBuyable;
            if (resource.isUnlocked)
            {
                isBuyable = TotalGold >= resource.GetUpgradeCost();
            }
            else
            {
                isBuyable = TotalGold >= resource.GetUnlockCost();
            }

            resource.GetResourceUI().SetResourceImage(resourceSprites[isBuyable ? 1 : 0]);
        }
    }

    public double TotalGold
    {
        get
        {
            return _totalGold;
        }
    }
}

[System.Serializable]
public struct ResourceConfig
{
    public string Name;
    public double UnlockCost;
    public double UpgradeCost;
    public double Output;
}
