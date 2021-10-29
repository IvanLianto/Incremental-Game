using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ResourceUI : MonoBehaviour
{
    [SerializeField] private Button resourceButton;
    [SerializeField] private Image resourceImage;
    [SerializeField] private Text resourceDescription;
    [SerializeField] private Text resourceUpgradeCost;
    [SerializeField] private Text resourceUnlockCost;

    public void Init(UnityAction onClick)
    {
        resourceButton.onClick.AddListener(onClick);
    }

    public void SetActivated(bool unlocked)
    {
        resourceUnlockCost.gameObject.SetActive(!unlocked);
        resourceUpgradeCost.gameObject.SetActive(unlocked);
    }

    public void SetControllerUI(string name, int level, double output, double unlockCost, double upgradeCost)
    {
        resourceDescription.text = $"{ name } Lv. { level }\n+{ output:0}";

        resourceUnlockCost.text = $"Unlock Cost\n{ unlockCost }";

        resourceUpgradeCost.text = $"Upgrade Cost\n{ upgradeCost }";
    }

    public void SetUpgradeUI(string name, int level, double output, double upgradeCost)
    {
        resourceUpgradeCost.text = $"Upgrade Cost\n{upgradeCost}";

        resourceDescription.text = $"{name} Lv. {level}\n+{output:0}";
    }

    public void SetResourceImage(Sprite sprite)
    {
        resourceImage.sprite = sprite;
    }

    public void SetResourceImageColor(Color color)
    {
        resourceImage.color = color;
    }
}
