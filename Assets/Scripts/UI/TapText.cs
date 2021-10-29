using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TapText : MonoBehaviour
{
    [SerializeField] private float spawnTime = .5f;
    [SerializeField] private Text text;

    private float _spawnTime;

    private void OnEnable()
    {
        _spawnTime = spawnTime;
    }

    private void Update()
    {
        _spawnTime -= Time.unscaledDeltaTime;
        if (_spawnTime <= 0f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            text.CrossFadeAlpha(0f, .5f, false);
            if (text.color.a == 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void SetText(double output)
    {
        text.text = $"+{output:0}";
    }
}
