using TMPro;
using UnityEngine;

namespace Timeline
{
    public class TextTransparencyController : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 5f; // Geçiş süresi
        private float currentFadeTime = 0f; // Geçen süre
        private TextMeshProUGUI textComponent; // Text objesinin referansı

        private void Start()
        {
            // Text objesini al
            textComponent = GetComponent<TextMeshProUGUI>();

            // Başlangıçta transparanlık değeri 0 olsun
            Color newColor = textComponent.color;
            newColor.a = 0f;
            textComponent.color = newColor;
        }

        private void Update()
        {
            // Geçen süreyi arttır
            currentFadeTime += Time.deltaTime;

            // Geçen süre, geçiş süresinden küçük olduğu sürece transparanlık değerini güncelle
            if (currentFadeTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(0f, 1f, currentFadeTime / fadeDuration);
                Color newColor = textComponent.color;
                newColor.a = alpha;
                textComponent.color = newColor;
            }
        }
    }
}
