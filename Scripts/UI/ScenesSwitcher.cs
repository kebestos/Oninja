using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ScenesSwitcher : MonoBehaviour
{
    GameObject aM;
    GameObject aM2;

    public SaveManager saveManager;

    private void Start()
    {
        aM = GameObject.FindGameObjectWithTag("Music");    
        aM2 = GameObject.FindGameObjectWithTag("MusicInGame");    
    }


    public void GotoMenuScene()
    {
        saveManager.Instance.Save();
        SceneManager.LoadScene("Menu");
        Destroy(aM2);
    }

    public void GotoCustomScene()
    {
        saveManager.Instance.Save();
        SceneManager.LoadScene("Custom");
    }

    public void GotoMoovingCharacter()
    {
        saveManager.Instance.Save();
        SceneManager.LoadScene("MoovingScene");
        Destroy(aM);
    }
}
