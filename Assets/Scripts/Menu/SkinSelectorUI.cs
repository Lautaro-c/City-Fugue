using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinSelectorUI : MonoBehaviour
{
    [SerializeField] private Sprite[] skins;
    [SerializeField] private string[] skinNames;

    [SerializeField] private Image skinPreview;
    [SerializeField] private TMP_Text skinNameText;

    private int currentSkin;

    private void Start()
    {
        UpdateUI();
    }

    public void NextSkin()
    {
        currentSkin++;

        if (currentSkin >= skins.Length)
            currentSkin = 0;

        UpdateUI();
    }

    public void PreviousSkin()
    {
        currentSkin--;

        if (currentSkin < 0)
            currentSkin = skins.Length - 1;

        UpdateUI();
    }

    private void UpdateUI()
    {
        skinPreview.sprite = skins[currentSkin];
        skinNameText.text = skinNames[currentSkin];
    }
}
