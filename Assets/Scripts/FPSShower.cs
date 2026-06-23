using TMPro;
using UnityEngine;

public class FPSShower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} FPS", fps);
        fpsText.text = text;
    }
}
