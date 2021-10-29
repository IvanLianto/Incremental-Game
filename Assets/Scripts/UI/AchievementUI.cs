using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementUI : MonoBehaviour
{
    private static AchievementUI _instance = null;

    public static AchievementUI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AchievementUI>();
            }
            return _instance;
        }
    }

    [SerializeField] private Transform popUpTransform;
    [SerializeField] private Text popUpText;
    [SerializeField] private float popUpShowDuration = 3f;

    private float popUpShowDurationCounter;

    void Update()
    {
        if (popUpShowDurationCounter > 0)
        {
            popUpShowDurationCounter -= Time.unscaledDeltaTime;
            popUpTransform.localScale = Vector3.LerpUnclamped(popUpTransform.localScale, Vector3.one, .5f);
        } else
        {
            popUpTransform.localScale = Vector2.LerpUnclamped(popUpTransform.localScale, Vector3.right, .5f);
        }
    }

    public void ShowAchivementPopUp(AchievementData data)
    {
        popUpText.text = data.title;
        popUpShowDurationCounter = popUpShowDuration;
        popUpTransform.localScale = Vector2.right;
    }

}
