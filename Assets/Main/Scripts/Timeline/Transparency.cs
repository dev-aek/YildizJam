using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Timeline
{
    public class TextTransparencyController : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 5f; 
        private float currentFadeTime = 0f; 
        private TextMeshProUGUI textComponent; 

        private void Start()
        {
            
            textComponent = GetComponent<TextMeshProUGUI>();

            
            Color newColor = textComponent.color;
            newColor.a = 0f;
            textComponent.color = newColor;
        }

        private void Update()
        {
            
            currentFadeTime += Time.deltaTime;
            

            if (currentFadeTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(0f, 1f, currentFadeTime / fadeDuration);
                Color newColor = textComponent.color;
                newColor.a = alpha;
                textComponent.color = newColor;
                
            }
            Invoke("OpenScene", 5f);
            
        }

        public void OpenScene()
        {
            SceneManager.LoadScene("Deneme 3");
        }
    }
}
