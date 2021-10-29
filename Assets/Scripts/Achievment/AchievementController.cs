using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementController : MonoBehaviour
{
    private static AchievementController instance = null;

    public static AchievementController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AchievementController>();
            }
            return instance;
        }
    }

    [SerializeField] private List<AchievementData> achievementList;

    public void UnlockAchievement(AchievementType type, string value)
    {
        var achievement = achievementList.Find(a => a.type == type && a.value == value);
        if (achievement != null && !achievement.isUnlocked)
        {
            achievement.isUnlocked = true;
            AchievementUI.Instance.ShowAchivementPopUp(achievement);
        }
    }
}

[System.Serializable]
public class AchievementData
{
    public string title;
    public AchievementType type;
    public string value;
    public bool isUnlocked;
}

public enum AchievementType
{
    UnlockResource
}
