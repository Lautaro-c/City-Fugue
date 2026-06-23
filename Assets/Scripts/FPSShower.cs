using TMPro;
using UnityEngine;

public class FPSShower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    private float deltaTime = 0.0f;
    private float updateInterval = 0.5f;
    private float nextUpdateTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (Time.unscaledTime >= nextUpdateTime)
        {
            nextUpdateTime = Time.unscaledTime + updateInterval;
            float fps = 1.0f / deltaTime;

            fpsText.SetText("{0:0} FPS", fps);
        }
    }
}