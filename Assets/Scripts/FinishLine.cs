using TMPro;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject VictoryImage;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI pointsText;
    private float timeSpend;
    private int score;
    private float health;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeSpend = Time.timeSinceLevelLoad;
            timeText.SetText("{0:F2}s", timeSpend);

            health = HealthManager.Instance.GetHealth();
            float maxHealth = HealthManager.Instance.GetMaxHealth();
            healthText.SetText("{0}/{1}", health, maxHealth);

            score = (Mathf.Max(0, (int)(1000 - timeSpend * 10)) + 1) * ((int)(health * 10) + 1);
            pointsText.SetText("{0}", score);

            VictoryImage.SetActive(true);
        }
    }
}