using UnityEngine;

public class SettingsManager : MonoBehaviour
{
   

    public static SettingsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }


}