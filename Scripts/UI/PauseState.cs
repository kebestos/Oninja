using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseState : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    [SerializeField]
    Sprite pausedSprite, playingSprite;

    // Link to button's image
    Image image;

    // Instance used for singleton
    public static PauseState instance;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        // Ensure singleton
        if (PauseState.instance == null)
            PauseState.instance = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                if (CheckLinks())
                {
                    instance.image.sprite = instance.pausedSprite;
                }
                Resume();
            }
            else
            {
                if (CheckLinks())
                {
                    instance.image.sprite = instance.playingSprite;
                }
                
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void LoadMenu()
    {
        //SaveLoad.Save();
        SceneManager.LoadScene("Menu");
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    static bool CheckLinks()
    {
        return (instance.image != null && instance.playingSprite != null && instance.pausedSprite != null);
    }

    public void TogglePause()
    {
        GameIsPaused = !GameIsPaused; // Flip current status
        Debug.Log("GameIsPaused a change de valeur");
    }
}
