using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTransparencyController : MonoBehaviour
{
    public float fadeDuration = 5f; // Geçiþ süresi
    private float currentFadeTime = 0f; // Geçen süre
    private TextMeshProUGUI textComponent; // Text objesinin referansý

    void Start()
    {
        // Text objesini al
        textComponent = GetComponent<TextMeshProUGUI>();

        // Baþlangýçta transparanlýk deðeri 0 olsun
        Color newColor = textComponent.color;
        newColor.a = 0f;
        textComponent.color = newColor;
    }

    void Update()
    {
        // Geçen süreyi arttýr
        currentFadeTime += Time.deltaTime;

        // Geçen süre, geçiþ süresinden küçük olduðu sürece transparanlýk deðerini güncelle
        if (currentFadeTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentFadeTime / fadeDuration);
            Color newColor = textComponent.color;
            newColor.a = alpha;
            textComponent.color = newColor;
        }
    }
}
