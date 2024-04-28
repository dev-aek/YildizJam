using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI volumeText;
    public Slider slider;

    private void Start()
    {
        LoadAudio();
    }

    public void SetAudio()
    {
        float value = slider.value;
        AudioListener.volume = value;
        volumeText.text = ((int)(value * 100)).ToString();
        SaveAudio();
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void SaveAudio()
    {
        PlayerPrefs.SetFloat("audioVolume", AudioListener.volume);
    }
    private void LoadAudio()
    {
        if (PlayerPrefs.HasKey("audioVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("audioVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("audioVolume", .5f);
            AudioListener.volume = PlayerPrefs.GetFloat("audioVolume");
        }
    }
}
