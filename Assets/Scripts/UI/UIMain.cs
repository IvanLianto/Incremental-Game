using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMain : MonoBehaviour
{
    public static UIMain Instance;

    [SerializeField] private Text goldInfo;
    [SerializeField] private Text autoCollectInfo;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetAutoCollectInfo(double output)
    {
        autoCollectInfo.text = $"Auto Collect: { output:F1} / second";
    }

    public void SetGoldInfoText(double value)
    {
        goldInfo.text = $"Gold: {value:0}";
    }

}
