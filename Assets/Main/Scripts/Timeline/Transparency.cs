using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTransparencyController : MonoBehaviour
{
    public float fadeDuration = 5f; // Ge�i� s�resi
    private float currentFadeTime = 0f; // Ge�en s�re
    private TextMeshProUGUI textComponent; // Text objesinin referans�

    void Start()
    {
        // Text objesini al
        textComponent = GetComponent<TextMeshProUGUI>();

        // Ba�lang��ta transparanl�k de�eri 0 olsun
        Color newColor = textComponent.color;
        newColor.a = 0f;
        textComponent.color = newColor;
    }

    void Update()
    {
        // Ge�en s�reyi artt�r
        currentFadeTime += Time.deltaTime;

        // Ge�en s�re, ge�i� s�resinden k���k oldu�u s�rece transparanl�k de�erini g�ncelle
        if (currentFadeTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentFadeTime / fadeDuration);
            Color newColor = textComponent.color;
            newColor.a = alpha;
            textComponent.color = newColor;
        }
    }
}
