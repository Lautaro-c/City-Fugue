using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() 
    { 
    Application.Quit();
    
    }
}
