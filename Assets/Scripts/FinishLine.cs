using TMPro;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject VictoryImage;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI pointsText;
    private float timeSpend;
    private int score = 0;
    private int points = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeSpend = Time.timeSinceLevelLoad;
            timeText.SetText("{0}s", timeSpend);
            score = (Mathf.Max(0, (int)(100 - timeSpend)) + 1) * ((int)(points * 10) + 1);
            scoreText.SetText("{0}", score);

            VictoryImage.SetActive(true);
        }
    }

    public void IncreasePoints()
    {
        points += 100;
        pointsText.text = "Points: " + points;
    }
}