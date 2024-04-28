using System;
using System.Collections;
using System.Collections.Generic;
using EventBus;
using EventBus.Events;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private TextMeshProUGUI worldTimeText;
    [SerializeField] private TextMeshProUGUI questAwardText;
    [SerializeField] public Color lastWTTColor = Color.white; //last world time text color

    [Space(10)]
    [Header("Properties")]
    [SerializeField] private float worldTime = 60;
    [SerializeField] private bool isWorldTimer = true;
    [SerializeField] public float wTimeBlinkSpeed = 1;
    [SerializeField] private float fadeTextDuration = 2f; // Ge�i� s�resi

    private float currentTextFadeTime = 0f; // Ge�en s�re

    private Color firsWTTColor; //firt world time text color
    private void Awake()
    {
        firsWTTColor = worldTimeText.color;
    }

    private void Update()
    {
        if (isWorldTimer)
        {
            DisplayTime();
            if (worldTime == 0)
            {
                isWorldTimer = false;
            }
            worldTime -= Time.deltaTime;
            QuestText();
        }

    }

    private void OnEnable()
    {
        EventBus<TimeAwardEvent>.Subscribe(OnTimeAward);
    }

    private void OnDisable()
    {
        EventBus<TimeAwardEvent>.Unsubscribe(OnTimeAward);
    }

    private void OnTimeAward(TimeAwardEvent @event)
    {
        TimeAward(@event.Time);
    }

    private void TimeAward(int t)
    {
        worldTime += t;
        questAwardText.gameObject.SetActive(true);
        questAwardText.text = "+" + t.ToString();
    }

    public void DisplayTime()
    {
        worldTimeText.color = firsWTTColor;
        int hours = Mathf.FloorToInt(worldTime / 60.0f);
        int seconds = Mathf.FloorToInt(worldTime - hours * 60.0f);
        worldTimeText.text = string.Format("{0:00}:{1:00}", hours, seconds);
        worldTimeText.color = Color.Lerp(firsWTTColor, lastWTTColor, Mathf.PingPong(Time.time * wTimeBlinkSpeed, 1));


    }

    private void QuestText()
    {
        if (questAwardText.gameObject.active)
        {
            currentTextFadeTime += Time.deltaTime;
            if (currentTextFadeTime < fadeTextDuration)
            {
                float alpha = Mathf.Lerp(0f, 1f, currentTextFadeTime / fadeTextDuration);
                Color newColor = questAwardText.color;
                newColor.a = alpha;
                questAwardText.color = newColor;
            }
            else
            {
                Color newColor = questAwardText.color;
                newColor.a = 0;
                questAwardText.color = newColor;
                questAwardText.gameObject.SetActive(false);
            }
        }
        else
        {
            currentTextFadeTime = 0f;
        }
    }
   /* public void DisplayPauseTime()
    {
        worldTimeText.color = Color.Lerp(firsWTTColor, lastWTTColor, Mathf.PingPong(Time.time * wTimeBlinkSpeed, 1));
    }*/
}
