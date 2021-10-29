using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResourceController : MonoBehaviour
{
    [SerializeField] private ResourceUI rui;
    private ResourceConfig config;

    private int level = 1;

    public bool isUnlocked { get; private set; }

    private void Start()
    {
        rui.Init(() => { 
            if (isUnlocked)
            {
                UpgradeLevel();
            } else
            {
                UnlockResource();
            }
        });
    }

    public void SetConfig(ResourceConfig config)
    {
        this.config = config;
        rui.SetControllerUI(config.Name, level, GetOutput(), GetUnlockCost(), GetUpgradeCost());
        SetUnlocked(config.UnlockCost == 0);
    }

    public void UpgradeLevel()
    {
        double upgradeCost = GetUpgradeCost();
        if (GameManager.Instance.TotalGold < upgradeCost)
        {
            return;
        }

        GameManager.Instance.AddGold(-upgradeCost);
        level++;

        rui.SetUpgradeUI(config.Name, level, GetOutput(), GetUpgradeCost());
    }

    public void UnlockResource()
    {
        double unlockCost = GetUnlockCost();

        if (GameManager.Instance.TotalGold < unlockCost)
        {
            return;
        }

        SetUnlocked(true);
        GameManager.Instance.ShowNextResource();
        AchievementController.Instance.UnlockAchievement(AchievementType.UnlockResource, config.Name);
    }

    public void SetUnlocked(bool unlocked)
    {
        isUnlocked = unlocked;
        rui.SetResourceImageColor(isUnlocked ? Color.white : Color.grey);
        rui.SetActivated(unlocked);
    }

    public double GetOutput()
    {
        return config.Output * level;
    }

    public double GetUpgradeCost()
    {
        return config.UpgradeCost * level;
    }

    public double GetUnlockCost()
    {
        return config.UnlockCost;
    }

    public ResourceUI GetResourceUI()
    {
        return rui;
    }

}
