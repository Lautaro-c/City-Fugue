using TMPro;

using UnityEngine;
 
public class FPSShower : MonoBehaviour

{

    [SerializeField] private TextMeshProUGUI fpsText;

    private float timer;

    private int frameCount;

    private void Update()

    {

        frameCount++;

        timer += Time.unscaledDeltaTime;

        if (timer >= 0.5f)

        {

            float fps = frameCount / timer;

            fpsText.SetText("{0:0} FPS", fps);

            frameCount = 0;

            timer = 0f;

        }

    }

}
